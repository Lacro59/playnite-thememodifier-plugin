using Playnite.SDK;
using Playnite.SDK.Data;
using Playnite.SDK.Events;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using CommonPluginsShared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ThemeModifier.Models;
using ThemeModifier.Services;
using ThemeModifier.Views;
using CommonPluginsPlaynite;
using CommonPluginsShared.PlayniteExtended;
using CommonPlayniteShared.Manifests;
using ThemeModifier.Controls;

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
        }


        #region Custom event

        #endregion


        #region Theme integration
        public override List<TopPanelItem> GetTopPanelItems()
        {
            return null;
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
        public override List<GameMenuItem> GetGameMenuItems(GetGameMenuItemsArgs args)
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
        public override List<MainMenuItem> GetMainMenuItems(GetMainMenuItemsArgs args)
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
        public override void OnGameSelected(GameSelectionEventArgs args)
        {

        }
        
        // Add code to be executed when game is finished installing.
        public override void OnGameInstalled(Game game)
        {

        }

        // Add code to be executed when game is started running.
        public override void OnGameStarted(Game game)
        {

        }

        // Add code to be executed when game is preparing to be started.
        public override void OnGameStarting(Game game)
        {

        }

        // Add code to be executed when game is preparing to be started.
        public override void OnGameStopped(Game game, long elapsedSeconds)
        {

        }

        // Add code to be executed when game is uninstalled.
        public override void OnGameUninstalled(Game game)
        {

        }
        #endregion


        #region Application event
        // Add code to be executed when Playnite is initialized.
        public override void OnApplicationStarted()
        {

        }

        // Add code to be executed when Playnite is shutting down.
        public override void OnApplicationStopped()
        {

        }
        #endregion


        // Add code to be executed when library is updated.
        public override void OnLibraryUpdated()
        {

        }


        #region Settings
        public override ISettings GetSettings(bool firstRunSettings)
        {
            return PluginSettings;
        }

        public override UserControl GetSettingsView(bool firstRunSettings)
        {
            return new ThemeModifierSettingsView(PlayniteApi, PluginSettings.Settings, ThemeDefault, PlayniteApi.Paths.ConfigurationPath, this.GetPluginUserDataPath());
        }
        #endregion
    }
}
