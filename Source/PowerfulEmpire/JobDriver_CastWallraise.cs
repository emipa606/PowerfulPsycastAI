using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace PowerfulEmpire;

public class JobDriver_CastWallraise : JobDriver
{
    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        return true;
    }

    [DebuggerHidden]
    protected override IEnumerable<Toil> MakeNewToils()
    {
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
        this.FailOnDespawnedOrNull(TargetIndex.A);
        yield return new Toil
        {
            initAction = delegate
            {
                if (targetA.HasThing)
                {
                    MoteMaker.MakeAttachedOverlay(targetA.Thing, ThingDefOf.Mote_PsyfocusPulse, Vector3.zero);
                }
                else
                {
                    MoteMaker.MakeStaticMote(targetA.Cell, actor.Map, ThingDefOf.Mote_PsyfocusPulse);
                }

                var psycastPsychicEffect = SoundDefOf.PsycastPsychicEffect;
                psycastPsychicEffect.PlayOneShot(new TargetInfo(targetA.Cell, actor.Map));
                var map = actor.Map;
                LocalTargetInfo target = targetA.Cell.RandomAdjacentCell8Way();
                var list = new List<Thing>();
                list.AddRange(AffectedCells(target).SelectMany(c => from t in c.GetThingList(map)
                    where t.def.category == ThingCategory.Item
                    select t));
                foreach (var item in list)
                {
                    item.DeSpawn();
                }

                foreach (var item2 in AffectedCells(target))
                {
                    GenSpawn.Spawn(ThingDefOf.RaisedRocks, item2, map);
                    GenClamor.DoClamor(actor, item2, 25f, ClamorDefOf.Ability);
                }

                foreach (var item3 in list)
                {
                    var intVec = IntVec3.Invalid;
                    for (var i = 0; i < 9; i++)
                    {
                        var intVec2 = item3.Position + GenRadial.RadialPattern[i];
                        if (!intVec2.InBounds(map) || !intVec2.Walkable(map) ||
                            map.thingGrid.ThingsListAtFast(intVec2).Count > 0)
                        {
                            continue;
                        }

                        intVec = intVec2;
                        break;
                    }

                    if (intVec != IntVec3.Invalid)
                    {
                        GenSpawn.Spawn(item3, intVec, map);
                    }
                    else
                    {
                        GenPlace.TryPlaceThing(item3, item3.Position, map, ThingPlaceMode.Near);
                    }
                }

                actor.psychicEntropy.TryAddEntropy(35f, overLimit: true);
                actor.psychicEntropy.OffsetPsyfocusDirectly(-0.02f);
            },
            defaultCompleteMode = ToilCompleteMode.Instant
        };
        this.FailOnDespawnedOrNull(TargetIndex.A);
    }

    private IEnumerable<IntVec3> AffectedCells(LocalTargetInfo target)
    {
        foreach (var intVec in new List<IntVec2>
                 {
                     IntVec2.FromString("(0, 0)"),
                     IntVec2.FromString("(1, 0)"),
                     IntVec2.FromString("(-1, 0)"),
                     IntVec2.FromString("(0, 1)"),
                     IntVec2.FromString("(0, -1)")
                 })
        {
            yield return target.Cell + new IntVec3(intVec.x, 0, intVec.z);
        }
    }
}