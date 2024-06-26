using System.Collections.Generic;
using System.Diagnostics;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace PowerfulEmpire;

public class JobDriver_CastInvisibility : JobDriver
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
                var hediff = HediffMaker.MakeHediff(DefOfLocal.PsychicInvisibility, actor);
                var hediffComp_Disappears = hediff.TryGetComp<HediffComp_Disappears>();
                if (hediffComp_Disappears != null)
                {
                    hediffComp_Disappears.ticksToDisappear = 15f.SecondsToTicks();
                }

                actor.health.AddHediff(hediff);
                actor.psychicEntropy.TryAddEntropy(45f, overLimit: true);
                actor.psychicEntropy.OffsetPsyfocusDirectly(-0.03f);
            },
            defaultCompleteMode = ToilCompleteMode.Instant
        };
    }
}