using Verse;

namespace PowerfulEmpire;

[StaticConstructorOnStartup]
public class SettingsImplementer
{
    static SettingsImplementer()
    {
        ImplementSettings();
    }

    public static void ImplementSettings()
    {
        Log.Message("[PowerfulPsycastAI]: Updating ability settings");
        PawnGroupKindWorker_Psycast.givePsycastToAll = PowerfulEmpireSettings.Instance.repSettings.repGivePsycastToAll;

        PawnGroupKindWorker_Psycast.allowIdentifierHat =
            PowerfulEmpireSettings.Instance.repSettings.repAllowIdentifierHat;
        PawnGroupKindWorker_Psycast.psycastGainChanceNeolithic =
            PowerfulEmpireSettings.Instance.repSettings.repPsycastGainChanceNeolithic;
        PawnGroupKindWorker_Psycast.psycastGainChanceMedieval =
            PowerfulEmpireSettings.Instance.repSettings.repPsycastGainChanceMedieval;
        PawnGroupKindWorker_Psycast.psycastGainChanceIndustrial =
            PowerfulEmpireSettings.Instance.repSettings.repPsycastGainChanceIndustrial;
        PawnGroupKindWorker_Psycast.psycastGainChanceSpacer =
            PowerfulEmpireSettings.Instance.repSettings.repPsycastGainChanceSpacer;
        PawnGroupKindWorker_Psycast.psycastGainChanceUltra =
            PowerfulEmpireSettings.Instance.repSettings.repPsycastGainChanceUltra;
        PawnGroupKindWorker_Psycast.maxPsycastLvAllowed =
            PowerfulEmpireSettings.Instance.repSettings.repMaxPsycastLvAllowed - 1;
        PawnGroupKindWorker_Psycast.psycastScaleMonth =
            PowerfulEmpireSettings.Instance.repSettings.repPsycastScaleMonth * 900000;

        JobGiver_AIPsycast.allowAIHeatPush = PowerfulEmpireSettings.Instance.repSettings.repAllowAIHeatPush;

        JobGiver_AIPsycast.allowBeckon = PowerfulEmpireSettings.Instance.repSettings.repAllowBeckon;
        DefOfLocal.Beckon.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowBeckon;
        DefOfLocal.Beckon.ai_IsOffensive = PowerfulEmpireSettings.Instance.repSettings.repAllowBeckon;

        JobGiver_AIPsycast.allowBerserk = PowerfulEmpireSettings.Instance.repSettings.repAllowBerserk;
        DefOfLocal.Berserk.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowBerserk;
        DefOfLocal.Berserk.ai_IsOffensive = PowerfulEmpireSettings.Instance.repSettings.repAllowBerserk;

        JobGiver_AIPsycast.allowBerserkPulse = PowerfulEmpireSettings.Instance.repSettings.repAllowBerserkPulse;
        DefOfLocal.BerserkPulse.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowBerserkPulse;
        DefOfLocal.BerserkPulse.ai_IsOffensive = PowerfulEmpireSettings.Instance.repSettings.repAllowBerserkPulse;
        DefOfLocal.BerserkPulse.ai_SearchAOEForTargets =
            PowerfulEmpireSettings.Instance.repSettings.repAllowBerserkPulse;

        JobGiver_AIPsycast.allowBlindingPulse = PowerfulEmpireSettings.Instance.repSettings.repAllowBlindingPulse;
        DefOfLocal.BlindingPulse.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowBlindingPulse;
        DefOfLocal.BlindingPulse.ai_IsOffensive = PowerfulEmpireSettings.Instance.repSettings.repAllowBlindingPulse;
        DefOfLocal.BlindingPulse.ai_SearchAOEForTargets =
            PowerfulEmpireSettings.Instance.repSettings.repAllowBlindingPulse;
        DefOfLocal.BlindingPulse.ai_IsIncendiary = PowerfulEmpireSettings.Instance.repSettings.repAllowBlindingPulse;

        JobGiver_AIPsycast.allowBurden = PowerfulEmpireSettings.Instance.repSettings.repAllowBurden;
        DefOfLocal.Burden.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowBurden;
        DefOfLocal.Burden.ai_IsOffensive = PowerfulEmpireSettings.Instance.repSettings.repAllowBurden;

        JobGiver_AIPsycast.allowChaosSkip = PowerfulEmpireSettings.Instance.repSettings.repAllowChaosSkip;
        DefOfLocal.ChaosSkip.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowChaosSkip;
        DefOfLocal.ChaosSkip.ai_IsOffensive = PowerfulEmpireSettings.Instance.repSettings.repAllowChaosSkip;

        JobGiver_AIPsycast.allowFocus = PowerfulEmpireSettings.Instance.repSettings.repAllowFocus;
        DefOfLocal.Focus.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowFocus;

        JobGiver_AIPsycast.allowInvisibility = PowerfulEmpireSettings.Instance.repSettings.repAllowInvisibility;
        DefOfLocal.Invisibility.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowInvisibility;

        JobGiver_AIPsycast.allowManhunterPulse = PowerfulEmpireSettings.Instance.repSettings.repAllowManhunterPulse;
        DefOfLocal.ManhunterPulse.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowManhunterPulse;
        DefOfLocal.ManhunterPulse.ai_IsOffensive = PowerfulEmpireSettings.Instance.repSettings.repAllowManhunterPulse;
        DefOfLocal.ManhunterPulse.ai_SearchAOEForTargets =
            PowerfulEmpireSettings.Instance.repSettings.repAllowManhunterPulse;

        JobGiver_AIPsycast.allowPainblock = PowerfulEmpireSettings.Instance.repSettings.repAllowPainblock;
        DefOfLocal.Painblock.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowPainblock;

        JobGiver_AIPsycast.allowSkip = PowerfulEmpireSettings.Instance.repSettings.repAllowSkip;
        DefOfLocal.Skip.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowSkip;

        JobGiver_AIPsycast.allowSmokepop = PowerfulEmpireSettings.Instance.repSettings.repAllowSmokepop;
        DefOfLocal.Smokepop.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowSmokepop;

        JobGiver_AIPsycast.allowStun = PowerfulEmpireSettings.Instance.repSettings.repAllowStun;
        DefOfLocal.Stun.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowStun;
        DefOfLocal.Stun.ai_IsOffensive = PowerfulEmpireSettings.Instance.repSettings.repAllowStun;

        JobGiver_AIPsycast.allowVertigoPulse = PowerfulEmpireSettings.Instance.repSettings.repAllowVertigoPulse;
        DefOfLocal.VertigoPulse.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowVertigoPulse;
        DefOfLocal.VertigoPulse.ai_IsOffensive = PowerfulEmpireSettings.Instance.repSettings.repAllowVertigoPulse;
        DefOfLocal.VertigoPulse.ai_SearchAOEForTargets =
            PowerfulEmpireSettings.Instance.repSettings.repAllowVertigoPulse;
        DefOfLocal.VertigoPulse.ai_IsIncendiary = PowerfulEmpireSettings.Instance.repSettings.repAllowVertigoPulse;

        JobGiver_AIPsycast.allowWallraise = PowerfulEmpireSettings.Instance.repSettings.repAllowWallraise;
        DefOfLocal.Wallraise.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowWallraise;

        JobGiver_AIPsycast.allowWaterskip = PowerfulEmpireSettings.Instance.repSettings.repAllowWaterskip;
        DefOfLocal.Waterskip.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowWaterskip;

        JobGiver_AIPsycast.allowMassChaosSkip = PowerfulEmpireSettings.Instance.repSettings.repAllowMassChaosSkip;
        DefOfLocal.MassChaosSkip.aiCanUse = PowerfulEmpireSettings.Instance.repSettings.repAllowMassChaosSkip;
        DefOfLocal.MassChaosSkip.ai_IsOffensive = PowerfulEmpireSettings.Instance.repSettings.repAllowMassChaosSkip;
        DefOfLocal.MassChaosSkip.ai_SearchAOEForTargets =
            PowerfulEmpireSettings.Instance.repSettings.repAllowMassChaosSkip;
        DefOfLocal.MassChaosSkip.ai_IsIncendiary = PowerfulEmpireSettings.Instance.repSettings.repAllowMassChaosSkip;
    }
}