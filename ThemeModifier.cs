using Newtonsoft.Json;
using Playnite.SDK;
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

            Common.LogDebug(true, $"ThemeDefault: {JsonConvert.SerializeObject(ThemeDefault)}");
            Common.LogDebug(true, $"ThemeDefaultConstants: {JsonConvert.SerializeObject(ThemeDefaultConstants)}");
            Common.LogDebug(true, $"ThemeActualConstants: {JsonConvert.SerializeObject(ThemeActualConstants)}");

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


        #region Game event
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
                Description = "Test",
                Action = (mainMenuItem) => { }
            });
#endif

            return mainMenuItems;
        }
        #endregion


        /*
        private void IntegrationUI()
        {
            List<ResourcesList> resourcesLists = new List<ResourcesList>();
            resourcesLists.Add(new ResourcesList { Key = "TM_Image", Value = null });
            resourcesLists.Add(new ResourcesList { Key = "TM_ImageShape", Value = null });
            ui.AddResources(resourcesLists);

            ThemeManifest ThemeInfos = ThemeClass.GetActualTheme(PlayniteApi);
            string pluginFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string ImageName = string.Empty;

            if (ThemeInfos != null && PluginSettings.Settings.EnableIconChanger)
            {
                if (settings.UseIconCircle)
                {
                    ImageName = "circle";
                }
                if (settings.UseIconClock)
                {
                    ImageName = "clock";
                }
                if (settings.UseIconSquareCorne)
                {
                    ImageName = "squareCorne";
                }
                if (settings.UseIconWe4ponx)
                {
                    ImageName = "we4ponx";
                }

                resourcesLists = new List<ResourcesList>();
                resourcesLists.Add(new ResourcesList { Key = "TM_Image", Value = new BitmapImage(new Uri($"{pluginFolder}\\Themes\\Images\\{ImageName}.png")) });
                resourcesLists.Add(new ResourcesList { Key = "TM_ImageShape", Value = new BitmapImage(new Uri($"{pluginFolder}\\Themes\\Images\\{ImageName}Shape.png")) });
                ui.AddResources(resourcesLists);
            }

            if (ThemeInfos != null && PluginSettings.Settings.EnableIconChanger && PluginSettings.Settings.EnableInDescription && File.Exists(Path.Combine(ThemeInfos.DirectoryPath, "apply")))
            {
                try
                {
                    var PART_ControlGameView = CommonPluginsShared.IntegrationUI.SearchElementByName("PART_ControlGameView");
                    var PART_ImageIcon = CommonPluginsShared.IntegrationUI.SearchElementByName("PART_ImageIcon", PART_ControlGameView);
                    var PART_ThemeModifierIcon = CommonPluginsShared.IntegrationUI.SearchElementByName("PART_ThemeModifierIcon", PART_ControlGameView);

                    if (PART_ImageIcon != null || PART_ThemeModifierIcon != null)
                    {
                        double MaxHeight = 10;
#if DEBUG
                        logger.Debug($"ThemeModifier [Ignored] - PART_ImageIcon.MaxHeight: {PART_ImageIcon.MaxHeight} - MaxHeight: {MaxHeight}");
#endif
                        if (!double.IsNaN(PART_ImageIcon.MaxHeight) && (MaxHeight < 11 || double.IsInfinity(MaxHeight)))
                        {
                            MaxHeight = PART_ImageIcon.MaxHeight;
                        }
#if DEBUG
                        logger.Debug($"ThemeModifier [Ignored] - PART_ImageIcon.Height: {PART_ImageIcon.Height} - MaxHeight: {MaxHeight}");
#endif
                        if (!double.IsNaN(PART_ImageIcon.Height) && (MaxHeight < 11 || double.IsInfinity(MaxHeight)))
                        {
                            logger.Debug("Height");
                            MaxHeight = PART_ImageIcon.Height;
                        }
#if DEBUG
                        logger.Debug($"ThemeModifier [Ignored] - PART_ImageIcon.MaxWidth: {PART_ImageIcon.MaxWidth} - MaxHeight: {MaxHeight}");
#endif
                        if (!double.IsNaN(PART_ImageIcon.MaxWidth) && (MaxHeight < 11 || double.IsInfinity(MaxHeight)))
                        {
                            MaxHeight = PART_ImageIcon.MaxWidth;
                        }
#if DEBUG
                        logger.Debug($"ThemeModifier [Ignored] - PART_ImageIcon.Width: {PART_ImageIcon.Width} - MaxHeight: {MaxHeight}");
#endif
                        if (!double.IsNaN(PART_ImageIcon.Width) && (MaxHeight < 11 || double.IsInfinity(MaxHeight)))
                        {
                            MaxHeight = PART_ImageIcon.Width;
                        }

#if DEBUG
                        logger.Debug($"ThemeModifier [Ignored] - MaxHeight: {MaxHeight}");
#endif

                        BitmapImage OriginalSource = new BitmapImage();
                        if (!string.IsNullOrEmpty(GameSelected.Icon))
                        {
                            OriginalSource = new BitmapImage(new Uri(PlayniteApi.Database.GetFullFilePath(GameSelected.Icon)));
                        }
                        else
                        {
                            OriginalSource = (BitmapImage)resources.GetResource("DefaultGameIcon");
                        }

                        Grid gNew = ThemeClass.CreateControl(settings, MaxHeight, ((Image)PART_ImageIcon).Source);
                        gNew.Name = "PART_ThemeModifierIcon";

                        if (gNew != null)
                        {
                            FrameworkElement parent = null;

                            if (PART_ThemeModifierIcon != null)
                            {
#if DEBUG
                                logger.Debug("ThemeModifier [Ignored] - Remove PART_ThemeModifierIcon");
#endif
                                ((Image)PART_ImageIcon).Source = OriginalSource;
                                ((Image)PART_ImageIcon).UpdateLayout();
                            }
                            else if (PART_ImageIcon != null)
                            {
#if DEBUG
                                logger.Debug("ThemeModifier [Ignored] - Remove PART_ImageIcon");
                                logger.Debug($"ThemeModifier [Ignored] - PART_ImageIcon.Parent is {PART_ImageIcon.Parent.ToString()}");
#endif
                                if (PART_ImageIcon.Parent is DockPanel)
                                {
                                    parent = (DockPanel)(PART_ImageIcon.Parent);
                                    ((DockPanel)parent).Children.Remove(PART_ImageIcon);
                                    ((DockPanel)parent).Children.Insert(0, gNew);
                                    ((DockPanel)parent).UpdateLayout();
                                }
                                if (PART_ImageIcon.Parent is StackPanel)
                                {
                                    parent = (StackPanel)(PART_ImageIcon.Parent);
                                    ((StackPanel)parent).Children.Remove(PART_ImageIcon);
                                    ((StackPanel)parent).Children.Insert(0, gNew);
                                    ((StackPanel)parent).UpdateLayout();
                                }
                                if (PART_ImageIcon.Parent is Canvas)
                                {
                                    parent = (Canvas)(PART_ImageIcon.Parent);
                                    ((Canvas)parent).Children.Remove(PART_ImageIcon);
                                    ((Canvas)parent).Children.Insert(0, gNew);
                                    ((Canvas)parent).UpdateLayout();
                                }
                            }
                        }
                        else
                        {
                            logger.Warn("ThemeModifier - ThemeClass.CreateControl() is null");
                        }
                    }
                    else
                    {
                        logger.Warn("ThemeModifier - PART_ImageIcon not find ");
                    }
                }
                catch (Exception ex)
                {
                    Common.LogError(ex, false, "Error integration in Details View");
                }
            }
        }
        */


        #region Game event
        public override void OnGameSelected(GameSelectionEventArgs args)
        {
            /*
            try
            {
                if (args.NewValue != null && args.NewValue.Count == 1)
                {
                    GameSelected = args.NewValue[0];
                    if (PlayniteApi.ApplicationInfo.Mode == ApplicationMode.Desktop)
                    {
                        IntegrationUI();

                        var TaskIntegrationUI = Task.Run(() =>
                        {
                            themeModifierUI.Initial();
                            themeModifierUI.taskHelper.Check();
                            var dispatcherOp = themeModifierUI.AddElements();
                            if (dispatcherOp != null)
                            {
                                dispatcherOp.Completed += (s, e) => { themeModifierUI.RefreshElements(args.NewValue[0]); };
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, $"Error on OnGameSelected()");
            }
            */
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
