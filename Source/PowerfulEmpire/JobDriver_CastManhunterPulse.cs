using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace PowerfulEmpire;

public class JobDriver_CastManhunterPulse : JobDriver
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
                if (targetA.Thing is Pawn victim &&
                    GenSight.LineOfSight(pawn.Position, victim.Position, pawn.Map, false) &&
                    actor.Position.InHorDistOf(victim.Position, 34.9f))
                {
                    if (targetA.HasThing)
                    {
                        FleckMaker.AttachedOverlay(targetA.Thing, FleckDefOf.PsycastAreaEffect, Vector2.zero);
                    }

                    var psycastPsychicEffect = SoundDefOf.PsycastPsychicEffect;
                    psycastPsychicEffect.PlayOneShot(new TargetInfo(targetA.Cell, actor.Map));
                    var list = (from Pawn pwn in actor.Map.mapPawns.AllPawns
                        where pwn.Position.InHorDistOf(victim.Position, 27.9f) && pwn.RaceProps.Animal
                        select pwn).ToList();
                    foreach (var affectedPawn in list)
                    {
                        if (!affectedPawn.mindState.mentalStateHandler
                                .TryStartMentalState(MentalStateDefOf.Manhunter, null, true))
                        {
                            continue;
                        }

                        float numSeconds =
                            (60f * affectedPawn.GetStatValue(StatDefOf.PsychicSensitivity)).SecondsToTicks();
                        affectedPawn.mindState.mentalStateHandler.CurState.forceRecoverAfterTicks =
                            numSeconds.SecondsToTicks();
                    }

                    actor.psychicEntropy.TryAddEntropy(50f, overLimit: true);
                    actor.psychicEntropy.OffsetPsyfocusDirectly(-0.04f);
                }
                else
                {
                    actor.jobs.EndCurrentJob(JobCondition.Ongoing | JobCondition.Succeeded);
                }
            },
            defaultCompleteMode = ToilCompleteMode.Instant
        };
        this.FailOnDespawnedOrNull(TargetIndex.A);
    }
}