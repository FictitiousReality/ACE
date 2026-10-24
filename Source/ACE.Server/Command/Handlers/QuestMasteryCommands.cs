using System.Linq;
using System.Text;
using ACE.Entity.Enum;
using ACE.Server.Managers;
using ACE.Server.Network;

namespace ACE.Server.Command.Handlers
{
    public static class QuestMasteryCommands
    {
        [CommandHandler("questmastery", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 0,
            "Shows permanent Quest Mastery credit and records supported historical completions.")]
        public static void HandleQuestMastery(Session session, params string[] parameters)
        {
            var player = session?.Player;
            if (player == null) return;
            QuestMasteryManager.Synchronize(player);
            var records = player.QuestManager.GetQuests();
            var count = QuestMasteryCatalog.CountEarned(records);
            var text = new StringBuilder();
            text.AppendLine($"Quest Mastery: {count}/{QuestMasteryCatalog.Entries.Count(e => e.Enabled)} unique adventures.");
            text.AppendLine(!QuestMasteryManager.Enabled ? "Quest Mastery is disabled. Saved credit is retained." :
                QuestMasteryManager.RewardsActive ? $"Permanent personal XP and Luminance bonus: +{QuestMasteryCatalog.BonusPercent(count):0.#}%." : "Power rewards are not active.");
            foreach (var entry in QuestMasteryCatalog.Entries.Where(e => QuestMasteryCatalog.HasEarned(records, e)))
                text.AppendLine($"[COMPLETE] {entry.Name}");
            text.Append("Repeats add no credit. Missing old records do not prove a quest was never completed.");
            CommandHandlerHelper.WriteOutputInfo(session, text.ToString(), ChatMessageType.Broadcast);
        }
    }
}
