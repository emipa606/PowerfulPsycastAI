using System.Collections.Generic;
using System.Diagnostics;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace PowerfulEmpire;

public class JobDriver_CastSmokepop : JobDriver
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
                GenExplosion.DoExplosion(targetA.Cell, actor.Map, 3.5f, DamageDefOf.Smoke, null, -1, -1f, null, null,
                    null, null, null, 0f, 1, GasType.BlindSmoke);
                actor.psychicEntropy.TryAddEntropy(30f, overLimit: true);
                actor.psychicEntropy.OffsetPsyfocusDirectly(-0.02f);
            },
            defaultCompleteMode = ToilCompleteMode.Instant
        };
    }
}