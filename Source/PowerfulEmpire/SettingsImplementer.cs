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
        PawnGroupKindWorker_Psycast.givePsycastToAll = PowerfulEmpireSettings.Instance.settings.repGivePsycastToAll;

        PawnGroupKindWorker_Psycast.allowIdentifierHat = PowerfulEmpireSettings.Instance.settings.repAllowIdentifierHat;
        PawnGroupKindWorker_Psycast.psycastGainChanceNeolithic =
            PowerfulEmpireSettings.Instance.settings.repPsycastGainChanceNeolithic;
        PawnGroupKindWorker_Psycast.psycastGainChanceMedieval =
            PowerfulEmpireSettings.Instance.settings.repPsycastGainChanceMedieval;
        PawnGroupKindWorker_Psycast.psycastGainChanceIndustrial =
            PowerfulEmpireSettings.Instance.settings.repPsycastGainChanceIndustrial;
        PawnGroupKindWorker_Psycast.psycastGainChanceSpacer =
            PowerfulEmpireSettings.Instance.settings.repPsycastGainChanceSpacer;
        PawnGroupKindWorker_Psycast.psycastGainChanceUltra =
            PowerfulEmpireSettings.Instance.settings.repPsycastGainChanceUltra;
        PawnGroupKindWorker_Psycast.maxPsycastLvAllowed =
            PowerfulEmpireSettings.Instance.settings.repMaxPsycastLvAllowed - 1;
        PawnGroupKindWorker_Psycast.psycastScaleMonth =
            PowerfulEmpireSettings.Instance.settings.repPsycastScaleMonth * 900000;

        JobGiver_AIPsycast.allowAIHeatPush = PowerfulEmpireSettings.Instance.settings.repAllowAIHeatPush;

        JobGiver_AIPsycast.allowBeckon = PowerfulEmpireSettings.Instance.settings.repAllowBeckon;
        DefOfLocal.Beckon.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowBeckon;
        DefOfLocal.Beckon.ai_IsOffensive = PowerfulEmpireSettings.Instance.settings.repAllowBeckon;

        JobGiver_AIPsycast.allowBerserk = PowerfulEmpireSettings.Instance.settings.repAllowBerserk;
        DefOfLocal.Berserk.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowBerserk;
        DefOfLocal.Berserk.ai_IsOffensive = PowerfulEmpireSettings.Instance.settings.repAllowBerserk;

        JobGiver_AIPsycast.allowBerserkPulse = PowerfulEmpireSettings.Instance.settings.repAllowBerserkPulse;
        DefOfLocal.BerserkPulse.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowBerserkPulse;
        DefOfLocal.BerserkPulse.ai_IsOffensive = PowerfulEmpireSettings.Instance.settings.repAllowBerserkPulse;
        DefOfLocal.BerserkPulse.ai_SearchAOEForTargets = PowerfulEmpireSettings.Instance.settings.repAllowBerserkPulse;

        JobGiver_AIPsycast.allowBlindingPulse = PowerfulEmpireSettings.Instance.settings.repAllowBlindingPulse;
        DefOfLocal.BlindingPulse.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowBlindingPulse;
        DefOfLocal.BlindingPulse.ai_IsOffensive = PowerfulEmpireSettings.Instance.settings.repAllowBlindingPulse;
        DefOfLocal.BlindingPulse.ai_SearchAOEForTargets =
            PowerfulEmpireSettings.Instance.settings.repAllowBlindingPulse;
        DefOfLocal.BlindingPulse.ai_IsIncendiary = PowerfulEmpireSettings.Instance.settings.repAllowBlindingPulse;

        JobGiver_AIPsycast.allowBurden = PowerfulEmpireSettings.Instance.settings.repAllowBurden;
        DefOfLocal.Burden.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowBurden;
        DefOfLocal.Burden.ai_IsOffensive = PowerfulEmpireSettings.Instance.settings.repAllowBurden;

        JobGiver_AIPsycast.allowChaosSkip = PowerfulEmpireSettings.Instance.settings.repAllowChaosSkip;
        DefOfLocal.ChaosSkip.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowChaosSkip;
        DefOfLocal.ChaosSkip.ai_IsOffensive = PowerfulEmpireSettings.Instance.settings.repAllowChaosSkip;

        JobGiver_AIPsycast.allowFocus = PowerfulEmpireSettings.Instance.settings.repAllowFocus;
        DefOfLocal.Focus.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowFocus;

        JobGiver_AIPsycast.allowInvisibility = PowerfulEmpireSettings.Instance.settings.repAllowInvisibility;
        DefOfLocal.Invisibility.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowInvisibility;

        JobGiver_AIPsycast.allowManhunterPulse = PowerfulEmpireSettings.Instance.settings.repAllowManhunterPulse;
        DefOfLocal.ManhunterPulse.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowManhunterPulse;
        DefOfLocal.ManhunterPulse.ai_IsOffensive = PowerfulEmpireSettings.Instance.settings.repAllowManhunterPulse;
        DefOfLocal.ManhunterPulse.ai_SearchAOEForTargets =
            PowerfulEmpireSettings.Instance.settings.repAllowManhunterPulse;

        JobGiver_AIPsycast.allowPainblock = PowerfulEmpireSettings.Instance.settings.repAllowPainblock;
        DefOfLocal.Painblock.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowPainblock;

        JobGiver_AIPsycast.allowSkip = PowerfulEmpireSettings.Instance.settings.repAllowSkip;
        DefOfLocal.Skip.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowSkip;

        JobGiver_AIPsycast.allowSmokepop = PowerfulEmpireSettings.Instance.settings.repAllowSmokepop;
        DefOfLocal.Smokepop.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowSmokepop;

        JobGiver_AIPsycast.allowStun = PowerfulEmpireSettings.Instance.settings.repAllowStun;
        DefOfLocal.Stun.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowStun;
        DefOfLocal.Stun.ai_IsOffensive = PowerfulEmpireSettings.Instance.settings.repAllowStun;

        JobGiver_AIPsycast.allowVertigoPulse = PowerfulEmpireSettings.Instance.settings.repAllowVertigoPulse;
        DefOfLocal.VertigoPulse.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowVertigoPulse;
        DefOfLocal.VertigoPulse.ai_IsOffensive = PowerfulEmpireSettings.Instance.settings.repAllowVertigoPulse;
        DefOfLocal.VertigoPulse.ai_SearchAOEForTargets = PowerfulEmpireSettings.Instance.settings.repAllowVertigoPulse;
        DefOfLocal.VertigoPulse.ai_IsIncendiary = PowerfulEmpireSettings.Instance.settings.repAllowVertigoPulse;

        JobGiver_AIPsycast.allowWallraise = PowerfulEmpireSettings.Instance.settings.repAllowWallraise;
        DefOfLocal.Wallraise.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowWallraise;

        JobGiver_AIPsycast.allowWaterskip = PowerfulEmpireSettings.Instance.settings.repAllowWaterskip;
        DefOfLocal.Waterskip.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowWaterskip;

        JobGiver_AIPsycast.allowMassChaosSkip = PowerfulEmpireSettings.Instance.settings.repAllowMassChaosSkip;
        DefOfLocal.MassChaosSkip.aiCanUse = PowerfulEmpireSettings.Instance.settings.repAllowMassChaosSkip;
        DefOfLocal.MassChaosSkip.ai_IsOffensive = PowerfulEmpireSettings.Instance.settings.repAllowMassChaosSkip;
        DefOfLocal.MassChaosSkip.ai_SearchAOEForTargets =
            PowerfulEmpireSettings.Instance.settings.repAllowMassChaosSkip;
        DefOfLocal.MassChaosSkip.ai_IsIncendiary = PowerfulEmpireSettings.Instance.settings.repAllowMassChaosSkip;
    }
}