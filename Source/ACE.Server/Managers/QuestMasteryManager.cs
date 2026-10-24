using System.Collections.Generic;
using ACE.Database;
using ACE.Entity.Enum;
using ACE.Server.Network.GameMessages.Messages;
using ACE.Server.WorldObjects;

namespace ACE.Server.Managers
{
    public static class QuestMasteryManager
    {
        public static bool Enabled => PropertyManager.GetBool("quest_mastery_enabled").Item;
        public static bool RewardsActive => Enabled && PropertyManager.GetBool("quest_mastery_rewards_enabled").Item;

        public static void Observe(Player player, string sourceQuest)
        {
            if (!Enabled) return;
            var entry = QuestMasteryCatalog.FindSource(sourceQuest);
            if (entry != null && TryAward(player, entry))
                Notify(player, QuestMasteryCatalog.AwardMessage(entry.Name,
                    QuestMasteryCatalog.CountEarned(player.QuestManager.GetQuests()), RewardsActive));
        }

        // Explicit command recovery: no inference from absent/expired native flags.
        public static void Synchronize(Player player)
        {
            if (!Enabled) return;
            var recovered = new List<string>();
            foreach (var entry in QuestMasteryCatalog.Entries)
                if (entry.Enabled && TryAward(player, entry)) recovered.Add(entry.Name);
            if (recovered.Count > 0)
                Notify(player, QuestMasteryCatalog.AwardMessage(string.Join(", ", recovered),
                    QuestMasteryCatalog.CountEarned(player.QuestManager.GetQuests()), RewardsActive, true));
        }

        private static bool TryAward(Player player, QuestMasteryCatalog.Entry entry) =>
            RecordAward(player.QuestManager, entry, DatabaseManager.World.GetCachedQuest(entry.LedgerQuest) != null);

        // Shared transition used by live completion and recovery. Return success only
        // after the actual character registry contains the new protected record.
        public static bool RecordAward(QuestManager manager, QuestMasteryCatalog.Entry entry, bool definitionExists)
        {
            lock (manager)
            {
                if (!definitionExists || !QuestMasteryCatalog.ShouldAward(manager.GetQuests(), entry)) return false;
                manager.Update(entry.LedgerQuest);
                return QuestMasteryCatalog.HasEarned(manager.GetQuests(), entry);
            }
        }

        public static long ApplyPersonalBonus(Player recipient, long amount, XpType source) =>
            RewardsActive ? QuestMasteryCatalog.ApplyBonus(amount, source,
                QuestMasteryCatalog.CountEarned(recipient.QuestManager.GetQuests())) : amount;

        private static void Notify(Player player, string text) =>
            player.Session.Network.EnqueueSend(new GameMessageSystemChat(text, ChatMessageType.Broadcast));
    }
}
