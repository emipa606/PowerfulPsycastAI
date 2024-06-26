using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace PowerfulEmpire;

public class PawnGroupKindWorker_Psycast : PawnGroupKindWorker
{
    public static bool givePsycastToAll;

    public static bool allowIdentifierHat;

    public static int psycastGainChanceNeolithic;

    public static int psycastGainChanceMedieval;

    public static int psycastGainChanceIndustrial;

    public static int psycastGainChanceSpacer;

    public static int psycastGainChanceUltra;

    public static int maxPsycastLvAllowed;

    public static int psycastScaleMonth;

    public override float MinPointsToGenerateAnything(PawnGroupMaker groupMaker, FactionDef faction,
        PawnGroupMakerParms parms = null)
    {
        var num = parms is { points: > 0f } ? parms.points : 100000f;
        var num2 = float.MaxValue;
        var options = PawnGroupMakerUtility.GetOptions(parms, faction, groupMaker.options, num, num, float.MaxValue);
        foreach (var item in options)
        {
            if (item.Option.kind.isFighter && item.Cost < num2 &&
                PawnGroupMakerUtility.PawnGenOptionValid(item.Option, parms))
            {
                num2 = item.Cost;
            }
        }

        if (num2 != float.MaxValue)
        {
            return num2;
        }

        foreach (var item2 in options)
        {
            if (item2.Cost < num2 && PawnGroupMakerUtility.PawnGenOptionValid(item2.Option, parms))
            {
                num2 = item2.Cost;
            }
        }

        return num2;
    }

    public override bool CanGenerateFrom(PawnGroupMakerParms parms, PawnGroupMaker groupMaker)
    {
        return base.CanGenerateFrom(parms, groupMaker) &&
               PawnGroupMakerUtility.AnyOptions(parms, parms.faction?.def, groupMaker.options, parms.points);
    }

    protected override void GeneratePawns(PawnGroupMakerParms parms, PawnGroupMaker groupMaker, List<Pawn> outPawns,
        bool errorOnZeroResults = true)
    {
        if (!CanGenerateFrom(parms, groupMaker))
        {
            if (errorOnZeroResults)
            {
                Log.Error(string.Concat("Cannot generate pawns for ", parms.faction, " with ", parms.points,
                    ". Defaulting to a single random cheap group."));
            }

            return;
        }

        var allowFood = parms.raidStrategy == null || parms.raidStrategy.pawnsCanBringFood ||
                        parms.faction != null && !parms.faction.HostileTo(Faction.OfPlayer);
        Predicate<Pawn> predicate = parms.raidStrategy != null
            ? p => parms.raidStrategy.Worker.CanUsePawn(parms.points, p, outPawns)
            : null;
        var oneIsDowned = false;
        foreach (var item in
                 PawnGroupMakerUtility.ChoosePawnGenOptionsByPoints(parms.points, groupMaker.options, parms))
        {
            var kind = item.Option.kind;
            var faction = parms.faction;
            var ideo = parms.ideo;
            var xenotype = item.Xenotype;
            var request = new PawnGenerationRequest(kind, faction, PawnGenerationContext.NonPlayer, parms.tile, false,
                false, false, true, true, 1f, false, true, true, allowFood, true, parms.inhabitants, false, false,
                false, 0f,
                0f, null, 1f, null, predicate, null, null, null, null, null, null, null, null, null, ideo, false, false,
                false, false, null, null, xenotype);
            if (parms.raidAgeRestriction != null && parms.raidAgeRestriction.Worker.ShouldApplyToKind(item.Option.kind))
            {
                request.BiologicalAgeRange = parms.raidAgeRestriction.ageRange;
                request.AllowedDevelopmentalStages = parms.raidAgeRestriction.developmentStage;
            }

            if (item.Option.kind.pawnGroupDevelopmentStage.HasValue)
            {
                request.AllowedDevelopmentalStages = item.Option.kind.pawnGroupDevelopmentStage.Value;
            }

            if (!Find.Storyteller.difficulty.childRaidersAllowed && parms.faction != null &&
                parms.faction.HostileTo(Faction.OfPlayer))
            {
                request.AllowedDevelopmentalStages = DevelopmentalStage.Adult;
            }

            var pawn = PawnGenerator.GeneratePawn(request);
            if (parms.forceOneDowned && !oneIsDowned)
            {
                pawn.health.forceDowned = true;
                if (pawn.guest != null)
                {
                    pawn.guest.Recruitable = true;
                }

                pawn.mindState.canFleeIndividual = false;
                oneIsDowned = true;
            }

            if (!givePsycastToAll || !pawn.RaceProps.Humanlike ||
                pawn.health.hediffSet.HasHediff(HediffDefOf.PsychicAmplifier) ||
                !(pawn.GetStatValue(StatDefOf.PsychicSensitivity) >= 1f) || pawn.Faction == Faction.OfPlayer ||
                pawn.HostFaction == Faction.OfPlayer)
            {
                outPawns.Add(pawn);
                continue;
            }

            if (pawn.Faction.def.techLevel == TechLevel.Neolithic &&
                Rand.RangeInclusive(1, 100) > psycastGainChanceNeolithic ||
                pawn.Faction.def.techLevel == TechLevel.Medieval &&
                Rand.RangeInclusive(1, 100) > psycastGainChanceMedieval ||
                pawn.Faction.def.techLevel == TechLevel.Industrial &&
                Rand.RangeInclusive(1, 100) > psycastGainChanceIndustrial ||
                pawn.Faction.def.techLevel == TechLevel.Spacer &&
                Rand.RangeInclusive(1, 100) > psycastGainChanceSpacer ||
                pawn.Faction.def.techLevel == TechLevel.Ultra && Rand.RangeInclusive(1, 100) > psycastGainChanceUltra ||
                pawn.Faction.def.techLevel == TechLevel.Animal || pawn.Faction.def.techLevel == TechLevel.Archotech)
            {
                outPawns.Add(pawn);
                continue;
            }

            var bodyPartRecord = pawn.RaceProps.body.GetPartsWithDef(DefOfLocal.Brain).FirstOrFallback();
            if (bodyPartRecord == null)
            {
                outPawns.Add(pawn);
                continue;
            }

            if (psycastScaleMonth != 0)
            {
                var maxInclusive =
                    Mathf.Min((int)Mathf.Floor(Find.TickManager.TicksGame / (float)psycastScaleMonth),
                        maxPsycastLvAllowed);
                var levelOffset = Rand.RangeInclusive(0, maxInclusive);
                pawn.health.AddHediff(HediffDefOf.PsychicAmplifier, bodyPartRecord);
                var firstHediffOfDef =
                    pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.PsychicAmplifier);
                ((Hediff_Psylink)firstHediffOfDef).ChangeLevel(levelOffset);
            }
            else
            {
                var levelOffset2 = Rand.RangeInclusive(0, maxPsycastLvAllowed);
                pawn.health.AddHediff(HediffDefOf.PsychicAmplifier, bodyPartRecord);
                var firstHediffOfDef2 =
                    pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.PsychicAmplifier);
                ((Hediff_Psylink)firstHediffOfDef2).ChangeLevel(levelOffset2);
            }

            if (allowIdentifierHat)
            {
                if (pawn.Faction.def.techLevel < TechLevel.Spacer && ThingDef.Named("Apparel_PsyfocusHelmet") != null)
                {
                    var apparel = (Apparel)ThingMaker.MakeThing(ThingDef.Named("Apparel_PsyfocusHelmet"));
                    apparel.SetColor(Color.magenta);
                    PawnGenerator.PostProcessGeneratedGear(apparel, pawn);
                    if (ApparelUtility.HasPartsToWear(pawn, apparel.def))
                    {
                        pawn.apparel.Wear(apparel, false);
                    }
                }

                if (pawn.Faction.def.techLevel > TechLevel.Industrial &&
                    ThingDef.Named("Apparel_ArmorHelmetReconPrestige") != null)
                {
                    var apparel4 =
                        (Apparel)ThingMaker.MakeThing(ThingDef.Named("Apparel_ArmorHelmetReconPrestige"));
                    apparel4.SetColor(Color.yellow);
                    PawnGenerator.PostProcessGeneratedGear(apparel4, pawn);
                    if (ApparelUtility.HasPartsToWear(pawn, apparel4.def))
                    {
                        pawn.apparel.Wear(apparel4, false);
                    }
                }
            }

            outPawns.Add(pawn);
        }
    }

    public override IEnumerable<PawnKindDef> GeneratePawnKindsExample(PawnGroupMakerParms parms,
        PawnGroupMaker groupMaker)
    {
        foreach (var item in
                 PawnGroupMakerUtility.ChoosePawnGenOptionsByPoints(parms.points, groupMaker.options, parms))
        {
            yield return item.Option.kind;
        }
    }
}