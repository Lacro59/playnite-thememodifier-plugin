using Playnite.SDK;
using Playnite.SDK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThemeModifier.Models;
using ThemeModifier.Services;

namespace ThemeModifier
{
    public class ThemeModifierSettings : ObservableObject
    {
        #region Settings variables
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


        private bool _EnableIntegrationIcon { get; set; } = false;
        public bool EnableIntegrationIcon
        {
            get => _EnableIntegrationIcon;
            set
            {
                _EnableIntegrationIcon = value;
                OnPropertyChanged();
            }
        }

        public bool UseIconCircle { get; set; } = true;
        public bool UseIconClock { get; set; } = false;
        public bool UseIconSquareCorne { get; set; } = false;
        public bool UseIconWe4ponx { get; set; } = false;
        #endregion

        // Playnite serializes settings object to a JSON object and saves it as text file.
        // If you want to exclude some property from being saved then use `JsonDontSerialize` ignore attribute.
        #region Variables exposed

        #endregion  
    }


    public class ThemeModifierSettingsViewModel : ObservableObject, ISettings
    {
        private readonly ThemeModifier Plugin;
        private ThemeModifierSettings EditingClone { get; set; }

        private ThemeModifierSettings _Settings;
        public ThemeModifierSettings Settings
        {
            get => _Settings;
            set
            {
                _Settings = value;
                OnPropertyChanged();
            }
        }


        public ThemeModifierSettingsViewModel(ThemeModifier plugin)
        {
            // Injecting your plugin instance is required for Save/Load method because Playnite saves data to a location based on what plugin requested the operation.
            Plugin = plugin;

            // Load saved settings.
            var savedSettings = plugin.LoadPluginSettings<ThemeModifierSettings>();

            // LoadPluginSettings returns null if not saved data is available.
            if (savedSettings != null)
            {
                Settings = savedSettings;
            }
            else
            {
                Settings = new ThemeModifierSettings();
            }
        }

        // Code executed when settings view is opened and user starts editing values.
        public void BeginEdit()
        {
            EditingClone = Serialization.GetClone(Settings);
        }

        // Code executed when user decides to cancel any changes made since BeginEdit was called.
        // This method should revert any changes made to Option1 and Option2.
        public void CancelEdit()
        {
            ThemeClass.RestoreColor(ThemeModifier.ThemeDefault, Settings);

            Settings = EditingClone;

            ThemeClass.RestoreColor(ThemeModifier.ThemeDefault, Settings, true);

            ThemeClass.SetThemeSettingsConstants(ThemeModifier.ThemeDefaultConstants);
            ThemeClass.SetThemeSettingsConstants(ThemeModifier.ThemeActualConstants);
        }

        // Code executed when user decides to confirm changes made since BeginEdit was called.
        // This method should save settings made to Option1 and Option2.
        public void EndEdit()
        {
            Plugin.SavePluginSettings(Settings);
            this.OnPropertyChanged();
        }

        // Code execute when user decides to confirm changes made since BeginEdit was called.
        // Executed before EndEdit is called and EndEdit is not called if false is returned.
        // List of errors is presented to user if verification fails.
        public bool VerifySettings(out List<string> errors)
        {
            errors = new List<string>();
            return true;
        }
    }
}
