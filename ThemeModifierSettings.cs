using Newtonsoft.Json;
using Playnite.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeModifier
{
    public class ThemeModifierSettings : ISettings
    {
        private readonly ThemeModifier plugin;

        public bool EnableCheckVersion { get; set; } = true;

        public string ControlBackgroundBrush_Edit { get; set; } = string.Empty;
        public string TextBrush_Edit { get; set; } = string.Empty;
        public string TextBrushDarker_Edit { get; set; } = string.Empty;
        public string TextBrushDark_Edit { get; set; } = string.Empty;
        public string NormalBrush_Edit { get; set; } = string.Empty;
        public string NormalBrushDark_Edit { get; set; } = string.Empty;
        public string NormalBorderBrush_Edit { get; set; } = string.Empty;
        public string HoverBrush_Edit { get; set; } = string.Empty;
        public string GlyphBrush_Edit { get; set; } = string.Empty;
        public string HighlightGlyphBrush_Edit { get; set; } = string.Empty;
        public string PopupBorderBrush_Edit { get; set; } = string.Empty;
        public string TooltipBackgroundBrush_Edit { get; set; } = string.Empty;
        public string ButtonBackgroundBrush_Edit { get; set; } = string.Empty;
        public string GridItemBackgroundBrush_Edit { get; set; } = string.Empty;
        public string PanelSeparatorBrush_Edit { get; set; } = string.Empty;
        public string PopupBackgroundBrush_Edit { get; set; } = string.Empty;
        public string PositiveRatingBrush_Edit { get; set; } = string.Empty;
        public string NegativeRatingBrush_Edit { get; set; } = string.Empty;
        public string MixedRatingBrush_Edit { get; set; } = string.Empty;
        public string ExpanderBackgroundBrush_Edit { get; set; } = string.Empty;
        public string WindowBackgourndBrush_Edit { get; set; } = string.Empty;

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

                EnableIconChanger = savedSettings.EnableIconChanger;
                UseIconCircle = savedSettings.UseIconCircle;
                UseIconClock = savedSettings.UseIconClock;
                UseIconSquareCorne = savedSettings.UseIconSquareCorne;
                UseIconWe4ponx = savedSettings.UseIconWe4ponx;
                EnableInDescription = savedSettings.EnableInDescription;
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
        }

        public void EndEdit()
        {
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