# Quest Mastery v1 — 127 adventures

Implemented on branch `quest-mastery` in a fresh clone of `FictitiousReality/ACE`, based on `7f87f9d8`. The existing Buadren checkout and live server have not been deployed or modified by this release work.

Each approved unique adventure grants a permanent character ledger and a private award message. Each credit adds **0.5% personal XP and Luminance**, capped at +100% at 200. This roster has 127 adventures, so its maximum is **+63.5% (1.635x)**. Global rates stay unchanged. Five families accept multiple final source keys but earn one credit. The 23 pending hook/guard objectives and the separate Lunnum proposal are excluded.

## Files and scope

- `quest-mastery-v1.sql` is the single flat WORLD installer containing every database change in this release: 127 independent ledger definitions. The identical Git copy is `Database/Optional/QuestMastery/quest-mastery-v1.sql`.
- `quest-mastery-roster-v1.json` lists every stable ledger, exact source keys, audited objective, route limit, and wiki link. Git copy: `Database/Optional/QuestMastery/roster-v1.json`.
- Server source is required for awards, notifications, protection, command, and personal rewards. SQL alone cannot implement these features.
- No new item WCIDs, recipes, vendor placements, native NPC edits, or shard schema changes are included. No account/character data or credentials are packaged.

The installer checks native quest definitions and completion writer counts by WCID/key, without relying on database-local auto-increment emote IDs. It rejects unexpected writers, implicit Quest-string writers, and conflicting ledger identities. These are structural compatibility checks, not a full re-audit of modified destination content. This roster was audited against standard world 0.9.296; worlds with changed completion branches need review even if their structural checks pass. Do not bypass a failed preflight.

## Installation

1. Back up world, shard and auth using your normal backup process. Stop ACE gracefully and wait for saves/process exit.
2. Build the branch for the destination host. Use a native ARM64 build on this Windows ARM server; the isolated Release/x64 build used for tests is not a deployment build for it.
3. Select the destination WORLD schema in your SQL client and import `quest-mastery-v1.sql`. No database name or credentials are embedded. A second import is safe for matching definitions. After a failed preflight, correct the content difference before retrying; the installer recreates its own procedure on the next run.
4. Deploy the built server, preserve its existing private configuration, and restart. Restart refreshes quest-definition caches. No recipe/template cache or existing inventory instance migration is needed because no items or recipes changed.
5. Initially both feature switches default to false. On an admin character, enable tracking with `/modifybool quest_mastery_enabled true`. Keep rewards disabled for score-only validation; messages explicitly say power rewards are inactive.
6. Enable rewards with `/modifybool quest_mastery_rewards_enabled true` when ready. These are server feature switches, not global XP/Luminance rate changes. ACE persists modified properties through its normal property-save cycle; use graceful shutdown.

Disable `quest_mastery_rewards_enabled` to suspend the bonus while retaining tracking, or `quest_mastery_enabled` to suspend both. Both preserve earned ledgers. Do not erase permanent records to disable a feature.

## Award and reward behavior

The private award message names the canonical adventure, total earned count, and active personal XP/Luminance bonus. It is sent only after a new ledger transition is confirmed. Repeats, alternative reward paths, and already-credited records do not announce another award. `/questmastery` shows the count, active bonus or disabled/score-only state, and completed adventures.

Running `/questmastery` also recovers eligible, positive native final records that have not yet earned a ledger. Recovered credits receive a clearly labeled historical-credit summary. Missing or erased native records are not guessed. Ordinary relogs preserve ledgers and do not themselves reannounce credit.

The bonus applies to Kill/Quest earnings after existing modifiers and fellowship sharing, using each recipient's own credit. Original earning type is preserved through fellowship forwarding, so sharing an Admin or other excluded grant cannot turn it into bonus-eligible earnings. Allegiance pass-up and item XP use their original amounts; this is a personal character XP/Luminance bonus. Luminance unlock and storage limits remain in the normal path. Ordinary rounding uses midpoint-to-even; small rewards may round to the same whole number.

Credit takes effect when the native completion stamp is processed. An earning granted afterward uses the new total; an earning already granted retains the previous total. Some quests stamp completion between reward actions, so a multi-action payout can straddle this point. This implementation follows event ordering and does not retroactively recalculate rewards or change native quest scripts. A mastery message confirms the mastery award, not successful delivery of every native reward item.

## Stable identities and prior pilot

The Dagger of Tikola entry retains `qm_pilot_dagger_of_tikola_v1`, preserving existing credit. Its original pilot definition is accepted unchanged. The other four allocated pilot identities remain registered and reset-protected but disabled in the approved roster, including Green Mire Grave, whose later provenance review remains held. Their saved rows are not deleted. See `roster-v1.json` for the new `qm_v1_*` allocations. Native repeat flags remain resettable; permanent ledger keys reject erase, erase-all, decrement, counter reset and bit reset through QuestManager.

## Verification and gameplay checklist

Release/x64 server build succeeded. **10 new QuestMastery tests plus 1 existing skill-formula test passed**. The new tests exercise every one of 140 source keys through the actual registry award transition, source erasure, repeated events, multiple-route families, character isolation, all protected reset paths, exact identity matching, saved-ledger counting, feature switches, reward calculations/cap/overflow, and notification wording.

Disposable MySQL validation passed: fresh 127-ledger import, idempotent re-import, prior Tikola compatibility, collision rejection with no partial inserts, changed native definition rejection, and changed writer-type rejection. The disposable schema was removed and live world quest rows were unchanged. See `quest-mastery-sql-validation.json`.

These are source/SQL tests, not in-game verification. Before release acceptance:

1. In score-only mode complete an eligible audited quest on a character without its ledger. Confirm exactly one named award message and `/questmastery` entry; no active-bonus claim.
2. Repeat the quest or use another approved reward/faction route. Confirm no extra credit or award message.
3. Enable rewards and verify a known Kill/Quest earning at a known count. One credit should multiply otherwise-applicable earnings by 1.005; 127 by 1.635. Check XP and Luminance separately, with an unlocked character and available storage for Luminance.
4. Use two characters with different mastery counts in a fellowship. Confirm each receives their own multiplier after sharing, without a second application or propagation of the originating character's mastery.
5. Exercise a destination reward and a multi-source family, plus `/questmastery` recovery of a valid old final record. Recovery must be labeled and must not repeat on a second command.
6. Log out, allow saves, restart gracefully, and log back in. Confirm the same credit and bonus without another award announcement. Verify normal quest repeat resets leave mastery intact.

No gameplay checks above have been performed for this release, and it has not been installed into the local live world.
