using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace PowerfulEmpire;

public class JobDriver_CastBerserkPulse : JobDriver
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
        if (targetA.Thing is Pawn victim && GenSight.LineOfSight(pawn.Position, victim.Position, pawn.Map, false) &&
            actor.Position.InHorDistOf(victim.Position, 14.9f))
        {
            yield return new Toil
            {
                initAction = delegate
                {
                    if (targetA.HasThing)
                    {
                        FleckMaker.AttachedOverlay(targetA.Thing, FleckDefOf.PsycastAreaEffect, Vector2.zero);
                    }

                    var psycastPsychicEffect = SoundDefOf.PsycastPsychicEffect;
                    psycastPsychicEffect.PlayOneShot(new TargetInfo(targetA.Cell, actor.Map));
                    var list = (from Pawn pwn in actor.Map.mapPawns.AllPawns
                        where pwn.Position.InHorDistOf(victim.Position, 2.9f)
                        select pwn).ToList();
                    foreach (var affectedPawn in list)
                    {
                        if (!affectedPawn.mindState.mentalStateHandler
                                .TryStartMentalState(MentalStateDefOf.Berserk, null, true))
                        {
                            continue;
                        }

                        float numSeconds =
                            (10f * affectedPawn.GetStatValue(StatDefOf.PsychicSensitivity)).SecondsToTicks();
                        affectedPawn.mindState.mentalStateHandler.CurState.forceRecoverAfterTicks =
                            numSeconds.SecondsToTicks();
                    }

                    pawn.psychicEntropy.TryAddEntropy(65f, overLimit: true);
                    pawn.psychicEntropy.OffsetPsyfocusDirectly(-0.06f);
                },
                defaultCompleteMode = ToilCompleteMode.Instant
            };
        }

        this.FailOnDespawnedOrNull(TargetIndex.A);
    }
}