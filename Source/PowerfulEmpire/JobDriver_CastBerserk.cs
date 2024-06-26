using System.Collections.Generic;
using System.Diagnostics;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace PowerfulEmpire;

public class JobDriver_CastBerserk : JobDriver
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
        var ticks = (0.25f * statValue).SecondsToTicks();
        MoteMaker.MakeAttachedOverlay(actor, ThingDefOf.Mote_CastPsycast, Vector2.zero, 1f, ticks);
        yield return Toils_General.Wait(ticks);
        this.FailOnDespawnedOrNull(TargetIndex.A);
        if (targetA.Thing is Pawn victim && GenSight.LineOfSight(pawn.Position, victim.Position, pawn.Map, false) &&
            actor.Position.InHorDistOf(victim.Position, 19.9f) &&
            victim.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, true))
        {
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
                    float numSeconds = (15f * victim.GetStatValue(StatDefOf.PsychicSensitivity)).SecondsToTicks();
                    victim.mindState.mentalStateHandler.CurState.forceRecoverAfterTicks = numSeconds.SecondsToTicks();
                    actor.psychicEntropy.TryAddEntropy(40f, overLimit: true);
                    actor.psychicEntropy.OffsetPsyfocusDirectly(-0.04f);
                },
                defaultCompleteMode = ToilCompleteMode.Instant
            };
        }

        this.FailOnDespawnedOrNull(TargetIndex.A);
    }
}