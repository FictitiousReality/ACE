using System;
using System.Collections.Generic;
using System.Linq;
using ACE.Database.Models.Shard;
using ACE.Entity.Enum;
using ACE.Server.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACE.Server.Tests
{
    [TestClass]
    public class QuestMasteryTests
    {
        private static QuestMasteryCatalog.Entry Sample => QuestMasteryCatalog.FindSource("fachubfolthid");
        private static CharacterPropertiesQuestRegistry Record(string key, int count = 1) =>
            new CharacterPropertiesQuestRegistry { QuestName = key, NumTimesCompleted = count };
        private static QuestManager Registry(params CharacterPropertiesQuestRegistry[] records)
        {
            var manager = new QuestManager((ACE.Server.WorldObjects.Creature)null);
            foreach (var r in records) manager.GetQuests().Add(r);
            return manager;
        }

        [TestMethod]
        public void RosterHas127DistinctAdventuresAnd140UniqueSources()
        {
            var enabled = QuestMasteryCatalog.Entries.Where(e => e.Enabled).ToList();
            Assert.AreEqual(127, enabled.Count);
            Assert.AreEqual(127, enabled.Select(e => e.LedgerQuest).Distinct().Count());
            var keys = enabled.SelectMany(e => e.SourceQuests).ToList();
            Assert.AreEqual(140, keys.Count);
            Assert.AreEqual(140, keys.Distinct(StringComparer.OrdinalIgnoreCase).Count());
        }

        [TestMethod]
        public void EverySourceAwardsOnceThroughActualRegistryTransition()
        {
            foreach (var entry in QuestMasteryCatalog.Entries.Where(e => e.Enabled))
            foreach (var key in entry.SourceQuests)
            {
                var manager = Registry(Record(key));
                Assert.IsTrue(QuestMasteryManager.RecordAward(manager, entry, true), key);
                Assert.IsFalse(QuestMasteryManager.RecordAward(manager, entry, true), key);
                Assert.AreEqual(1, QuestMasteryCatalog.CountEarned(manager.GetQuests()));
                manager.Erase(key);
                Assert.IsFalse(QuestMasteryManager.RecordAward(manager, entry, true));
                Assert.AreEqual(1, manager.GetCurrentSolves(entry.LedgerQuest));
            }
        }

        [TestMethod]
        public void MissingDefinitionAndPartialOrNonpositiveProofCannotAward()
        {
            Assert.IsFalse(QuestMasteryManager.RecordAward(Registry(Record("fachubfolthid")), Sample, false));
            foreach (var count in new[] { 0, -1 })
                Assert.IsFalse(QuestMasteryManager.RecordAward(Registry(Record("fachubfolthid", count)), Sample, true));
            Assert.IsFalse(QuestMasteryManager.RecordAward(Registry(Record("fachubfolthidportal_flag")), Sample, true));
            Assert.IsNull(QuestMasteryCatalog.FindSource("fachubfolthid_extra"));
        }

        [TestMethod]
        public void AlternativesUseOneLedgerAndCharactersStayIndependent()
        {
            foreach (var entry in QuestMasteryCatalog.Entries.Where(e => e.Enabled && e.SourceQuests.Count > 1))
            {
                var first = Registry(entry.SourceQuests.Select(k => Record(k, 50)).ToArray());
                var second = Registry(Record(entry.SourceQuests.Last()));
                Assert.IsTrue(QuestMasteryManager.RecordAward(first, entry, true));
                Assert.IsFalse(QuestMasteryManager.RecordAward(first, entry, true));
                Assert.IsTrue(QuestMasteryManager.RecordAward(second, entry, true));
                Assert.AreEqual(1, QuestMasteryCatalog.CountEarned(first.GetQuests()));
                Assert.AreEqual(1, QuestMasteryCatalog.CountEarned(second.GetQuests()));
            }
        }

        [TestMethod]
        public void ResetPathsProtectEnabledAndRetiredAllocatedKeys()
        {
            foreach (var entry in QuestMasteryCatalog.Entries)
            {
                var manager = Registry(Record(entry.LedgerQuest), Record("ordinary_repeat"));
                manager.Erase(entry.LedgerQuest.ToUpperInvariant() + "@repeat");
                manager.Decrement(entry.LedgerQuest, 99);
                manager.SetQuestCompletions(entry.LedgerQuest, 0);
                manager.SetQuestBits(entry.LedgerQuest, 1, false);
                manager.EraseAll();
                Assert.AreEqual(1, manager.GetCurrentSolves(entry.LedgerQuest));
                Assert.AreEqual(1, manager.GetQuests().Count);
            }
            Assert.IsFalse(QuestMasteryCatalog.IsPermanentLedger("qm_unallocated"));
            Assert.IsFalse(QuestMasteryCatalog.IsPermanentLedger(Sample.SourceQuests[0]));
        }

        [TestMethod]
        public void SavedLedgersCountWithoutSourceRecordsAndIgnoreHeldPilot()
        {
            var records = QuestMasteryCatalog.Entries.Where(e => e.Enabled).Select(e => Record(e.LedgerQuest, 99)).ToList();
            records.Add(Record("qm_pilot_green_mire_grave_v1"));
            Assert.AreEqual(127, QuestMasteryCatalog.CountEarned(records));
            Assert.AreSame(Sample, QuestMasteryCatalog.FindSource("FACHUBFOLTHID@comment"));
            Assert.IsTrue(QuestMasteryCatalog.IsPermanentLedger("QM_PILOT_GREEN_MIRE_GRAVE_V1@reset"));
            Assert.IsNull(QuestMasteryCatalog.FindSource("fachubgreenmiregrave"));
        }

        [TestMethod]
        public void BonusIsHalfPercentPerQuestAndCapsWithoutRescaling()
        {
            Assert.AreEqual(1000L, QuestMasteryCatalog.ApplyBonus(1000, XpType.Kill, 0));
            Assert.AreEqual(1005L, QuestMasteryCatalog.ApplyBonus(1000, XpType.Quest, 1));
            Assert.AreEqual(1635L, QuestMasteryCatalog.ApplyBonus(1000, XpType.Kill, 127));
            Assert.AreEqual(2000L, QuestMasteryCatalog.ApplyBonus(1000, XpType.Quest, 250));
            Assert.AreEqual(100m, QuestMasteryCatalog.BonusPercent(200));
            Assert.AreEqual(long.MaxValue, QuestMasteryCatalog.ApplyBonus(long.MaxValue, XpType.Kill, 200));
        }

        [TestMethod]
        public void RecipientBonusDoesNotMultiplyAnotherRecipientsShare()
        {
            // The shared base is divided first; each recipient uses only their own ledger.
            var shares = new[] { 500L, 500L };
            Assert.AreEqual(1000L, QuestMasteryCatalog.ApplyBonus(shares[0], XpType.Kill, 200));
            Assert.AreEqual(500L, QuestMasteryCatalog.ApplyBonus(shares[1], XpType.Kill, 0));
            foreach (var type in new[] { XpType.Admin, XpType.Allegiance, XpType.Proficiency, XpType.Emote, XpType.Fellowship })
                Assert.AreEqual(500L, QuestMasteryCatalog.ApplyBonus(500, type, 200));
        }

        [TestMethod]
        public void FeatureSwitchesDisableRewardsWithoutRemovingCredit()
        {
            Assert.IsTrue(PropertyManager.ModifyBool("quest_mastery_enabled", false));
            Assert.IsTrue(PropertyManager.ModifyBool("quest_mastery_rewards_enabled", true));
            Assert.IsFalse(QuestMasteryManager.RewardsActive);
            Assert.AreEqual(500L, QuestMasteryManager.ApplyPersonalBonus(null, 500, XpType.Kill));
            PropertyManager.ModifyBool("quest_mastery_enabled", true);
            Assert.IsTrue(QuestMasteryManager.RewardsActive);
            PropertyManager.ModifyBool("quest_mastery_rewards_enabled", false);
            Assert.IsFalse(QuestMasteryManager.RewardsActive);
            Assert.AreEqual(500L, QuestMasteryManager.ApplyPersonalBonus(null, 500, XpType.Quest));
            PropertyManager.ModifyBool("quest_mastery_enabled", false);
        }

        [TestMethod]
        public void MessagesDistinguishAwardRecoveryAndInactiveRewards()
        {
            StringAssert.Contains(QuestMasteryCatalog.AwardMessage(Sample.Name, 12, true), "+6%");
            StringAssert.Contains(QuestMasteryCatalog.AwardMessage(Sample.Name, 12, true), Sample.Name);
            StringAssert.Contains(QuestMasteryCatalog.AwardMessage(Sample.Name, 12, false), "Power rewards are not active");
            StringAssert.Contains(QuestMasteryCatalog.AwardMessage(Sample.Name, 12, true, true), "previous completion recorded");
        }
    }
}
