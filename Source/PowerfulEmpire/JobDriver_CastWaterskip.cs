using System.Collections.Generic;
using System.Diagnostics;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace PowerfulEmpire;

public class JobDriver_CastWaterskip : JobDriver
{
    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        return true;
    }

    [DebuggerHidden]
    protected override IEnumerable<Toil> MakeNewToils()
    {
        this.FailOnDespawnedOrNull(TargetIndex.A);
        var actor = pawn;
        var targetA = job.targetA;
        yield return new Toil
        {
            initAction = delegate { actor.pather.StopDead(); },
            defaultCompleteMode = ToilCompleteMode.Instant
        };
        var statValue = actor.GetStatValue(StatDefOf.AimingDelayFactor);
        var ticks = (1f * statValue).SecondsToTicks();
        MoteMaker.MakeAttachedOverlay(actor, ThingDefOf.Mote_CastPsycast, Vector2.zero, 1f, ticks);
        yield return Toils_General.Wait(ticks);
        yield return new Toil
        {
            initAction = delegate
            {
                var map = actor.Map;
                foreach (var item in AffectedCells(targetA, map))
                {
                    var thingList = item.GetThingList(map);
                    for (var num = thingList.Count - 1; num >= 0; num--)
                    {
                        if (thingList[num] is Fire)
                        {
                            thingList[num].Destroy();
                        }
                    }

                    if (!item.Filled(map))
                    {
                        FilthMaker.TryMakeFilth(item, map, ThingDefOf.Filth_Water);
                    }

                    var dataStatic = FleckMaker.GetDataStatic(item.ToVector3Shifted(), map,
                        FleckDefOf.WaterskipSplashParticles);
                    dataStatic.rotationRate = Rand.Range(-30, 30);
                    dataStatic.rotation = 90 * Rand.RangeInclusive(0, 3);
                    map.flecks.CreateFleck(dataStatic);
                    CompAbilityEffect_Teleport.SendSkipUsedSignal(item, actor);
                }
            },
            defaultCompleteMode = ToilCompleteMode.Instant
        };
    }

    private IEnumerable<IntVec3> AffectedCells(LocalTargetInfo target, Map map)
    {
        foreach (var item in GenRadial.RadialCellsAround(target.Cell, 1.9f, true))
        {
            if (item.InBounds(map) && GenSight.LineOfSightToEdges(target.Cell, item, map, true))
            {
                yield return item;
            }
        }
    }
}