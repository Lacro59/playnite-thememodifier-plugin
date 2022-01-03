using CommonPluginsShared.Models;
using Playnite.SDK;
using Playnite.SDK.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Media.Imaging;
using ThemeModifier.Models;
using ThemeModifier.Services;
using ThemeModifier.Views;

namespace ThemeModifier
{
    public class ThemeModifierSettings : ObservableObject
    {
        #region Settings variables
        public bool MenuInExtensions { get; set; } = true;

        [DontSerialize]
        public BitmapImage BitmapFrame { get; set; }
        [DontSerialize]
        public BitmapImage BitmapShape { get; set; }


        public bool EnableIntegrationButtonHeader { get; set; } = true;

        [DontSerialize]
        public bool OnlyEditConstant { get; set; } = false;

        public string ControlBackgroundBrush_Edit { get; set; } = string.Empty;
        public LinearGradient ControlBackgroundBrush_EditGradient { get; set; } = new LinearGradient();
        public string TextBrush_Edit { get; set; } = string.Empty;
        public LinearGradient TextBrush_EditGradient { get; set; } = new LinearGradient();
        public string TextBrushDarker_Edit { get; set; } = string.Empty;
        public LinearGradient TextBrushDarker_EditGradient { get; set; } = new LinearGradient();
        public string TextBrushDark_Edit { get; set; } = string.Empty;
        public LinearGradient TextBrushDark_EditGradient { get; set; } = new LinearGradient();
        public string NormalBrush_Edit { get; set; } = string.Empty;
        public LinearGradient NormalBrush_EditGradient { get; set; } = new LinearGradient();
        public string NormalBrushDark_Edit { get; set; } = string.Empty;
        public LinearGradient NormalBrushDark_EditGradient { get; set; } = new LinearGradient();
        public string NormalBorderBrush_Edit { get; set; } = string.Empty;
        public LinearGradient NormalBorderBrush_EditGradient { get; set; } = new LinearGradient();
        public string HoverBrush_Edit { get; set; } = string.Empty;
        public LinearGradient HoverBrush_EditGradient { get; set; } = new LinearGradient();
        public string GlyphBrush_Edit { get; set; } = string.Empty;
        public LinearGradient GlyphBrush_EditGradient { get; set; } = new LinearGradient();
        public string HighlightGlyphBrush_Edit { get; set; } = string.Empty;
        public LinearGradient HighlightGlyphBrush_EditGradient { get; set; } = new LinearGradient();
        public string PopupBorderBrush_Edit { get; set; } = string.Empty;
        public LinearGradient PopupBorderBrush_EditGradient { get; set; } = new LinearGradient();
        public string TooltipBackgroundBrush_Edit { get; set; } = string.Empty;
        public LinearGradient TooltipBackgroundBrush_EditGradient { get; set; } = new LinearGradient();
        public string ButtonBackgroundBrush_Edit { get; set; } = string.Empty;
        public LinearGradient ButtonBackgroundBrush_EditGradient { get; set; } = new LinearGradient();
        public string GridItemBackgroundBrush_Edit { get; set; } = string.Empty;
        public LinearGradient GridItemBackgroundBrush_EditGradient { get; set; } = new LinearGradient();
        public string PanelSeparatorBrush_Edit { get; set; } = string.Empty;
        public LinearGradient PanelSeparatorBrush_EditGradient { get; set; } = new LinearGradient();
        public string WindowPanelSeparatorBrush_Edit { get; set; } = string.Empty;
        public LinearGradient WindowPanelSeparatorBrush_EditGradient { get; set; } = new LinearGradient();
        public string PopupBackgroundBrush_Edit { get; set; } = string.Empty;
        public LinearGradient PopupBackgroundBrush_EditGradient { get; set; } = new LinearGradient();
        public string CheckBoxCheckMarkBkBrush_Edit { get; set; } = string.Empty;
        public LinearGradient CheckBoxCheckMarkBkBrush_EditGradient { get; set; } = new LinearGradient();

        public string PositiveRatingBrush_Edit { get; set; } = string.Empty;
        public LinearGradient PositiveRatingBrush_EditGradient { get; set; } = new LinearGradient();
        public string NegativeRatingBrush_Edit { get; set; } = string.Empty;
        public LinearGradient NegativeRatingBrush_EditGradient { get; set; } = new LinearGradient();
        public string MixedRatingBrush_Edit { get; set; } = string.Empty;
        public LinearGradient MixedRatingBrush_EditGradient { get; set; } = new LinearGradient();

        public string WarningBrush_Edit { get; set; } = string.Empty;
        public LinearGradient WarningBrush_EditGradient { get; set; } = new LinearGradient();

        public string ExpanderBackgroundBrush_Edit { get; set; } = string.Empty;
        public LinearGradient ExpanderBackgroundBrush_EditGradient { get; set; } = new LinearGradient();
        public string WindowBackgourndBrush_Edit { get; set; } = string.Empty;
        public LinearGradient WindowBackgourndBrush_EditGradient { get; set; } = new LinearGradient();


        public List<ThemeConstants> ThemesConstants = new List<ThemeConstants>();


        private bool _EnableIntegrationIcon { get; set; } = true;
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

            ThemeModifier.SetFrame(this, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            this.OnPropertyChanged();
        }

        // Code executed when user decides to confirm changes made since BeginEdit was called.
        // This method should save settings made to Option1 and Option2.
        public void EndEdit()
        {
            Settings.ThemesConstants = ThemeClass.GetThemesConstants(Plugin.PlayniteApi, ThemeModifierSettingsView.SettingsThemeConstants, Settings.ThemesConstants);
            ThemeModifier.ThemeActualConstants = ThemeModifierSettingsView.SettingsThemeConstants;

            ThemeClass.SetThemeSettingsConstants(ThemeModifier.ThemeDefaultConstants);
            ThemeClass.SetThemeSettingsConstants(ThemeModifier.ThemeActualConstants);

            ThemeModifier.SetFrame(this, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

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
