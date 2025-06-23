using Mlie;
using UnityEngine;
using Verse;

namespace PowerfulEmpire;

public class PowerfulEmpireSettings : Mod
{
    private static Vector2 scrollPosition;
    private static string currentVersion;
    public static PowerfulEmpireSettings Instance;
    public readonly PowerfulEmpireModSettings repSettings;

    public PowerfulEmpireSettings(ModContentPack content)
        : base(content)
    {
        repSettings = GetSettings<PowerfulEmpireModSettings>();
        currentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
        Instance = this;
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        var rect = new Rect(0f, 0f, inRect.width - 16f, inRect.height + 400f);
        var listingStandard = new Listing_Standard();
        Widgets.BeginScrollView(inRect, ref scrollPosition, rect);
        listingStandard.Begin(rect);
        if (listingStandard.ButtonText("Reset".Translate()))
        {
            repSettings.Reset();
        }

        listingStandard.CheckboxLabeled("RepAllowAIHeatPushExp".Translate(), ref repSettings.repAllowAIHeatPush);
        listingStandard.Gap(4f);
        listingStandard.CheckboxLabeled("RepGivePsycastToAllExp".Translate(), ref repSettings.repGivePsycastToAll);
        listingStandard.Gap(4f);
        listingStandard.CheckboxLabeled("RepAllowIdentifierHatExp".Translate(), ref repSettings.repAllowIdentifierHat);
        listingStandard.Gap(4f);
        listingStandard.Label("RepMaxPsycastLvAllowedExp".Translate(repSettings.repMaxPsycastLvAllowed));
        listingStandard.Gap(2f);
        repSettings.repMaxPsycastLvAllowed = (int)listingStandard.Slider(repSettings.repMaxPsycastLvAllowed, 1f, 6f);
        listingStandard.Gap(4f);
        listingStandard.Label(
            "RepPsycastScaleMonthExp".Translate(repSettings.repPsycastScaleMonth, repSettings.repMaxPsycastLvAllowed));
        listingStandard.Gap(2f);
        listingStandard.IntAdjuster(ref repSettings.repPsycastScaleMonth, 1);
        listingStandard.Gap(4f);
        listingStandard.Label("RepPsycastGainChanceNeolithicExp".Translate(repSettings.repPsycastGainChanceNeolithic));
        listingStandard.Gap(2f);
        repSettings.repPsycastGainChanceNeolithic =
            (int)listingStandard.Slider(repSettings.repPsycastGainChanceNeolithic, 0f, 100f);
        listingStandard.Gap(4f);
        listingStandard.Label("RepPsycastGainChanceMedievalExp".Translate(repSettings.repPsycastGainChanceMedieval));
        listingStandard.Gap(2f);
        repSettings.repPsycastGainChanceMedieval =
            (int)listingStandard.Slider(repSettings.repPsycastGainChanceMedieval, 0f, 100f);
        listingStandard.Gap(4f);
        listingStandard.Label(
            "RepPsycastGainChanceIndustrialExp".Translate(repSettings.repPsycastGainChanceIndustrial));
        listingStandard.Gap(2f);
        repSettings.repPsycastGainChanceIndustrial =
            (int)listingStandard.Slider(repSettings.repPsycastGainChanceIndustrial, 0f, 100f);
        listingStandard.Gap(4f);
        listingStandard.Label("RepPsycastGainChanceSpacerExp".Translate(repSettings.repPsycastGainChanceSpacer));
        listingStandard.Gap(2f);
        repSettings.repPsycastGainChanceSpacer =
            (int)listingStandard.Slider(repSettings.repPsycastGainChanceSpacer, 0f, 100f);
        listingStandard.Gap(4f);
        listingStandard.Label("RepPsycastGainChanceUltraExp".Translate(repSettings.repPsycastGainChanceUltra));
        listingStandard.Gap(2f);
        repSettings.repPsycastGainChanceUltra =
            (int)listingStandard.Slider(repSettings.repPsycastGainChanceUltra, 0f, 100f);
        listingStandard.Gap();
        listingStandard.Label("ListAllowAbilitiesExp".Translate());
        listingStandard.Gap(2f);
        listingStandard.CheckboxLabeled("repAllowBerserkPulseExp".Translate(), ref repSettings.repAllowBerserkPulse);
        listingStandard.CheckboxLabeled("repAllowManhunterPulseExp".Translate(),
            ref repSettings.repAllowManhunterPulse);
        listingStandard.CheckboxLabeled("repAllowBerserkExp".Translate(), ref repSettings.repAllowBerserk);
        listingStandard.CheckboxLabeled("repAllowMassChaosSkipExp".Translate(), ref repSettings.repAllowMassChaosSkip);
        listingStandard.CheckboxLabeled("repAllowInvisibilityExp".Translate(), ref repSettings.repAllowInvisibility);
        listingStandard.CheckboxLabeled("repAllowFocusExp".Translate(), ref repSettings.repAllowFocus);
        listingStandard.CheckboxLabeled("repAllowSkipExp".Translate(), ref repSettings.repAllowSkip);
        listingStandard.CheckboxLabeled("repAllowSmokepopExp".Translate(), ref repSettings.repAllowSmokepop);
        listingStandard.CheckboxLabeled("repAllowWallraiseExp".Translate(), ref repSettings.repAllowWallraise);
        listingStandard.CheckboxLabeled("repAllowBeckonExp".Translate(), ref repSettings.repAllowBeckon);
        listingStandard.CheckboxLabeled("repAllowChaosSkipExp".Translate(), ref repSettings.repAllowChaosSkip);
        listingStandard.CheckboxLabeled("repAllowVertigoPulseExp".Translate(), ref repSettings.repAllowVertigoPulse);
        listingStandard.CheckboxLabeled("repAllowBlindingPulseExp".Translate(), ref repSettings.repAllowBlindingPulse);
        listingStandard.CheckboxLabeled("repAllowWaterskipExp".Translate(), ref repSettings.repAllowWaterskip);
        listingStandard.CheckboxLabeled("repAllowBurdenExp".Translate(), ref repSettings.repAllowBurden);
        listingStandard.CheckboxLabeled("repAllowPainblockExp".Translate(), ref repSettings.repAllowPainblock);
        listingStandard.CheckboxLabeled("repAllowStunExp".Translate(), ref repSettings.repAllowStun);
        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("RepPsycastCurrentModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
        Widgets.EndScrollView();
        base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return "Powerful Psycast AI";
    }

    public override void WriteSettings()
    {
        base.WriteSettings();
        SettingsImplementer.ImplementSettings();
    }
}