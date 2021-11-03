using Playnite.SDK;
using Playnite.SDK.Events;
using Playnite.SDK.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ThemeModifier.Models;
using ThemeModifier.Services;
using ThemeModifier.Views;
using CommonPluginsShared.PlayniteExtended;
using ThemeModifier.Controls;
using System.Windows.Media;

namespace ThemeModifier
{
    public class ThemeModifier : PluginExtended<ThemeModifierSettingsViewModel>
    {
        public override Guid Id { get; } = Guid.Parse("ec2f4013-17e6-428a-b8a9-5e34a3b80009");

        public static List<ThemeElement> ThemeDefault = new List<ThemeElement>();
        public static List<ThemeElement> ThemeDefaultConstants = new List<ThemeElement>();
        public static List<ThemeElement> ThemeActualConstants = new List<ThemeElement>();


        public ThemeModifier(IPlayniteAPI api) : base(api)
        {
            // Theme default
            ThemeDefault = ThemeClass.GetThemeDefault();
            ThemeDefaultConstants = ThemeClass.GetThemeDefaultConstants(PlayniteApi);

            // Theme actual
            if (ThemeDefaultConstants.Count > 0)
            {
                ThemeActualConstants = ThemeClass.GetThemeActualConstants(PluginSettings.Settings, PlayniteApi);
            }

            // Add modified values
            if (PlayniteApi.ApplicationInfo.Mode == ApplicationMode.Desktop)
            {
                ThemeClass.SetThemeSettings(PluginSettings.Settings);
                ThemeClass.SetThemeSettingsConstants(ThemeActualConstants);
            }

            // Custom elements integration
            AddCustomElementSupport(new AddCustomElementSupportArgs
            {
                ElementList = new List<string> { "PluginIcon" },
                SourceName = "ThemeModifier"
            });

            // Settings integration
            AddSettingsSupport(new AddSettingsSupportArgs
            {
                SourceName = "ThemeModifier",
                SettingsRoot = $"{nameof(PluginSettings)}.{nameof(PluginSettings.Settings)}"
            });


            SetFrame(PluginSettings, PluginFolder);
        }


        public static void SetFrame(ThemeModifierSettingsViewModel PluginSettings, string PluginFolder)
        {
            if (PluginSettings.Settings.EnableIntegrationIcon)
            {
                string ImageName = string.Empty;
                if (PluginSettings.Settings.UseIconCircle)
                {
                    ImageName = "circle";
                }
                if (PluginSettings.Settings.UseIconClock)
                {
                    ImageName = "clock";
                }
                if (PluginSettings.Settings.UseIconSquareCorne)
                {
                    ImageName = "squareCorne";
                }
                if (PluginSettings.Settings.UseIconWe4ponx)
                {
                    ImageName = "we4ponx";
                }

                string ImageFramePath = Path.Combine(PluginFolder, "Resources", "Images", ImageName + ".png");
                string ImageShapePath = Path.Combine(PluginFolder, "Resources", "Images", ImageName + "Shape.png");

                if (File.Exists(ImageFramePath) && File.Exists(ImageShapePath))
                {
                    PluginSettings.Settings.BitmapFrame = new BitmapImage(new Uri(ImageFramePath));
                    PluginSettings.Settings.BitmapShape = new BitmapImage(new Uri(ImageShapePath));
                }
            }
        }


        #region Custom event

        #endregion


        #region Theme integration
        public override IEnumerable<TopPanelItem> GetTopPanelItems()
        {
            if (PluginSettings.Settings.EnableIntegrationButtonHeader && ThemeDefaultConstants.Count > 0)
            {
                yield return new TopPanelItem()
                {
                    Icon = new TextBlock
                    {
                        Text = "\ue91c",
                        FontSize = 20,
                        FontFamily = resources.GetResource("CommonFont") as FontFamily
                    },
                    Title = resources.GetString("LOCThemeModifierEditThemeConstants"),
                    Activated = () =>
                    {
                        PluginSettings.Settings.OnlyEditConstant = true;
                        this.OpenSettingsView();
                        PluginSettings.Settings.OnlyEditConstant = false;
                    }
                };
            }

            yield break;
        }

        // List custom controls
        public override Control GetGameViewControl(GetGameViewControlArgs args)
        {
            if (args.Name == "PluginIcon")
            {
                return new PluginIcon(PlayniteApi, PluginSettings);
            }

            return null;
        }
        #endregion


        #region Menus
        // To add new game menu items override GetGameMenuItems
        public override IEnumerable<GameMenuItem> GetGameMenuItems(GetGameMenuItemsArgs args)
        {
            List<GameMenuItem> gameMenuItems = new List<GameMenuItem>
            {

            };

#if DEBUG
            gameMenuItems.Add(new GameMenuItem
            {
                MenuSection = resources.GetString("LOCThemeModifier"),
                Description = "-"
            });
            gameMenuItems.Add(new GameMenuItem
            {
                MenuSection = resources.GetString("LOCThemeModifier"),
                Description = "Test",
                Action = (mainMenuItem) => { }
            });
#endif

            return gameMenuItems;
        }

        // To add new main menu items override GetMainMenuItems
        public override IEnumerable<MainMenuItem> GetMainMenuItems(GetMainMenuItemsArgs args)
        {
            string MenuInExtensions = string.Empty;
            if (PluginSettings.Settings.MenuInExtensions)
            {
                MenuInExtensions = "@";
            }

            List<MainMenuItem> mainMenuItems = new List<MainMenuItem>
            {

            };

#if DEBUG
            mainMenuItems.Add(new MainMenuItem
            {
                MenuSection = MenuInExtensions + resources.GetString("LOCThemeModifier"),
                Description = "-"
            });
            mainMenuItems.Add(new MainMenuItem
            {
                MenuSection = MenuInExtensions + resources.GetString("LOCThemeModifier"),
                Description = "Test",
                Action = (mainMenuItem) => { }
            });
#endif

            return mainMenuItems;
        }
        #endregion


        #region Game event
        public override void OnGameSelected(OnGameSelectedEventArgs args)
        {

        }
        
        // Add code to be executed when game is finished installing.
        public override void OnGameInstalled(OnGameInstalledEventArgs args)
        {

        }

        // Add code to be executed when game is uninstalled.
        public override void OnGameUninstalled(OnGameUninstalledEventArgs args)
        {

        }

        // Add code to be executed when game is preparing to be started.
        public override void OnGameStarting(OnGameStartingEventArgs args)
        {

        }

        // Add code to be executed when game is started running.
        public override void OnGameStarted(OnGameStartedEventArgs args)
        {

        }

        // Add code to be executed when game is preparing to be started.
        public override void OnGameStopped(OnGameStoppedEventArgs args)
        {

        }
        #endregion


        #region Application event
        // Add code to be executed when Playnite is initialized.
        public override void OnApplicationStarted(OnApplicationStartedEventArgs args)
        {

        }

        // Add code to be executed when Playnite is shutting down.
        public override void OnApplicationStopped(OnApplicationStoppedEventArgs args)
        {

        }
        #endregion


        // Add code to be executed when library is updated.
        public override void OnLibraryUpdated(OnLibraryUpdatedEventArgs args)
        {

        }


        #region Settings
        public override ISettings GetSettings(bool firstRunSettings)
        {
            return PluginSettings;
        }

        public override UserControl GetSettingsView(bool firstRunSettings)
        {
            PluginSettings.Settings.OnlyEditConstant = false;
            return new ThemeModifierSettingsView(PlayniteApi, PluginSettings.Settings, ThemeDefault, PlayniteApi.Paths.ConfigurationPath, this.GetPluginUserDataPath());
        }
        #endregion
    }
}
