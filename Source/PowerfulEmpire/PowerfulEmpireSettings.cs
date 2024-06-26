using Mlie;
using UnityEngine;
using Verse;

namespace PowerfulEmpire;

public class PowerfulEmpireSettings : Mod
{
    private static Vector2 scrollPosition;
    private static string currentVersion;
    public static PowerfulEmpireSettings Instance;
    public readonly PowerfulEmpireModSettings settings;

    public PowerfulEmpireSettings(ModContentPack content)
        : base(content)
    {
        settings = GetSettings<PowerfulEmpireModSettings>();
        currentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
        Instance = this;
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        var rect = new Rect(0f, 0f, inRect.width - 16f, inRect.height + 400f);
        var listing_Standard = new Listing_Standard();
        Widgets.BeginScrollView(inRect, ref scrollPosition, rect);
        listing_Standard.Begin(rect);
        if (listing_Standard.ButtonText("Reset".Translate()))
        {
            settings.Reset();
        }

        listing_Standard.CheckboxLabeled("RepAllowAIHeatPushExp".Translate(), ref settings.repAllowAIHeatPush);
        listing_Standard.Gap(4f);
        listing_Standard.CheckboxLabeled("RepGivePsycastToAllExp".Translate(), ref settings.repGivePsycastToAll);
        listing_Standard.Gap(4f);
        listing_Standard.CheckboxLabeled("RepAllowIdentifierHatExp".Translate(), ref settings.repAllowIdentifierHat);
        listing_Standard.Gap(4f);
        listing_Standard.Label("RepMaxPsycastLvAllowedExp".Translate(settings.repMaxPsycastLvAllowed));
        listing_Standard.Gap(2f);
        settings.repMaxPsycastLvAllowed = (int)listing_Standard.Slider(settings.repMaxPsycastLvAllowed, 1f, 6f);
        listing_Standard.Gap(4f);
        listing_Standard.Label(
            "RepPsycastScaleMonthExp".Translate(settings.repPsycastScaleMonth, settings.repMaxPsycastLvAllowed));
        listing_Standard.Gap(2f);
        listing_Standard.IntAdjuster(ref settings.repPsycastScaleMonth, 1);
        listing_Standard.Gap(4f);
        listing_Standard.Label("RepPsycastGainChanceNeolithicExp".Translate(settings.repPsycastGainChanceNeolithic));
        listing_Standard.Gap(2f);
        settings.repPsycastGainChanceNeolithic =
            (int)listing_Standard.Slider(settings.repPsycastGainChanceNeolithic, 0f, 100f);
        listing_Standard.Gap(4f);
        listing_Standard.Label("RepPsycastGainChanceMedievalExp".Translate(settings.repPsycastGainChanceMedieval));
        listing_Standard.Gap(2f);
        settings.repPsycastGainChanceMedieval =
            (int)listing_Standard.Slider(settings.repPsycastGainChanceMedieval, 0f, 100f);
        listing_Standard.Gap(4f);
        listing_Standard.Label("RepPsycastGainChanceIndustrialExp".Translate(settings.repPsycastGainChanceIndustrial));
        listing_Standard.Gap(2f);
        settings.repPsycastGainChanceIndustrial =
            (int)listing_Standard.Slider(settings.repPsycastGainChanceIndustrial, 0f, 100f);
        listing_Standard.Gap(4f);
        listing_Standard.Label("RepPsycastGainChanceSpacerExp".Translate(settings.repPsycastGainChanceSpacer));
        listing_Standard.Gap(2f);
        settings.repPsycastGainChanceSpacer =
            (int)listing_Standard.Slider(settings.repPsycastGainChanceSpacer, 0f, 100f);
        listing_Standard.Gap(4f);
        listing_Standard.Label("RepPsycastGainChanceUltraExp".Translate(settings.repPsycastGainChanceUltra));
        listing_Standard.Gap(2f);
        settings.repPsycastGainChanceUltra = (int)listing_Standard.Slider(settings.repPsycastGainChanceUltra, 0f, 100f);
        listing_Standard.Gap();
        listing_Standard.Label("ListAllowAbilitiesExp".Translate());
        listing_Standard.Gap(2f);
        listing_Standard.CheckboxLabeled("repAllowBerserkPulseExp".Translate(), ref settings.repAllowBerserkPulse);
        listing_Standard.CheckboxLabeled("repAllowManhunterPulseExp".Translate(), ref settings.repAllowManhunterPulse);
        listing_Standard.CheckboxLabeled("repAllowBerserkExp".Translate(), ref settings.repAllowBerserk);
        listing_Standard.CheckboxLabeled("repAllowMassChaosSkipExp".Translate(), ref settings.repAllowMassChaosSkip);
        listing_Standard.CheckboxLabeled("repAllowInvisibilityExp".Translate(), ref settings.repAllowInvisibility);
        listing_Standard.CheckboxLabeled("repAllowFocusExp".Translate(), ref settings.repAllowFocus);
        listing_Standard.CheckboxLabeled("repAllowSkipExp".Translate(), ref settings.repAllowSkip);
        listing_Standard.CheckboxLabeled("repAllowSmokepopExp".Translate(), ref settings.repAllowSmokepop);
        listing_Standard.CheckboxLabeled("repAllowWallraiseExp".Translate(), ref settings.repAllowWallraise);
        listing_Standard.CheckboxLabeled("repAllowBeckonExp".Translate(), ref settings.repAllowBeckon);
        listing_Standard.CheckboxLabeled("repAllowChaosSkipExp".Translate(), ref settings.repAllowChaosSkip);
        listing_Standard.CheckboxLabeled("repAllowVertigoPulseExp".Translate(), ref settings.repAllowVertigoPulse);
        listing_Standard.CheckboxLabeled("repAllowBlindingPulseExp".Translate(), ref settings.repAllowBlindingPulse);
        listing_Standard.CheckboxLabeled("repAllowWaterskipExp".Translate(), ref settings.repAllowWaterskip);
        listing_Standard.CheckboxLabeled("repAllowBurdenExp".Translate(), ref settings.repAllowBurden);
        listing_Standard.CheckboxLabeled("repAllowPainblockExp".Translate(), ref settings.repAllowPainblock);
        listing_Standard.CheckboxLabeled("repAllowStunExp".Translate(), ref settings.repAllowStun);
        if (currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("RepPsycastCurrentModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
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