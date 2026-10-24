using System;
using System.Collections.Generic;
using System.Linq;
using ACE.Database.Models.Shard;
using ACE.Entity.Enum;

namespace ACE.Server.Managers
{
    public static class QuestMasteryCatalog
    {
        public sealed class Entry
        {
            public string Name { get; }
            public string LedgerQuest { get; }
            public bool Enabled { get; }
            public IReadOnlyList<string> SourceQuests { get; }
            public Entry(string name, string ledgerQuest, bool enabled, params string[] sourceQuests)
            {
                Name = name; LedgerQuest = ledgerQuest; Enabled = enabled;
                SourceQuests = Array.AsReadOnly(sourceQuests);
            }
        }
        // Stable identities. Retired pilot keys stay registered and reset-protected.
        public static IReadOnlyList<Entry> Entries { get; } = Array.AsReadOnly(new[]
        {
            new Entry("Aerbax's Citadel", "qm_v1_aerbax_s_citadel", true, "AerbaxCompletedWait"),
            new Entry("Aerbax's Prodigal Human", "qm_v1_aerbax_s_prodigal_human", true, "ProdigalHumanCompleted_1107"),
            new Entry("Aerbax's Prodigal Lugian", "qm_v1_aerbax_s_prodigal_lugian", true, "AerbaxsProdigalLugian_Repeat"),
            new Entry("Aerbax's Prodigal Mosswart", "qm_v1_aerbax_s_prodigal_mosswart", true, "AerbaxsProdigalMosswartCompleted"),
            new Entry("Aerbax's Prodigal Shadow", "qm_v1_aerbax_s_prodigal_shadow", true, "ProdigalShadowRepeat"),
            new Entry("Aetherium Raid", "qm_v1_aetherium_raid", true, "AetheriumRaidsLowCompleted"),
            new Entry("Alfrin's Stolen Supplies", "qm_v1_alfrin_s_stolen_supplies", true, "DrudgeHideoutCompleted_1111"),
            new Entry("Apostate Excavation Investigation", "qm_v1_apostate_excavation_investigation", true, "GearKnightExcavationComplete0809"),
            new Entry("Apostate Finale", "qm_v1_apostate_finale", true, "ApostateFinaleCompleted_0910"),
            new Entry("Asheron's Departure (Bur flagging)", "qm_v1_asheron_s_departure_bur_flagging", true, "AsheronsDepartureJournal"),
            new Entry("Assault Quest", "qm_v1_assault_quest", true, "TurshTotemHighComplete_0511"),
            new Entry("Atamarr Tasks", "qm_v1_atamarr_tasks", true, "AtamarrDone"),
            new Entry("Aun Tanua's War Taiaha Quest", "qm_v1_aun_tanua_s_war_taiaha_quest", true, "TanuaCompleted"),
            new Entry("Banderling Haunt", "qm_v1_banderling_haunt", true, "QuestBanderlingHauntCompleted0105"),
            new Entry("Blightfinger Quest", "qm_v1_blightfinger_quest", true, "TentaclesTthuunComplete0408"),
            new Entry("Bloodstone Investigation", "qm_v1_bloodstone_investigation", true, "BloodstoneFactoryCompleted_0211"),
            new Entry("Burun Liberator", "qm_v1_burun_liberator", true, "BurunLiberator_Repeat"),
            new Entry("Climbing the Viridian Deru Tree", "qm_v1_climbing_the_viridian_deru_tree", true, "ClimbedViridianDeru"),
            new Entry("Clutch of Kings (Broodu)", "qm_v1_clutch_of_kings_broodu", true, "BrooduComplete"),
            new Entry("Clutch of Kings (Browerk)", "qm_v1_clutch_of_kings_browerk", true, "BrowerkComplete"),
            new Entry("Clutch of Kings (Keerik)", "qm_v1_clutch_of_kings_keerik", true, "KeerikComplete"),
            new Entry("Clutch of Kings (Kiree)", "qm_v1_clutch_of_kings_kiree", true, "KireeComplete"),
            new Entry("Clutch of Kings (Reeshan)", "qm_v1_clutch_of_kings_reeshan", true, "ReeshanComplete"),
            new Entry("Clutch of Kings (Rehir)", "qm_v1_clutch_of_kings_rehir", true, "RehirComplete"),
            new Entry("Coral Tower Destroyer", "qm_v1_coral_tower_destroyer", true, "TaskCoralTowersWait"),
            new Entry("Count Phainor's Amulet", "qm_v1_count_phainor_s_amulet", true, "GraverobberCompleted_0213"),
            new Entry("Cow Tipping", "qm_v1_cow_tipping", true, "CowTipWait"),
            new Entry("Crystal Minds and Shattered Souls", "qm_v1_crystal_minds_and_shattered_souls", true, "GOTSOULSHATTERREWARD"),
            new Entry("Crystalline Bound Wisp", "qm_v1_crystalline_bound_wisp", true, "CrystallineSkillTest10109Wait"),
            new Entry("Crystalline Markers", "qm_v1_crystalline_markers", true, "CrystallineMarker25x"),
            new Entry("Dagger of Tikola Quest", "qm_pilot_dagger_of_tikola_v1", true, "fachubfolthid"),
            new Entry("Dark Isle Delivery", "qm_v1_dark_isle_delivery", true, "TaskDIDeliveryWait"),
            new Entry("Dark Isle Scouting", "qm_v1_dark_isle_scouting", true, "TaskDIScoutComplete"),
            new Entry("Death's Allure", "qm_v1_death_s_allure", true, "DeathsAllureQuestFinished"),
            new Entry("Deewain's Dark Cavern", "qm_v1_deewain_s_dark_cavern", true, "DeewainCompleted0211"),
            new Entry("Defeating Bandit Mana Hunter Boss", "qm_v1_defeating_bandit_mana_hunter_boss", true, "TaskFreebooterBanditBossKillWait"),
            new Entry("Defeating Vaeshok", "qm_v1_defeating_vaeshok", true, "TaskDIRuschkBossKillTaskWait"),
            new Entry("Defense of Zaikhal", "qm_v1_defense_of_zaikhal", true, "ZaikhalDefenseCopperComplete_0511"),
            new Entry("Dericost Ritual Investigation", "qm_v1_dericost_ritual_investigation", true, "RitualHuntCompleted_0311"),
            new Entry("Disrupting the Mana Siphon", "qm_v1_disrupting_the_mana_siphon", true, "GearknightInvasionHighSiphonWait_1009"),
            new Entry("Dream Reaver Investigation", "qm_v1_dream_reaver_investigation", true, "BTMCompleted_0513"),
            new Entry("Drudge Aviator", "qm_v1_drudge_aviator", true, "QuestReconnaissanceCompleted0105"),
            new Entry("Enrico's Betrayal", "qm_v1_enrico_s_betrayal", true, "EnricosBetrayalComplete_0511"),
            new Entry("Faces of the Mukkir", "qm_v1_faces_of_the_mukkir", true, "FacesMukkirComplete1005"),
            new Entry("Falatacot Depot Quest", "qm_v1_falatacot_depot_quest", true, "FalatacotDepotQuest_Completed"),
            new Entry("Fear Factory Quest", "qm_v1_fear_factory_quest", true, "FearFactoryCompleted_0813"),
            new Entry("Fiun Healing Machine Quest", "qm_v1_fiun_healing_machine_quest", true, "FiunHealingMachineCompleted_0511"),
            new Entry("Fiun Spellcasting Gloves Quest", "qm_v1_fiun_spellcasting_gloves_quest", true, "SpellCastingGloves"),
            new Entry("Foundry of Izexi Quest", "qm_v1_foundry_of_izexi_quest", true, "FoundryofIzexiCompleted_1212"),
            new Entry("Frest Greelving's Haunted Mansion", "qm_v1_frest_greelving_s_haunted_mansion", true, "GAVEEVILCOUCH"),
            new Entry("Gauntlet", "qm_v1_gauntlet", true, "GauntletWinsCounter"),
            new Entry("Gear Crossbow Quest", "qm_v1_gear_crossbow_quest", true, "gearxbowwait"),
            new Entry("Ghost Hunter Quest", "qm_v1_ghost_hunter_quest", true, "GhostHunterWait"),
            new Entry("Glenden Wood Invasion", "qm_v1_glenden_wood_invasion", true, "Completed_GWI80_1205", "Completed_GWI100_1205", "Completed_GWI130_1205"),
            new Entry("Grael's Summoning Chamber", "qm_v1_grael_s_summoning_chamber", true, "GraelsSummoningChamber_Wait"),
            new Entry("Graveyard Delivery", "qm_v1_graveyard_delivery", true, "TaskGrave2FedExWait"),
            new Entry("Guardian of the Deru Trees of Marae Lassel", "qm_v1_guardian_of_the_deru_trees_of_marae_lassel", true, "GuardianoftheDeruTrees_Completed"),
            new Entry("Halt Dericost Ritual Quest", "qm_v1_halt_dericost_ritual_quest", true, "RitualTimeCompleted_0411"),
            new Entry("Hamud's Demise", "qm_v1_hamud_s_demise", true, "HamudsDemiseCompleted_0511"),
            new Entry("Harlune's Diplomacy", "qm_v1_harlune_s_diplomacy", true, "CHDSCompletedOnce0608"),
            new Entry("Hea Bone and Hide Shirt Quest", "qm_v1_hea_bone_and_hide_shirt_quest", true, "HeaBoneHeadTurnin", "HeaBoneTreatyTurnin"),
            new Entry("Head of Baron Entemarre", "qm_v1_head_of_baron_entemarre", true, "CampEntemarreCompleted"),
            new Entry("Hoshino Fortress Infiltration", "qm_v1_hoshino_fortress_infiltration", true, "HoshinoTowerComplete_0812"),
            new Entry("Hoshino Must Die", "qm_v1_hoshino_must_die", true, "DefeatHoshinoKeiCompleted_0113"),
            new Entry("Ithaenc Quiddity Seed Quest", "qm_v1_ithaenc_quiddity_seed_quest", true, "clawofthehopeslayerfirstrun"),
            new Entry("Janthef's Release", "qm_v1_janthef_s_release", true, "EscapeCompleted_0213"),
            new Entry("Jester Focuses", "qm_v1_jester_focuses", true, "JesterFocusesWait"),
            new Entry("Jester's Lost Marbles", "qm_v1_jester_s_lost_marbles", true, "jestermarkercomplete"),
            new Entry("Kidnapped Handmaiden", "qm_v1_kidnapped_handmaiden", true, "KidnapPrincessComplete_0410"),
            new Entry("Liberation of Uziz", "qm_v1_liberation_of_uziz", true, "AbductedCompleted_0313"),
            new Entry("Lord Rytheran's Journal", "qm_v1_lord_rytheran_s_journal", true, "rythjournalrepeat"),
            new Entry("Lost Lore", "qm_v1_lost_lore", true, "LostLorePageCompleted_0712"),
            new Entry("Lost Messenger", "qm_v1_lost_messenger", true, "LostMessengerQuest_Wait"),
            new Entry("Lost Pet", "qm_v1_lost_pet", true, "LostPetQuestCompleted0805"),
            new Entry("Lunnum's Pyre", "qm_v1_lunnum_s_pyre", true, "LunnumsPyreQuestCompleted0805"),
            new Entry("Lunnum's Return", "qm_v1_lunnum_s_return", true, "LunnumsReturnComplete1006"),
            new Entry("Messenger's Collar Quest", "qm_v1_messenger_s_collar_quest", true, "MessengersCollar_Repeat"),
            new Entry("Moarsman Spawning Pools", "qm_v1_moarsman_spawning_pools", true, "TaskSpawnPoolsWait"),
            new Entry("Moarsmen on the Shores", "qm_v1_moarsmen_on_the_shores", true, "RedspireMoarsmenComplete_0508", "MayoiMoarsmenComplete_0508", "DanbyMoarsmenComplete_0508", "BeachFortMoarsmenComplete_0508", "AyanBaqurMoarsmenComplete_0508", "EastwatchMoarsmenComplete_0508"),
            new Entry("Monouga Laboratory", "qm_v1_monouga_laboratory", true, "ProdigalMonougaCompleted0707"),
            new Entry("Monthly Game Hunter", "qm_v1_monthly_game_hunter", true, "MGHEasyCompleted", "MGHModerateCompleted", "MGHHardCompleted", "MGHVeryHardCompleted", "MGHExtremeCompleted"),
            new Entry("Mountain Sewer Quest", "qm_v1_mountain_sewer_quest", true, "BurFlagged(Gem)"),
            new Entry("Necklace of the Elemental Adepts Quest", "qm_v1_necklace_of_the_elemental_adepts_quest", true, "NecklaceOfTheElementsComplete"),
            new Entry("Nexus Crawl", "qm_v1_nexus_crawl", true, "NexusCrawlCompleted0509"),
            new Entry("Ninja Academy", "qm_v1_ninja_academy", true, "NinjaAcademyFinished_0812"),
            new Entry("Northern Shroud Cabal", "qm_v1_northern_shroud_cabal", true, "NorthernShroudCabalComplete_0511"),
            new Entry("Of Trust and Betrayal", "qm_v1_of_trust_and_betrayal", true, "OfTrustBetrayalComplete_0511"),
            new Entry("Olthoi Queen Quest", "qm_v1_olthoi_queen_quest", true, "MaraeFinalReward"),
            new Entry("Palm Fort", "qm_v1_palm_fort", true, "PalmFortDefended1209"),
            new Entry("Paradox-touched Olthoi Egg Quest", "qm_v1_paradox_touched_olthoi_egg_quest", true, "ParadoxEggWait"),
            new Entry("Pay Respects to Antius Blackmoor", "qm_v1_pay_respects_to_antius_blackmoor", true, "paidrespectstoantiusblackmoor"),
            new Entry("Portal Space Rifts", "qm_v1_portal_space_rifts", true, "PortalSpaceRiftsDone"),
            new Entry("Queen Elysa in Danger", "qm_v1_queen_elysa_in_danger", true, "QueenElysaInDangerCompleted"),
            new Entry("Red Veined Grub Collection Task", "qm_v1_red_veined_grub_collection_task", true, "RedVeinedGrubTaskWait"),
            new Entry("Releasing the Light", "qm_v1_releasing_the_light", true, "BlessedCaveCompleted_0912"),
            new Entry("Renegade Mace Quest", "qm_v1_renegade_mace_quest", true, "ReignofTerrorComplete_0511"),
            new Entry("Rynthid Assessment", "qm_v1_rynthid_assessment", true, "RynthidTrainingCompleted_1013"),
            new Entry("Rynthid Foothold", "qm_v1_rynthid_foothold", true, "GenesisCompleted_0813"),
            new Entry("Save Karul", "qm_v1_save_karul", true, "DeconstructionCompleted_0213"),
            new Entry("Sawato Extortion", "qm_v1_sawato_extortion", true, "SawatoExtortionComplete0806"),
            new Entry("Search for Gareth Dain", "qm_v1_search_for_gareth_dain", true, "DainAlive", "DainDead"),
            new Entry("Sepulcher of Nightmares", "qm_v1_sepulcher_of_nightmares", true, "TomeNightmareSepulcher_0805"),
            new Entry("Shade Iron", "qm_v1_shade_iron", true, "ShadeIronWait"),
            new Entry("Shambling Archivist Destroyer", "qm_v1_shambling_archivist_destroyer", true, "TaskGrave1BossKillWait"),
            new Entry("Shield of Yanshi Quest", "qm_v1_shield_of_yanshi_quest", true, "YanshiTunnelsComplete1205"),
            new Entry("Silifi of Crimson Night Quest", "qm_v1_silifi_of_crimson_night_quest", true, "SilifiofCrimsonNightComplete"),
            new Entry("Snow Tusker Blood Collection", "qm_v1_snow_tusker_blood_collection", true, "TuskerBloodCollectionWait"),
            new Entry("Soul Hunter Quest", "qm_v1_soul_hunter_quest", true, "SoulHunterQuestWait_1106"),
            new Entry("Soul Stone Quest", "qm_v1_soul_stone_quest", true, "RoadsComplete"),
            new Entry("Southern Shroud Cabal", "qm_v1_southern_shroud_cabal", true, "SouthernShroudCabalComplete_0511"),
            new Entry("Splitting Grael", "qm_v1_splitting_grael", true, "SplitGraelHighComplete0806"),
            new Entry("Supply Saboteur", "qm_v1_supply_saboteur", true, "SuppliesTurnedIn1209"),
            new Entry("Suzuhara Baijin's Delivery", "qm_v1_suzuhara_baijin_s_delivery", true, "SuzuharaComplete_0511"),
            new Entry("Tanada House of Water Quest", "qm_v1_tanada_house_of_water_quest", true, "TanadaHouseofWaterCompleted"),
            new Entry("Tanada Temple of Black Water Quest", "qm_v1_tanada_temple_of_black_water_quest", true, "TanadaTempleofBlackWaterCompleted"),
            new Entry("The Archivist Quest", "qm_v1_the_archivist_quest", true, "TheArchivist_Repeat"),
            new Entry("The Black Spear Ice Totem", "qm_v1_the_black_spear_ice_totem", true, "BlackSpearIceTotemCompleted"),
            new Entry("The Shadows of Bitter Winter", "qm_v1_the_shadows_of_bitter_winter", true, "ShadowsWinterComplete1205"),
            new Entry("Tracing The Stone", "qm_v1_tracing_the_stone", true, "TaskGrave2WallCarvingWait"),
            new Entry("Tracking Grael", "qm_v1_tracking_grael", true, "TrackingGrael_Wait"),
            new Entry("Trade Alliance Quest", "qm_v1_trade_alliance_quest", true, "TradeAllianceQuestCompleted"),
            new Entry("Uncovering the Renegades", "qm_v1_uncovering_the_renegades", true, "UncoverRenegadesComplete0309"),
            new Entry("Unleash the Gearknights", "qm_v1_unleash_the_gearknights", true, "UnleashGearKnightComplete0909"),
            new Entry("Virindi Rescue", "qm_v1_virindi_rescue", true, "VirindiRescueCompleted0612"),
            new Entry("Wardley and the Wights", "qm_v1_wardley_and_the_wights", true, "FrozenWightLairComplete1111"),
            new Entry("Whispering Blade Chapterhouse Access", "qm_v1_whispering_blade_chapterhouse_access", true, "WhisperingBladeChapterhouseAccess_Wait"),
            new Entry("Whispering Blade Infiltration", "qm_v1_whispering_blade_infiltration", true, "WhisperingBladeInfiltrationWait"),
            new Entry("Green Mire Grave", "qm_pilot_green_mire_grave_v1", false, "fachubgreenmiregrave"),
            new Entry("Glenden Wood Dungeon", "qm_pilot_glenden_wood_v1", false, "fachubglendenwood"),
            new Entry("Colier Mines", "qm_pilot_colier_mines_v1", false, "fachubcolier"),
            new Entry("Mosswart Ice Tachi", "qm_pilot_ice_tachi_v1", false, "fachubmosswartcamp"),
        });
        public static string Normalize(string name) => (name ?? string.Empty).Split('@')[0];
        public static bool HasPositiveRecord(IEnumerable<CharacterPropertiesQuestRegistry> records, string key) =>
            records.Any(r => r.NumTimesCompleted > 0 && string.Equals(Normalize(r.QuestName), Normalize(key), StringComparison.OrdinalIgnoreCase));
        public static Entry FindSource(string key) => Entries.FirstOrDefault(e => e.Enabled && e.SourceQuests.Any(s =>
            string.Equals(s, Normalize(key), StringComparison.OrdinalIgnoreCase)));
        public static bool IsPermanentLedger(string key) => Entries.Any(e =>
            string.Equals(e.LedgerQuest, Normalize(key), StringComparison.OrdinalIgnoreCase));
        public static bool HasEarned(IEnumerable<CharacterPropertiesQuestRegistry> records, Entry entry) => entry.Enabled && HasPositiveRecord(records, entry.LedgerQuest);
        public static bool ShouldAward(IEnumerable<CharacterPropertiesQuestRegistry> records, Entry entry) =>
            entry.Enabled && !HasEarned(records, entry) && entry.SourceQuests.Any(s => HasPositiveRecord(records, s));
        public static int CountEarned(IEnumerable<CharacterPropertiesQuestRegistry> records) => Entries.Count(e => HasEarned(records, e));
        public static decimal BonusPercent(int count) => Math.Clamp(count, 0, 200) * 0.5m;
        // Apply only to the recipient's final share; never feed this bonus back into sharing.
        public static long ApplyBonus(long amount, XpType type, int count)
        {
            if (amount <= 0 || (type != XpType.Kill && type != XpType.Quest)) return amount;
            var result = decimal.Round(amount * (1m + BonusPercent(count) / 100m), 0, MidpointRounding.ToEven);
            return result >= long.MaxValue ? long.MaxValue : (long)result;
        }
        public static string AwardMessage(string name, int count, bool rewardsActive, bool recovered = false) =>
            $"Quest Mastery {(recovered ? "previous completion recorded" : "awarded")}: {name}. " +
            $"Total: {count} unique adventures. " +
            (rewardsActive ? $"Permanent personal XP and Luminance bonus: +{BonusPercent(count):0.#}%." : "Power rewards are not active.");
    }
}
