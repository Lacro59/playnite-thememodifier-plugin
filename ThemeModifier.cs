using Playnite.Controls;
using Playnite.SDK;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using PluginCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Media;
using ThemeModifier.Models;
using ThemeModifier.Services;
using ThemeModifier.Views;

namespace ThemeModifier
{
    public class ThemeModifier : Plugin
    {
        private static readonly ILogger logger = LogManager.GetLogger();
        private static IResourceProvider resources = new ResourceProvider();

        private ThemeModifierSettings settings { get; set; }

        private List<ThemeElement> ThemeDefault = new List<ThemeElement>();

        public override Guid Id { get; } = Guid.Parse("ec2f4013-17e6-428a-b8a9-5e34a3b80009");

        public ThemeModifier(IPlayniteAPI api) : base(api)
        {
            settings = new ThemeModifierSettings(this);


            // Get plugin's location 
            string pluginFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Add plugin localization in application ressource.
            PluginCommon.Localization.SetPluginLanguage(pluginFolder, api.Paths.ConfigurationPath);
            // Add common in application ressource.
            PluginCommon.Common.Load(pluginFolder);

            // Check version
            if (settings.EnableCheckVersion)
            {
                CheckVersion cv = new CheckVersion();

                if (cv.Check("ThemeModifier", pluginFolder))
                {
                    cv.ShowNotification(api, "ThemeModifier - " + resources.GetString("LOCUpdaterWindowTitle"));
                }
            }


            EventManager.RegisterClassHandler(typeof(Button), Button.ClickEvent, new RoutedEventHandler(OnButtonCancelClick));

            // Default values
            ThemeDefault.Add(new ThemeElement { name = "ControlBackgroundBrush", color = resources.GetResource("ControlBackgroundBrush") });
            ThemeDefault.Add(new ThemeElement { name = "TextBrush", color = resources.GetResource("TextBrush") });
            ThemeDefault.Add(new ThemeElement { name = "TextBrushDarker", color = resources.GetResource("TextBrushDarker") });
            ThemeDefault.Add(new ThemeElement { name = "TextBrushDark", color = resources.GetResource("TextBrushDark") });
            ThemeDefault.Add(new ThemeElement { name = "NormalBrush", color = resources.GetResource("NormalBrush") });
            ThemeDefault.Add(new ThemeElement { name = "NormalBrushDark", color = resources.GetResource("NormalBrushDark") });
            ThemeDefault.Add(new ThemeElement { name = "NormalBorderBrush", color = resources.GetResource("NormalBorderBrush") });
            ThemeDefault.Add(new ThemeElement { name = "HoverBrush", color = resources.GetResource("HoverBrush") });
            ThemeDefault.Add(new ThemeElement { name = "GlyphBrush", color = resources.GetResource("GlyphBrush") });
            ThemeDefault.Add(new ThemeElement { name = "HighlightGlyphBrush", color = resources.GetResource("HighlightGlyphBrush") });
            ThemeDefault.Add(new ThemeElement { name = "PopupBorderBrush", color = resources.GetResource("PopupBorderBrush") });
            ThemeDefault.Add(new ThemeElement { name = "TooltipBackgroundBrush", color = resources.GetResource("TooltipBackgroundBrush") });
            ThemeDefault.Add(new ThemeElement { name = "ButtonBackgroundBrush", color = resources.GetResource("ButtonBackgroundBrush") });
            ThemeDefault.Add(new ThemeElement { name = "GridItemBackgroundBrush", color = resources.GetResource("GridItemBackgroundBrush") });
            ThemeDefault.Add(new ThemeElement { name = "PanelSeparatorBrush", color = resources.GetResource("PanelSeparatorBrush") });
            ThemeDefault.Add(new ThemeElement { name = "PopupBackgroundBrush", color = resources.GetResource("PopupBackgroundBrush") });
            ThemeDefault.Add(new ThemeElement { name = "PositiveRatingBrush", color = resources.GetResource("PositiveRatingBrush") });
            ThemeDefault.Add(new ThemeElement { name = "NegativeRatingBrush", color = resources.GetResource("NegativeRatingBrush") });
            ThemeDefault.Add(new ThemeElement { name = "MixedRatingBrush", color = resources.GetResource("MixedRatingBrush") });
            ThemeDefault.Add(new ThemeElement { name = "ExpanderBackgroundBrush", color = resources.GetResource("ExpanderBackgroundBrush") });
            ThemeDefault.Add(new ThemeElement { name = "WindowBackgourndBrush", color = resources.GetResource("WindowBackgourndBrush") });


            // Add modified values
            ThemeClass.SetThemeSettings(settings);
        }

        public override IEnumerable<ExtensionFunction> GetFunctions()
        {
            return new List<ExtensionFunction>
            {
                //new ExtensionFunction(
                //    "Execute function from GenericPlugin",
                //    () =>
                //    {
                //        // Add code to be execute when user invokes this menu entry.
                //        PlayniteApi.Dialogs.ShowMessage("Code executed from a plugin!");
                //    })
            };
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            T parent = VisualTreeHelper.GetParent(child) as T;

            if (parent != null)
            {
                return parent;
            }
            else
            {
                return FindParent<T>(VisualTreeHelper.GetParent(child));
            }
        }
        private void OnButtonCancelClick(object sender, RoutedEventArgs e)
        {
            string ButtonName = "";
            try
            {
                ButtonName = ((Button)sender).Name;
                if (ButtonName == "ButtonCancel")
                {
                    if ((string)FindParent<WindowBase>((Button)sender).GetValue(AutomationProperties.AutomationIdProperty) == "WindowSettings")
                    {
                        var savedSettings = this.LoadPluginSettings<ThemeModifierSettings>();
                        ThemeClass.RestoreColor(ThemeDefault, settings);
                        ThemeClass.RestoreColor(ThemeDefault, savedSettings, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "HowLongToBeat", "OnButtonCancelClick() error");
            }
        }


        public override void OnGameInstalled(Game game)
        {
            // Add code to be executed when game is finished installing.
        }

        public override void OnGameStarted(Game game)
        {
            // Add code to be executed when game is started running.
        }

        public override void OnGameStarting(Game game)
        {
            // Add code to be executed when game is preparing to be started.
        }

        public override void OnGameStopped(Game game, long elapsedSeconds)
        {
            // Add code to be executed when game is preparing to be started.
        }

        public override void OnGameUninstalled(Game game)
        {
            // Add code to be executed when game is uninstalled.
        }

        public override void OnApplicationStarted()
        {
            // Add code to be executed when Playnite is initialized.
        }

        public override void OnApplicationStopped()
        {
            // Add code to be executed when Playnite is shutting down.
        }

        public override void OnLibraryUpdated()
        {
            // Add code to be executed when library is updated.
        }

        public override ISettings GetSettings(bool firstRunSettings)
        {
            return settings;
        }

        public override UserControl GetSettingsView(bool firstRunSettings)
        {
            return new ThemeModifierSettingsView(settings, ThemeDefault);
        }
    }
}