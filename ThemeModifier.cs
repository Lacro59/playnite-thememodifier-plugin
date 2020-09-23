using Playnite.Controls;
using Playnite.SDK;
using Playnite.SDK.Events;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using PluginCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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

        private readonly IntegrationUI ui = new IntegrationUI();
        private List<ThemeElement> ThemeDefault = new List<ThemeElement>();

        public override Guid Id { get; } = Guid.Parse("ec2f4013-17e6-428a-b8a9-5e34a3b80009");

        private Game GameSelected { get; set; }


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
               new ExtensionFunction(
                    "ThemeModifier Test",
                    () =>
                    {
                        if (settings.EnableIconChanger)
                        {

                            



                        }
                    })
            };
        }

        // TODO In PluginCommon
        private FrameworkElement SearchElementByName(string ElementName, DependencyObject dpObj = null)
        {
            FrameworkElement ElementFind = null;

            if (dpObj == null)
            {
                dpObj = Application.Current.MainWindow;
            }

            ElementFind = (FrameworkElement)LogicalTreeHelper.FindLogicalNode(dpObj, ElementName);

            if (ElementFind == null)
            {
                foreach (FrameworkElement el in Tools.FindVisualChildren<FrameworkElement>(dpObj))
                {
                    if (el.Name == ElementName)
                    {
                        ElementFind = el;
                        break;
                    }
                }
            }

            return ElementFind;
        }

        private void OnButtonCancelClick(object sender, RoutedEventArgs e)
        {
            string ButtonName = string.Empty;
            try
            {
                ButtonName = ((Button)sender).Name;
                if (ButtonName == "ButtonCancel")
                {
                    if ((string)Tools.FindParent<WindowBase>((Button)sender).GetValue(AutomationProperties.AutomationIdProperty) == "WindowSettings")
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


        public override void OnGameSelected(GameSelectionEventArgs args)
        {
            try
            {
                if (args.NewValue != null && args.NewValue.Count == 1)
                {
                    GameSelected = args.NewValue[0];

                    IntegrationUI();
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemModifier", $"Error on OnGameSelected()");
            }
        }

        private void IntegrationUI()
        {
            List<ResourcesList> resourcesLists = new List<ResourcesList>();
            resourcesLists.Add(new ResourcesList { Key = "TM_Image", Value = null });
            resourcesLists.Add(new ResourcesList { Key = "TM_ImageShape", Value = null });
            ui.AddResources(resourcesLists);

            var ThemeInfos = ThemeClass.GetActualTheme(PlayniteApi.Paths.ConfigurationPath);
            string pluginFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string ImageName = string.Empty;

            if (ThemeInfos != null && settings.EnableIconChanger)
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

            if (ThemeInfos != null && settings.EnableIconChanger && settings.EnableInDescription && File.Exists(Path.Combine(ThemeInfos.DirectoryPath, "apply")))
            {
                try
                {
                    var PART_ControlGameView = SearchElementByName("PART_ControlGameView");
                    var PART_ImageIcon = SearchElementByName("PART_ImageIcon", PART_ControlGameView);
                    var PART_ThemeModifierIcon = SearchElementByName("PART_ThemeModifierIcon", PART_ControlGameView);

                    if (PART_ImageIcon != null || PART_ThemeModifierIcon != null)
                    {
                        double MaxHeight = 10;
#if DEBUG
                        logger.Debug($"ThemeModifier - PART_ImageIcon.MaxHeight: {PART_ImageIcon.MaxHeight} - MaxHeight: {MaxHeight}");
#endif
                        if (!double.IsNaN(PART_ImageIcon.MaxHeight) && (MaxHeight < 11 || double.IsInfinity(MaxHeight)))
                        {
                            MaxHeight = PART_ImageIcon.MaxHeight;
                        }
#if DEBUG
                        logger.Debug($"ThemeModifier - PART_ImageIcon.Height: {PART_ImageIcon.Height} - MaxHeight: {MaxHeight}");
#endif
                        if (!double.IsNaN(PART_ImageIcon.Height) && (MaxHeight < 11 || double.IsInfinity(MaxHeight)))
                        {
                            logger.Debug("Height");
                            MaxHeight = PART_ImageIcon.Height;
                        }
#if DEBUG
                        logger.Debug($"ThemeModifier - PART_ImageIcon.MaxWidth: {PART_ImageIcon.MaxWidth} - MaxHeight: {MaxHeight}");
#endif
                        if (!double.IsNaN(PART_ImageIcon.MaxWidth) && (MaxHeight < 11 || double.IsInfinity(MaxHeight)))
                        {
                            MaxHeight = PART_ImageIcon.MaxWidth;
                        }
#if DEBUG
                        logger.Debug($"ThemeModifier - PART_ImageIcon.Width: {PART_ImageIcon.Width} - MaxHeight: {MaxHeight}");
#endif
                        if (!double.IsNaN(PART_ImageIcon.Width) && (MaxHeight < 11 || double.IsInfinity(MaxHeight)))
                        {
                            MaxHeight = PART_ImageIcon.Width;
                        }

#if DEBUG
                        logger.Debug($"ThemeModifier - MaxHeight: {MaxHeight}");
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
                            // StackPanel
                            FrameworkElement parent = null;

                            if (PART_ThemeModifierIcon != null)
                            {
#if DEBUG
                                logger.Debug("ThemeModifier - Remove PART_ThemeModifierIcon");
#endif
                                ((Image)PART_ImageIcon).Source = OriginalSource;
                                ((Image)PART_ImageIcon).UpdateLayout();
                            }
                            else if (PART_ImageIcon != null)
                            {
#if DEBUG
                                logger.Debug("ThemeModifier - Remove PART_ImageIcon");
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
                    Common.LogError(ex, "ThemeModifier", "Error integration in Details View");
                }
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
            return new ThemeModifierSettingsView(settings, ThemeDefault, PlayniteApi.Paths.ConfigurationPath);
        }
    }
}