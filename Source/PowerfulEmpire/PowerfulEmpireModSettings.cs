using Verse;

namespace PowerfulEmpire;

public class PowerfulEmpireModSettings : ModSettings
{
    public bool repAllowAIHeatPush;

    public bool repAllowBeckon = true;

    public bool repAllowBerserk;

    public bool repAllowBerserkPulse;

    public bool repAllowBlindingPulse = true;

    public bool repAllowBurden = true;

    public bool repAllowChaosSkip = true;

    public bool repAllowFocus = true;

    public bool repAllowIdentifierHat = true;

    public bool repAllowInvisibility = true;

    public bool repAllowManhunterPulse;

    public bool repAllowMassChaosSkip = true;

    public bool repAllowPainblock = true;

    public bool repAllowSkip = true;

    public bool repAllowSmokepop = true;

    public bool repAllowStun = true;

    public bool repAllowVertigoPulse = true;

    public bool repAllowWallraise = true;

    public bool repAllowWaterskip = true;

    public bool repGivePsycastToAll = true;

    public int repMaxPsycastLvAllowed = 6;

    public int repPsycastGainChanceIndustrial;

    public int repPsycastGainChanceMedieval;

    public int repPsycastGainChanceNeolithic = 10;

    public int repPsycastGainChanceSpacer;

    public int repPsycastGainChanceUltra = 20;

    public int repPsycastScaleMonth = 2;

    public override void ExposeData()
    {
        Scribe_Values.Look(ref repAllowAIHeatPush, "repAllowAIHeatPush");
        Scribe_Values.Look(ref repGivePsycastToAll, "repGivePsycastToAll", true);
        Scribe_Values.Look(ref repAllowIdentifierHat, "repAllowIdentifierHat", true);
        Scribe_Values.Look(ref repPsycastGainChanceNeolithic, "repPsycastGainChanceNeolithic", 10);
        Scribe_Values.Look(ref repPsycastGainChanceMedieval, "repPsycastGainChanceMedieval");
        Scribe_Values.Look(ref repPsycastGainChanceIndustrial, "repPsycastGainChanceIndustrial");
        Scribe_Values.Look(ref repPsycastGainChanceSpacer, "repPsycastGainChanceSpacer");
        Scribe_Values.Look(ref repPsycastGainChanceUltra, "repPsycastGainChanceUltra", 20);
        Scribe_Values.Look(ref repMaxPsycastLvAllowed, "repMaxPsycastLvAllowed", 6);
        Scribe_Values.Look(ref repPsycastScaleMonth, "repPsycastScaleMonth", 2);
        Scribe_Values.Look(ref repAllowBerserkPulse, "repAllowBerserkPulse");
        Scribe_Values.Look(ref repAllowManhunterPulse, "repAllowManhunterPulse");
        Scribe_Values.Look(ref repAllowBerserk, "repAllowBerserk");
        Scribe_Values.Look(ref repAllowVertigoPulse, "repAllowVertigoPulse", true);
        Scribe_Values.Look(ref repAllowInvisibility, "repAllowInvisibility", true);
        Scribe_Values.Look(ref repAllowBeckon, "repAllowBeckon", true);
        Scribe_Values.Look(ref repAllowBlindingPulse, "repAllowBlindingPulse", true);
        Scribe_Values.Look(ref repAllowSmokepop, "repAllowSmokepop", true);
        Scribe_Values.Look(ref repAllowFocus, "repAllowFocus", true);
        Scribe_Values.Look(ref repAllowWallraise, "repAllowWallraise", true);
        Scribe_Values.Look(ref repAllowPainblock, "repAllowPainblock", true);
        Scribe_Values.Look(ref repAllowStun, "repAllowStun", true);
        Scribe_Values.Look(ref repAllowMassChaosSkip, "repAllowMassChaosSkip", true);
        Scribe_Values.Look(ref repAllowChaosSkip, "repAllowChaosSkip", true);
        Scribe_Values.Look(ref repAllowSkip, "repAllowSkip", true);
        Scribe_Values.Look(ref repAllowBurden, "repAllowBurden", true);
        Scribe_Values.Look(ref repAllowWaterskip, "repAllowWaterskip", true);
        base.ExposeData();
    }

    public void Reset()
    {
        repAllowAIHeatPush = false;
        repGivePsycastToAll = true;
        repAllowIdentifierHat = true;
        repPsycastGainChanceNeolithic = 10;
        repPsycastGainChanceMedieval = 0;
        repPsycastGainChanceIndustrial = 0;
        repPsycastGainChanceSpacer = 0;
        repPsycastGainChanceUltra = 20;
        repMaxPsycastLvAllowed = 6;
        repPsycastScaleMonth = 2;
        repAllowBerserkPulse = false;
        repAllowManhunterPulse = false;
        repAllowBerserk = false;
        repAllowVertigoPulse = true;
        repAllowInvisibility = true;
        repAllowBeckon = true;
        repAllowBlindingPulse = true;
        repAllowSmokepop = true;
        repAllowFocus = true;
        repAllowWallraise = true;
        repAllowPainblock = true;
        repAllowStun = true;
        repAllowMassChaosSkip = true;
        repAllowChaosSkip = true;
        repAllowSkip = true;
        repAllowBurden = true;
        repAllowWaterskip = true;
    }
}