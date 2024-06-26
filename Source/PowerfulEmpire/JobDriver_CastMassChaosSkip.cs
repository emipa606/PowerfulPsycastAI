using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace PowerfulEmpire;

public class JobDriver_CastMassChaosSkip : JobDriver
{
    private readonly List<IntVec3> cells = [];

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
                if (targetA.Thing is not Pawn victim ||
                    !GenSight.LineOfSight(pawn.Position, victim.Position, pawn.Map, false) ||
                    !actor.Position.InHorDistOf(victim.Position, 24.9f))
                {
                    return;
                }

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
                var list = (from Pawn pwn in actor.Map.mapPawns.AllPawns
                    where pwn.Position.InHorDistOf(victim.Position, 7.9f)
                    select pwn).ToList();
                foreach (var target in list)
                {
                    var destination = GetDestination(target);
                    if (!destination.IsValid)
                    {
                        return;
                    }

                    target.TryGetComp<CompCanBeDormant>()?.WakeUp();
                    GenClamor.DoClamor(pawn, target.Position, 10f, ClamorDefOf.Ability);
                    MoteMaker.MakeStaticMote(target.Position, target.Map, ThingDefOf.Mote_PsyfocusPulse);
                    SoundDefOf.Psycast_Skip_Entry.PlayOneShot(new TargetInfo(target.Position, pawn.Map));
                    target.Position = destination.Cell;
                    var num = Rand.Range(70, 120);
                    target.stances.stunner.StunFor(num, pawn, false);

                    GenClamor.DoClamor(pawn, target.Position, 10f, ClamorDefOf.Ability);
                    MoteMaker.MakeStaticMote(target.Position, target.Map, ThingDefOf.Mote_PsyfocusPulse);
                    SoundDefOf.Psycast_Skip_Exit.PlayOneShot(new TargetInfo(target.Position, pawn.Map));
                    target.Notify_Teleported();
                }

                actor.psychicEntropy.TryAddEntropy(40f, overLimit: true);
                actor.psychicEntropy.OffsetPsyfocusDirectly(-0.03f);
            },
            defaultCompleteMode = ToilCompleteMode.Instant
        };
    }

    public LocalTargetInfo GetDestination(LocalTargetInfo target)
    {
        var map = pawn.Map;
        cells.Clear();
        var num = GenRadial.NumCellsInRadius(24.9f);
        for (var i = 0; i < num; i++)
        {
            var intVec = GenRadial.RadialPattern[i];
            if (!(intVec.DistanceTo(IntVec3.Zero) >= 6.9f))
            {
                continue;
            }

            var intVec2 = target.Cell + intVec;
            if (intVec2.Standable(map) && GenSight.LineOfSight(target.Cell, intVec2, map, false))
            {
                cells.Add(intVec2);
            }
        }

        return cells.Any() ? new LocalTargetInfo(cells.RandomElement()) : LocalTargetInfo.Invalid;
    }
}