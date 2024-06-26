using System.Collections.Generic;
using System.Diagnostics;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace PowerfulEmpire;

public class JobDriver_CastChaosSkip : JobDriver
{
    private readonly List<IntVec3> cells = [];

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
        var ticks = (0.25f * statValue).SecondsToTicks();
        MoteMaker.MakeAttachedOverlay(actor, ThingDefOf.Mote_CastPsycast, Vector2.zero, 1f, ticks);
        yield return Toils_General.Wait(ticks);
        this.FailOnDespawnedOrNull(TargetIndex.A);
        if (targetA.Thing is Pawn victim && GenSight.LineOfSight(pawn.Position, victim.Position, pawn.Map, false) &&
            actor.Position.InHorDistOf(victim.Position, 24.9f))
        {
            yield return new Toil
            {
                initAction = delegate
                {
                    var destination = GetDestination(victim);
                    if (!destination.IsValid)
                    {
                        return;
                    }

                    victim.TryGetComp<CompCanBeDormant>()?.WakeUp();
                    GenClamor.DoClamor(pawn, victim.Position, 10f, ClamorDefOf.Ability);
                    SoundDefOf.Psycast_Skip_Entry.PlayOneShot(new TargetInfo(victim.Position, pawn.Map));
                    victim.Position = destination.Cell;
                    var num = Rand.Range(70, 120);
                    victim.stances.stunner.StunFor(num, pawn, false);
                    GenClamor.DoClamor(pawn, victim.Position, 10f, ClamorDefOf.Ability);
                    SoundDefOf.Psycast_Skip_Exit.PlayOneShot(new TargetInfo(victim.Position, pawn.Map));
                    victim.Notify_Teleported();
                    actor.psychicEntropy.TryAddEntropy(18f, overLimit: true);
                    actor.psychicEntropy.OffsetPsyfocusDirectly(-0.02f);
                },
                defaultCompleteMode = ToilCompleteMode.Instant
            };
        }

        this.FailOnDespawnedOrNull(TargetIndex.A);
    }

    public LocalTargetInfo GetDestination(LocalTargetInfo target)
    {
        var map = pawn.Map;
        cells.Clear();
        var num = GenRadial.NumCellsInRadius(24.9f);
        for (var i = 0; i < num; i++)
        {
            var intVec = GenRadial.RadialPattern[i];
            if (!(intVec.DistanceTo(IntVec3.Zero) >= 6.9))
            {
                continue;
            }

            var intVec2 = target.Cell + intVec;
            if (intVec2.Standable(map))
            {
                cells.Add(intVec2);
            }
        }

        return cells.Any() ? new LocalTargetInfo(cells.RandomElement()) : LocalTargetInfo.Invalid;
    }
}