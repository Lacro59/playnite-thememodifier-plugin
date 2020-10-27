using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Playnite.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThemeModifier.Models;
using ThemeModifier.Services;
using ThemeModifier.Views;

namespace ThemeModifier
{
    public class ThemeModifierSettings : ISettings
    {
        private readonly ThemeModifier plugin;

        public bool EnableCheckVersion { get; set; } = true;
        public bool MenuInExtensions { get; set; } = true;

        public string ControlBackgroundBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient ControlBackgroundBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string TextBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient TextBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string TextBrushDarker_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient TextBrushDarker_EditGradient { get; set; } = new ThemeLinearGradient();
        public string TextBrushDark_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient TextBrushDark_EditGradient { get; set; } = new ThemeLinearGradient();
        public string NormalBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient NormalBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string NormalBrushDark_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient NormalBrushDark_EditGradient { get; set; } = new ThemeLinearGradient();
        public string NormalBorderBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient NormalBorderBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string HoverBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient HoverBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string GlyphBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient GlyphBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string HighlightGlyphBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient HighlightGlyphBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string PopupBorderBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient PopupBorderBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string TooltipBackgroundBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient TooltipBackgroundBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string ButtonBackgroundBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient ButtonBackgroundBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string GridItemBackgroundBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient GridItemBackgroundBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string PanelSeparatorBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient PanelSeparatorBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string PopupBackgroundBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient PopupBackgroundBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string PositiveRatingBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient PositiveRatingBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string NegativeRatingBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient NegativeRatingBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string MixedRatingBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient MixedRatingBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string ExpanderBackgroundBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient ExpanderBackgroundBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string WindowBackgourndBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient WindowBackgourndBrush_EditGradient { get; set; } = new ThemeLinearGradient();
        public string WarningBrush_Edit { get; set; } = string.Empty;
        public ThemeLinearGradient WarningBrush_EditGradient { get; set; } = new ThemeLinearGradient();


        public List<ThemeConstants> ThemesConstants = new List<ThemeConstants>();



        public bool EnableIconChanger { get; set; } = false;
        public bool UseIconCircle { get; set; } = true;
        public bool UseIconClock { get; set; } = false;
        public bool UseIconSquareCorne { get; set; } = false;
        public bool UseIconWe4ponx { get; set; } = false;
        public bool EnableInDescription { get; set; } = true;

        // Playnite serializes settings object to a JSON object and saves it as text file.
        // If you want to exclude some property from being saved then use `JsonIgnore` ignore attribute.
        [JsonIgnore]
        public bool OptionThatWontBeSaved { get; set; } = false;

        // Parameterless constructor must exist if you want to use LoadPluginSettings method.
        public ThemeModifierSettings()
        {
        }

        public ThemeModifierSettings(ThemeModifier plugin)
        {
            // Injecting your plugin instance is required for Save/Load method because Playnite saves data to a location based on what plugin requested the operation.
            this.plugin = plugin;

            // Load saved settings.
            var savedSettings = plugin.LoadPluginSettings<ThemeModifierSettings>();

            // LoadPluginSettings returns null if not saved data is available.
            if (savedSettings != null)
            {
                EnableCheckVersion = savedSettings.EnableCheckVersion;
                MenuInExtensions = savedSettings.MenuInExtensions;


                ControlBackgroundBrush_Edit = savedSettings.ControlBackgroundBrush_Edit;
                TextBrush_Edit = savedSettings.TextBrush_Edit;
                TextBrushDarker_Edit = savedSettings.TextBrushDarker_Edit;
                TextBrushDark_Edit = savedSettings.TextBrushDark_Edit;
                NormalBrush_Edit = savedSettings.NormalBrush_Edit;
                NormalBrushDark_Edit = savedSettings.NormalBrushDark_Edit;
                NormalBorderBrush_Edit = savedSettings.NormalBorderBrush_Edit;
                HoverBrush_Edit = savedSettings.HoverBrush_Edit;
                GlyphBrush_Edit = savedSettings.GlyphBrush_Edit;
                HighlightGlyphBrush_Edit = savedSettings.HighlightGlyphBrush_Edit;
                PopupBorderBrush_Edit = savedSettings.PopupBorderBrush_Edit;
                TooltipBackgroundBrush_Edit = savedSettings.TooltipBackgroundBrush_Edit;
                ButtonBackgroundBrush_Edit = savedSettings.ButtonBackgroundBrush_Edit;
                GridItemBackgroundBrush_Edit = savedSettings.GridItemBackgroundBrush_Edit;
                PanelSeparatorBrush_Edit = savedSettings.PanelSeparatorBrush_Edit;
                PopupBackgroundBrush_Edit = savedSettings.PopupBackgroundBrush_Edit;
                PositiveRatingBrush_Edit = savedSettings.PositiveRatingBrush_Edit;
                NegativeRatingBrush_Edit = savedSettings.NegativeRatingBrush_Edit;
                MixedRatingBrush_Edit = savedSettings.MixedRatingBrush_Edit;
                ExpanderBackgroundBrush_Edit = savedSettings.ExpanderBackgroundBrush_Edit;
                WindowBackgourndBrush_Edit = savedSettings.WindowBackgourndBrush_Edit;
                WarningBrush_Edit = savedSettings.WarningBrush_Edit;


                ControlBackgroundBrush_EditGradient = savedSettings.ControlBackgroundBrush_EditGradient;
                TextBrush_EditGradient = savedSettings.TextBrush_EditGradient;
                TextBrushDarker_EditGradient = savedSettings.TextBrushDarker_EditGradient;
                TextBrushDark_EditGradient = savedSettings.TextBrushDark_EditGradient;
                NormalBrush_EditGradient = savedSettings.NormalBrush_EditGradient;
                NormalBrushDark_EditGradient = savedSettings.NormalBrushDark_EditGradient;
                NormalBorderBrush_EditGradient = savedSettings.NormalBorderBrush_EditGradient;
                HoverBrush_EditGradient = savedSettings.HoverBrush_EditGradient;
                GlyphBrush_EditGradient = savedSettings.GlyphBrush_EditGradient;
                HighlightGlyphBrush_EditGradient = savedSettings.HighlightGlyphBrush_EditGradient;
                PopupBorderBrush_EditGradient = savedSettings.PopupBorderBrush_EditGradient;
                TooltipBackgroundBrush_EditGradient = savedSettings.TooltipBackgroundBrush_EditGradient;
                ButtonBackgroundBrush_EditGradient = savedSettings.ButtonBackgroundBrush_EditGradient;
                GridItemBackgroundBrush_EditGradient = savedSettings.GridItemBackgroundBrush_EditGradient;
                PanelSeparatorBrush_EditGradient = savedSettings.PanelSeparatorBrush_EditGradient;
                PopupBackgroundBrush_EditGradient = savedSettings.PopupBackgroundBrush_EditGradient;
                PositiveRatingBrush_EditGradient = savedSettings.PositiveRatingBrush_EditGradient;
                NegativeRatingBrush_EditGradient = savedSettings.NegativeRatingBrush_EditGradient;
                MixedRatingBrush_EditGradient = savedSettings.MixedRatingBrush_EditGradient;
                ExpanderBackgroundBrush_EditGradient = savedSettings.ExpanderBackgroundBrush_EditGradient;
                WindowBackgourndBrush_EditGradient = savedSettings.WindowBackgourndBrush_EditGradient;
                WarningBrush_EditGradient = savedSettings.WarningBrush_EditGradient;


                EnableIconChanger = savedSettings.EnableIconChanger;
                UseIconCircle = savedSettings.UseIconCircle;
                UseIconClock = savedSettings.UseIconClock;
                UseIconSquareCorne = savedSettings.UseIconSquareCorne;
                UseIconWe4ponx = savedSettings.UseIconWe4ponx;
                EnableInDescription = savedSettings.EnableInDescription;

                ThemesConstants = savedSettings.ThemesConstants;
            }
        }

        public void BeginEdit()
        {
            // Code executed when settings view is opened and user starts editing values.
        }

        public void CancelEdit()
        {
            // Code executed when user decides to cancel any changes made since BeginEdit was called.
            // This method should revert any changes made to Option1 and Option2.

            var savedSettings = plugin.LoadPluginSettings<ThemeModifierSettings>();
            var settings = new ThemeModifierSettings(plugin);
            ThemeClass.RestoreColor(ThemeModifier.ThemeDefault, settings);
            ThemeClass.RestoreColor(ThemeModifier.ThemeDefault, savedSettings, true);

            ThemeClass.SetThemeSettingsConstants(ThemeModifier.ThemeDefaultConstants);
            ThemeClass.SetThemeSettingsConstants(ThemeModifier.ThemeActualConstants);
        }

        public void EndEdit()
        {
            ThemesConstants = ThemeClass.GetThemesConstants(plugin.PlayniteApi, ThemeModifierSettingsView.SettingsThemeConstants, ThemesConstants);
            ThemeModifier.ThemeActualConstants = ThemeModifierSettingsView.SettingsThemeConstants;

            ThemeClass.SetThemeSettingsConstants(ThemeModifier.ThemeDefaultConstants);
            ThemeClass.SetThemeSettingsConstants(ThemeModifier.ThemeActualConstants);

            // Code executed when user decides to confirm changes made since BeginEdit was called.
            // This method should save settings made to Option1 and Option2.
            plugin.SavePluginSettings(this);
        }

        public bool VerifySettings(out List<string> errors)
        {
            // Code execute when user decides to confirm changes made since BeginEdit was called.
            // Executed before EndEdit is called and EndEdit is not called if false is returned.
            // List of errors is presented to user if verification fails.
            errors = new List<string>();
            return true;
        }
    }
}