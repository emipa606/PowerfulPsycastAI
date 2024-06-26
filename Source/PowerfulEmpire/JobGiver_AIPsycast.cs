using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace PowerfulEmpire;

internal class JobGiver_AIPsycast : ThinkNode_JobGiver
{
    public static bool allowAIHeatPush;

    public static bool allowBerserkPulse;

    public static bool allowManhunterPulse;

    public static bool allowBerserk;

    public static bool allowWaterskip;

    public static bool allowVertigoPulse;

    public static bool allowInvisibility;

    public static bool allowBeckon;

    public static bool allowBlindingPulse;

    public static bool allowSmokepop;

    public static bool allowWallraise;

    public static bool allowPainblock;

    public static bool allowFocus;

    public static bool allowStun;

    public static bool allowMassChaosSkip;

    public static bool allowChaosSkip;

    public static bool allowSkip;

    public static bool allowBurden;

    protected override Job TryGiveJob(Pawn pawn)
    {
        if (pawn.mindState.enemyTarget == null || !pawn.HasPsylink || pawn.psychicEntropy.Severity != 0)
        {
            return null;
        }

        var abilities = pawn.abilities.abilities;
        if (abilities.Count == 0)
        {
            return null;
        }

        var enemyTarget = pawn.mindState.enemyTarget as Pawn;
        var building = pawn.mindState.enemyTarget as Building;
        var berserkPulse = false;
        var manhunterPulse = false;
        var berserk = false;
        var vertigoPulse = false;
        var waterskip = false;
        var invisibility = false;
        var beckon = false;
        var blindingPulse = false;
        var smokepop = false;
        var wallraise = false;
        var painblock = false;
        var focus = false;
        var stun = false;
        var massChaosSkip = false;
        var chaosSkip = false;
        var skip = false;
        var burden = false;
        if (enemyTarget != null && (enemyTarget.DestroyedOrNull() || enemyTarget.IsPsychologicallyInvisible() ||
                                    enemyTarget.Downed ||
                                    enemyTarget.GetStatValue(StatDefOf.PsychicSensitivity) == 0f ||
                                    !GenSight.LineOfSight(pawn.Position, enemyTarget.Position, pawn.Map, false) ||
                                    enemyTarget.InMentalState ||
                                    enemyTarget.MentalStateDef == MentalStateDefOf.PanicFlee ||
                                    enemyTarget.CurJobDef == JobDefOf.GotoMindControlled))
        {
            enemyTarget = (from Pawn pwn in pawn.Map.mapPawns.AllPawns
                where pwn.HostileTo(pawn.Faction) && !pwn.IsPsychologicallyInvisible() && !pwn.Downed &&
                      pwn.GetStatValue(StatDefOf.PsychicSensitivity) != 0f &&
                      GenSight.LineOfSight(pawn.Position, pwn.Position, pawn.Map, false) && !pwn.InMentalState &&
                      pwn.MentalStateDef != MentalStateDefOf.PanicFlee &&
                      pwn.CurJobDef != JobDefOf.GotoMindControlled
                select pwn).RandomElementWithFallback();
        }

        foreach (var ability in abilities)
        {
            if (enemyTarget != null)
            {
                if (ability.def == DefOfLocal.BerserkPulse && allowBerserkPulse)
                {
                    berserkPulse = true;
                }

                if (ability.def == DefOfLocal.ManhunterPulse && allowManhunterPulse)
                {
                    manhunterPulse = true;
                }

                if (ability.def == DefOfLocal.VertigoPulse && allowVertigoPulse)
                {
                    vertigoPulse = true;
                }

                if (ability.def == DefOfLocal.BlindingPulse && allowBlindingPulse)
                {
                    blindingPulse = true;
                }

                if (ability.def == DefOfLocal.Berserk && allowBerserk)
                {
                    berserk = true;
                }

                if (ability.def == DefOfLocal.Beckon && allowBeckon)
                {
                    beckon = true;
                }

                if (ability.def == DefOfLocal.Stun && allowStun)
                {
                    stun = true;
                }

                if (ability.def == DefOfLocal.MassChaosSkip && allowMassChaosSkip)
                {
                    massChaosSkip = true;
                }

                if (ability.def == DefOfLocal.ChaosSkip && allowChaosSkip)
                {
                    chaosSkip = true;
                }

                if (ability.def == DefOfLocal.Skip && allowSkip)
                {
                    skip = true;
                }

                if (ability.def == DefOfLocal.Burden && allowBurden)
                {
                    burden = true;
                }
            }

            if (ability.def == DefOfLocal.Waterskip && allowWaterskip)
            {
                waterskip = true;
            }

            if (ability.def == DefOfLocal.Wallraise && allowWallraise)
            {
                wallraise = true;
            }

            if (ability.def == DefOfLocal.Smokepop && allowSmokepop)
            {
                smokepop = true;
            }

            if (ability.def == DefOfLocal.Invisibility && allowInvisibility)
            {
                invisibility = true;
            }

            if (ability.def == DefOfLocal.Focus && allowFocus)
            {
                focus = true;
            }

            if (ability.def == DefOfLocal.Painblock && allowPainblock)
            {
                painblock = true;
            }
        }

        if (!enemyTarget.DestroyedOrNull() && pawn.CanReserve(enemyTarget) && !enemyTarget.GetRegion().touchesMapEdge)
        {
            if (berserkPulse && pawn.psychicEntropy.CurrentPsyfocus > 0.5 &&
                pawn.Position.InHorDistOf(enemyTarget.Position, 14.9f))
            {
                var possiblePawn = (from Pawn pwn in pawn.Map.mapPawns.AllPawns
                    where pwn.Position.InHorDistOf(enemyTarget.Position, 2.9f)
                    select pwn).ToList();
                var num = 0;
                var num2 = 0;
                foreach (var afffectedPawn in possiblePawn)
                {
                    if (afffectedPawn.Faction == pawn.Faction)
                    {
                        num++;
                    }
                    else
                    {
                        num2++;
                    }
                }

                if (num2 >= 2 && num2 > num)
                {
                    if (allowAIHeatPush || !pawn.psychicEntropy.WouldOverflowEntropy(65f))
                    {
                        return JobMaker.MakeJob(DefOfLocal.CastBerserkPulse, enemyTarget);
                    }
                }
            }

            if (manhunterPulse && pawn.psychicEntropy.CurrentPsyfocus > 0.5 &&
                pawn.Position.InHorDistOf(enemyTarget.Position, 34.9f) && enemyTarget.RaceProps.Animal &&
                pawn.HostileTo(Faction.OfPlayer))
            {
                var possiblePawn = (from Pawn pwn in pawn.Map.mapPawns.SpawnedPawnsInFaction(enemyTarget.Faction)
                    where pwn.Position.InHorDistOf(enemyTarget.Position, 27.9f) && pwn.RaceProps.Animal &&
                          !pwn.InMentalState
                    select pwn).ToList();
                if (possiblePawn.Count >= 3)
                {
                    if (allowAIHeatPush || !pawn.psychicEntropy.WouldOverflowEntropy(50f))
                    {
                        return JobMaker.MakeJob(DefOfLocal.CastManhunterPulse, enemyTarget);
                    }
                }
            }

            if (berserk && pawn.psychicEntropy.CurrentPsyfocus > 0.5 &&
                pawn.Position.InHorDistOf(enemyTarget.Position, 19.9f))
            {
                if (allowAIHeatPush || !pawn.psychicEntropy.WouldOverflowEntropy(40f))
                {
                    return JobMaker.MakeJob(DefOfLocal.CastBerserk, enemyTarget);
                }
            }

            if (vertigoPulse && pawn.psychicEntropy.CurrentPsyfocus > 0.25 && enemyTarget.RaceProps.IsFlesh &&
                pawn.Position.InHorDistOf(enemyTarget.Position, 24.9f) &&
                !enemyTarget.health.hediffSet.HasHediff(DefOfLocal.PsychicVertigo))
            {
                var possiblePawn = (from Pawn pwn in pawn.Map.mapPawns.AllPawns
                    where pwn.Position.InHorDistOf(enemyTarget.Position, 3.9f) & pwn.RaceProps.IsFlesh
                    select pwn).ToList();
                var num3 = 0;
                var num4 = 0;
                foreach (var affectedPawn in possiblePawn)
                {
                    if (affectedPawn.Faction == pawn.Faction)
                    {
                        num3++;
                    }
                    else
                    {
                        num4++;
                    }
                }

                if (num4 >= 2 && num4 > num3 + 1)
                {
                    if (allowAIHeatPush || !pawn.psychicEntropy.WouldOverflowEntropy(30f))
                    {
                        return JobMaker.MakeJob(DefOfLocal.CastVertigoPulse, enemyTarget);
                    }
                }
            }
        }

        if (waterskip && pawn.psychicEntropy.CurrentPsyfocus > 0.015)
        {
            var possiblePawn = (from Pawn pwn in pawn.Map.mapPawns.SpawnedPawnsInFaction(pawn.Faction)
                where pwn.Position.InHorDistOf(pawn.Position, 24.9f) && pwn.HasAttachment(ThingDefOf.Fire)
                select pwn).RandomElementWithFallback();
            if (possiblePawn != null)
            {
                if (allowAIHeatPush || !pawn.psychicEntropy.WouldOverflowEntropy(25f))
                {
                    return JobMaker.MakeJob(DefOfLocal.CastWaterskip, possiblePawn);
                }
            }
        }

        if (invisibility && pawn.psychicEntropy.CurrentPsyfocus > 0.5 &&
            !pawn.health.hediffSet.HasHediff(DefOfLocal.PsychicInvisibility))
        {
            if (allowAIHeatPush || !pawn.psychicEntropy.WouldOverflowEntropy(45f))
            {
                return JobMaker.MakeJob(DefOfLocal.CastInvisibility, pawn);
            }
        }

        if (!enemyTarget.DestroyedOrNull() && pawn.CanReserve(enemyTarget) && !enemyTarget.GetRegion().touchesMapEdge)
        {
            if (beckon && pawn.psychicEntropy.CurrentPsyfocus > 0.25 &&
                pawn.Position.InHorDistOf(enemyTarget.Position, 19.9f) &&
                enemyTarget.CurJobDef != JobDefOf.GotoMindControlled && (enemyTarget.equipment?.Primary == null ||
                                                                         enemyTarget.equipment.Primary.def
                                                                             .IsRangedWeapon))
            {
                if (allowAIHeatPush || !pawn.psychicEntropy.WouldOverflowEntropy(20f))
                {
                    return JobMaker.MakeJob(DefOfLocal.CastBeckon, enemyTarget);
                }
            }

            if (blindingPulse && pawn.psychicEntropy.CurrentPsyfocus > 0.01 &&
                pawn.Position.InHorDistOf(enemyTarget.Position, 24.9f) &&
                !enemyTarget.health.hediffSet.HasHediff(DefOfLocal.PsychicBlindness))
            {
                var possiblePawns = (from Pawn pwn in pawn.Map.mapPawns.AllPawns
                    where pwn.Position.InHorDistOf(enemyTarget.Position, 3.9f)
                    select pwn).ToList();
                var num5 = 0;
                var num6 = 0;
                foreach (var afffectedPawn in possiblePawns)
                {
                    if (afffectedPawn.Faction == pawn.Faction)
                    {
                        num5++;
                    }
                    else
                    {
                        num6++;
                    }
                }

                if (num6 >= 2 && num6 > num5 + 1)
                {
                    if (allowAIHeatPush || !pawn.psychicEntropy.WouldOverflowEntropy(20f))
                    {
                        return JobMaker.MakeJob(DefOfLocal.CastBlindingPulse, enemyTarget);
                    }
                }
            }
        }

        if (smokepop && pawn.psychicEntropy.CurrentPsyfocus > 0.25 && building != null &&
            building.def.building.ai_combatDangerous)
        {
            if (CoverUtility.TotalSurroundingCoverScore(enemyTarget.Position, pawn.Map) < 0.25f &&
                pawn.Position.InHorDistOf(building.Position, 24.9f))
            {
                if (allowAIHeatPush || !pawn.psychicEntropy.WouldOverflowEntropy(30f))
                {
                    return JobMaker.MakeJob(DefOfLocal.CastSmokepop, building);
                }
            }
            else
            {
                if (allowAIHeatPush || !pawn.psychicEntropy.WouldOverflowEntropy(30f))
                {
                    return JobMaker.MakeJob(DefOfLocal.CastSmokepop, pawn);
                }
            }
        }

        if (wallraise && pawn.psychicEntropy.CurrentPsyfocus > 0.25 &&
            CoverUtility.TotalSurroundingCoverScore(pawn.Position, pawn.Map) < 0.5f && pawn.equipment is
            {
                Primary: not null
            } && pawn.equipment.Primary.def.IsRangedWeapon)
        {
            if (allowAIHeatPush || !pawn.psychicEntropy.WouldOverflowEntropy(35f))
            {
                return JobMaker.MakeJob(DefOfLocal.CastWallraise, pawn);
            }
        }

        if (painblock && pawn.psychicEntropy.CurrentPsyfocus > 0.02 &&
            !pawn.health.hediffSet.HasHediff(DefOfLocal.PsychicAnesthesia) &&
            pawn.health.hediffSet.HasTendableHediff())
        {
            if (allowAIHeatPush || !pawn.psychicEntropy.WouldOverflowEntropy(8f))
            {
                return JobMaker.MakeJob(DefOfLocal.CastPainblock, pawn);
            }
        }

        if (focus && pawn.psychicEntropy.CurrentPsyfocus > 0.25 &&
            !pawn.psychicEntropy.WouldOverflowEntropy(15f) &&
            !pawn.health.hediffSet.HasHediff(DefOfLocal.PsychicFocus))
        {
            return JobMaker.MakeJob(DefOfLocal.CastFocus, pawn);
        }

        if (enemyTarget.DestroyedOrNull() || enemyTarget.GetRegion().touchesMapEdge || enemyTarget.DestroyedOrNull())
        {
            return null;
        }

        if (massChaosSkip && pawn.psychicEntropy.CurrentPsyfocus > 0.5 &&
            pawn.Position.InHorDistOf(enemyTarget.Position, 24.9f) &&
            CoverUtility.TotalSurroundingCoverScore(enemyTarget.Position, pawn.Map) >= 0.75f)
        {
            var possiblePawns = (from Pawn pwn in pawn.Map.mapPawns.AllPawns
                where pwn.Position.InHorDistOf(enemyTarget.Position, 7.9f)
                select pwn).ToList();
            var num7 = 0;
            var num8 = 0;
            foreach (var afffectedPawn in possiblePawns)
            {
                if (afffectedPawn.Faction == pawn.Faction)
                {
                    num7++;
                }
                else
                {
                    num8++;
                }
            }

            if (num8 >= 3 && num8 > num7 + 2)
            {
                if (allowAIHeatPush || !pawn.psychicEntropy.WouldOverflowEntropy(40f))
                {
                    return JobMaker.MakeJob(DefOfLocal.CastMassChaosSkip, enemyTarget);
                }
            }
        }

        if (stun && pawn.psychicEntropy.CurrentPsyfocus > 0.01 &&
            pawn.Position.InHorDistOf(enemyTarget.Position, 24.9f) && !enemyTarget.stances.FullBodyBusy)
        {
            if (allowAIHeatPush || !pawn.psychicEntropy.WouldOverflowEntropy(12f))
            {
                return JobMaker.MakeJob(DefOfLocal.CastStun, enemyTarget);
            }
        }

        if (chaosSkip && pawn.psychicEntropy.CurrentPsyfocus > 0.25 &&
            pawn.Position.InHorDistOf(enemyTarget.Position, 24.9f) &&
            CoverUtility.TotalSurroundingCoverScore(enemyTarget.Position, pawn.Map) >= 0.75f &&
            pawn.equipment is { Primary: not null } &&
            pawn.equipment.Primary.def.IsRangedWeapon)
        {
            if (allowAIHeatPush || !pawn.psychicEntropy.WouldOverflowEntropy(18f))
            {
                return JobMaker.MakeJob(DefOfLocal.CastChaosSkip, enemyTarget);
            }
        }

        if (skip && pawn.psychicEntropy.CurrentPsyfocus > 0.25 &&
            pawn.Position.InHorDistOf(enemyTarget.Position, 27.9f) &&
            CoverUtility.TotalSurroundingCoverScore(enemyTarget.Position, pawn.Map) >= 0.75f &&
            pawn.equipment is { Primary: not null } &&
            pawn.equipment.Primary.def.IsRangedWeapon)
        {
            if (allowAIHeatPush || !pawn.psychicEntropy.WouldOverflowEntropy(25f))
            {
                return JobMaker.MakeJob(DefOfLocal.CastSkip, enemyTarget);
            }
        }

        if (burden && pawn.psychicEntropy.CurrentPsyfocus > 0.01 && pawn.psychicEntropy.EntropyValue < 1f &&
            pawn.Position.InHorDistOf(enemyTarget.Position, 29.9f) &&
            !enemyTarget.health.hediffSet.HasHediff(DefOfLocal.PsychicBurden))
        {
            return JobMaker.MakeJob(DefOfLocal.CastBurden, enemyTarget);
        }

        return null;
    }
}