using Newtonsoft.Json;
using Playnite.SDK;
using PluginCommon;
using PluginCommon.PlayniteResources;
using PluginCommon.PlayniteResources.API;
using PluginCommon.PlayniteResources.Common;
using PluginCommon.PlayniteResources.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ThemeModifier.Models;
using ThemeModifier.PlayniteResources;

namespace ThemeModifier.Services
{
    public class ThemeClass
    {
        private static ILogger logger = LogManager.GetLogger();
        private static IResourceProvider resources = new ResourceProvider();

        private static List<string> ThemeFileToBackup = new List<string>();


        public static void SetColor(string name, Color? color, ThemeModifierSettings settings, dynamic colorDefault = null)
        {
            try
            {
                IntegrationUI ui = new IntegrationUI();
                List<ResourcesList> resourcesLists = new List<ResourcesList>();

                string colorString = string.Empty;
                if (color != null)
                {
                    colorString = color.ToString();
                    resourcesLists.Add(new ResourcesList { Key = name, Value = new SolidColorBrush((Color)color) });
                }
                else if (colorDefault != null)
                {
                    resourcesLists.Add(new ResourcesList { Key = name, Value = colorDefault });
                }

                ui.AddResources(resourcesLists);

                switch (name)
                {
                    case "ControlBackgroundBrush":
                        settings.ControlBackgroundBrush_Edit = colorString;
                        break;
                    case "TextBrush":
                        settings.TextBrush_Edit = colorString;
                        break;
                    case "TextBrushDarker":
                        settings.TextBrushDarker_Edit = colorString;
                        break;
                    case "TextBrushDark":
                        settings.TextBrushDark_Edit = colorString;
                        break;
                    case "NormalBrush":
                        settings.NormalBrush_Edit = colorString;
                        break;
                    case "NormalBrushDark":
                        settings.NormalBrushDark_Edit = colorString;
                        break;
                    case "NormalBorderBrush":
                        settings.NormalBorderBrush_Edit = colorString;
                        break;
                    case "HoverBrush":
                        settings.HoverBrush_Edit = colorString;
                        break;
                    case "GlyphBrush":
                        settings.GlyphBrush_Edit = colorString;
                        break;
                    case "HighlightGlyphBrush":
                        settings.HighlightGlyphBrush_Edit = colorString;
                        break;
                    case "PopupBorderBrush":
                        settings.PopupBorderBrush_Edit = colorString;
                        break;
                    case "TooltipBackgroundBrush":
                        settings.TooltipBackgroundBrush_Edit = colorString;
                        break;
                    case "ButtonBackgroundBrush":
                        settings.ButtonBackgroundBrush_Edit = colorString;
                        break;
                    case "GridItemBackgroundBrush":
                        settings.GridItemBackgroundBrush_Edit = colorString;
                        break;
                    case "PanelSeparatorBrush":
                        settings.PanelSeparatorBrush_Edit = colorString;
                        break;
                    case "PopupBackgroundBrush":
                        settings.PopupBackgroundBrush_Edit = colorString;
                        break;
                    case "PositiveRatingBrush":
                        settings.PositiveRatingBrush_Edit = colorString;
                        break;
                    case "NegativeRatingBrush":
                        settings.NegativeRatingBrush_Edit = colorString;
                        break;
                    case "MixedRatingBrush":
                        settings.MixedRatingBrush_Edit = colorString;
                        break;
                    case "ExpanderBackgroundBrush":
                        settings.ExpanderBackgroundBrush_Edit = colorString;
                        break;
                    case "WindowBackgourndBrush":
                        settings.WindowBackgourndBrush_Edit = colorString;
                        break;
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemeModifier", "Error on SetColor()");
            }
        }

        public static void RestoreColor(List<ThemeElement> ThemeDefault, ThemeModifierSettings settings, bool withSettings = false)
        {
            try
            {
                if (withSettings)
                {
                    SetThemeSettings(settings);
                }
                else
                {
                    IntegrationUI ui = new IntegrationUI();
                    List<ResourcesList> resourcesLists = new List<ResourcesList>();

                    foreach (ThemeElement themeElement in ThemeDefault)
                    {
                        resourcesLists.Add(new ResourcesList { Key = themeElement.name, Value = themeElement.color });

                        switch (themeElement.name)
                        {
                            case "ControlBackgroundBrush":
                                settings.ControlBackgroundBrush_Edit = string.Empty;
                                break;
                            case "TextBrush":
                                settings.TextBrush_Edit = string.Empty;
                                break;
                            case "TextBrushDarker":
                                settings.TextBrushDarker_Edit = string.Empty;
                                break;
                            case "TextBrushDark":
                                settings.TextBrushDark_Edit = string.Empty;
                                break;
                            case "NormalBrush":
                                settings.NormalBrush_Edit = string.Empty;
                                break;
                            case "NormalBrushDark":
                                settings.NormalBrushDark_Edit = string.Empty;
                                break;
                            case "NormalBorderBrush":
                                settings.NormalBorderBrush_Edit = string.Empty;
                                break;
                            case "HoverBrush":
                                settings.HoverBrush_Edit = string.Empty;
                                break;
                            case "GlyphBrush":
                                settings.GlyphBrush_Edit = string.Empty;
                                break;
                            case "HighlightGlyphBrush":
                                settings.HighlightGlyphBrush_Edit = string.Empty;
                                break;
                            case "PopupBorderBrush":
                                settings.PopupBorderBrush_Edit = string.Empty;
                                break;
                            case "TooltipBackgroundBrush":
                                settings.TooltipBackgroundBrush_Edit = string.Empty;
                                break;
                            case "ButtonBackgroundBrush":
                                settings.ButtonBackgroundBrush_Edit = string.Empty;
                                break;
                            case "GridItemBackgroundBrush":
                                settings.GridItemBackgroundBrush_Edit = string.Empty;
                                break;
                            case "PanelSeparatorBrush":
                                settings.PanelSeparatorBrush_Edit = string.Empty;
                                break;
                            case "PopupBackgroundBrush":
                                settings.PopupBackgroundBrush_Edit = string.Empty;
                                break;
                            case "PositiveRatingBrush":
                                settings.PositiveRatingBrush_Edit = string.Empty;
                                break;
                            case "NegativeRatingBrush":
                                settings.NegativeRatingBrush_Edit = string.Empty;
                                break;
                            case "MixedRatingBrush":
                                settings.MixedRatingBrush_Edit = string.Empty;
                                break;
                        }
                    }

                    ui.AddResources(resourcesLists);
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemeModifier", "Error on RestoreColor()");
            }
        }

        public static void SetThemeSettings(ThemeModifierSettings settings)
        {
            try
            {
                IntegrationUI ui = new IntegrationUI();
                List<ResourcesList> resourcesLists = new List<ResourcesList>();

                string[] TwoColors = null;

                if (!settings.ControlBackgroundBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "ControlBackgroundBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.ControlBackgroundBrush_Edit))
                    });
                }
                if (!settings.TextBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "TextBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.TextBrush_Edit))
                    });
                }
                if (!settings.TextBrushDarker_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "TextBrushDarker",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.TextBrushDarker_Edit))
                    });
                }
                if (!settings.TextBrushDark_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "TextBrushDark",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.TextBrushDark_Edit))
                    });
                }
                if (!settings.NormalBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "NormalBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.NormalBrush_Edit))
                    });
                }
                if (!settings.NormalBrushDark_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "NormalBrushDark",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.NormalBrushDark_Edit))
                    });
                }
                if (!settings.NormalBorderBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "NormalBorderBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.NormalBorderBrush_Edit))
                    });
                }
                if (!settings.HoverBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "HoverBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.HoverBrush_Edit))
                    });
                }
                if (!settings.GlyphBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "GlyphBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.GlyphBrush_Edit))
                    });
                }
                if (!settings.HighlightGlyphBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "HighlightGlyphBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.HighlightGlyphBrush_Edit))
                    });
                }
                if (!settings.PopupBorderBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "PopupBorderBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.PopupBorderBrush_Edit))
                    });
                }
                if (!settings.TooltipBackgroundBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "TooltipBackgroundBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.TooltipBackgroundBrush_Edit))
                    });
                }
                if (!settings.ButtonBackgroundBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "ButtonBackgroundBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.ButtonBackgroundBrush_Edit))
                    });
                }
                if (!settings.GridItemBackgroundBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "GridItemBackgroundBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.GridItemBackgroundBrush_Edit))
                    });
                }
                if (!settings.PanelSeparatorBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "PanelSeparatorBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.PanelSeparatorBrush_Edit))
                    });
                }
                if (!settings.PopupBackgroundBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "PopupBackgroundBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.PopupBackgroundBrush_Edit))
                    });
                }
                if (!settings.PositiveRatingBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "PositiveRatingBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.PositiveRatingBrush_Edit))
                    });
                }
                if (!settings.NegativeRatingBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "NegativeRatingBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.NegativeRatingBrush_Edit))
                    });
                }
                if (!settings.MixedRatingBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "MixedRatingBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.MixedRatingBrush_Edit))
                    });
                }

                if (!settings.ExpanderBackgroundBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "ExpanderBackgroundBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.ExpanderBackgroundBrush_Edit))
                    });
                }
                if (!settings.WindowBackgourndBrush_Edit.IsNullOrEmpty())
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "WindowBackgourndBrush",
                        Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.WindowBackgourndBrush_Edit))
                    });
                }

                ui.AddResources(resourcesLists);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemeModifier", "Error on SetThemeSettings()");
            }
        }



        public static ThemeManifest GetActualTheme(string PlayniteConfigurationPath)
        {
            string path = Path.Combine(PlayniteConfigurationPath, "config.json");
            string ThemeName = string.Empty;

            // Get actual theme
            try
            {
                if (File.Exists(path))
                {
                    ThemeName = ((dynamic)JsonConvert.DeserializeObject(File.ReadAllText(path))).Theme;

                    var AllThemeInfos = ThemeManager.GetAvailableThemes(ApplicationMode.Desktop);
#if DEBUG
                    logger.Debug($"ThemeModifier - {JsonConvert.SerializeObject(AllThemeInfos)}");
#endif
                    //
                    ThemeManifest ThemeInfo = AllThemeInfos.Find(x => x.Name.ToLower() == ThemeName.ToLower());
                    if (ThemeInfo != null)
                    {
                        return ThemeInfo;
                    }
                    ThemeInfo = AllThemeInfos.Find(x => x.LegacyDirId.ToLower() == ThemeName.ToLower());
                    if (ThemeInfo != null)
                    {
                        return ThemeInfo;
                    }

                    ThemeInfo = AllThemeInfos.Find(x => x.DirectoryPath.ToLower().IndexOf(ThemeName.ToLower()) > -1);
                    if (ThemeInfo != null)
                    {
                        return ThemeInfo;
                    }

                    logger.Warn($"ThemeModifier - No ThemeManifest find for {ThemeName}");

                    return null;
                }
                else
                {
                    logger.Warn($"ThemeModifier - Not find config file {path}");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"ThemeModifier - Failed to load config file {path}");
            }

            return null;
        }

        public static bool SetThemeFile(string PlayniteConfigurationPath, ThemeModifierSettings settings)
        {
            string pluginFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ThemeManifest ThemeInfos = GetActualTheme(PlayniteConfigurationPath);

            ThemeFileToBackup = new List<string>();

            ThemeFileToBackup.Add("DerivedStyles\\DetailsViewItemTemplate.xaml");

            if (ThemeInfos != null)
            {
                try
                {
                    foreach (string FileName in ThemeFileToBackup)
                    {
                        // Backup
                        if (!File.Exists(Path.Combine(ThemeInfos.DirectoryPath, FileName.Replace(".xaml", ".xaml.bck"))) 
                            && File.Exists(Path.Combine(ThemeInfos.DirectoryPath, FileName))
                            && !File.Exists(Path.Combine(ThemeInfos.DirectoryPath, "apply")))
                        {
                            File.Move(Path.Combine(ThemeInfos.DirectoryPath, FileName), Path.Combine(ThemeInfos.DirectoryPath, FileName.Replace(".xaml", ".xaml.bck")));
                        }

                        // Delete
                        if (File.Exists(Path.Combine(ThemeInfos.DirectoryPath, FileName)))
                        {
                            File.Delete(Path.Combine(ThemeInfos.DirectoryPath, FileName));
                        }

                        if (!Directory.Exists(Path.Combine(ThemeInfos.DirectoryPath, "DerivedStyles")))
                        {
                            Directory.CreateDirectory(Path.Combine(ThemeInfos.DirectoryPath, "DerivedStyles"));
                        }

                        // Copy
                        if (File.Exists(Path.Combine(pluginFolder, "Themes", FileName)))
                        {
                            File.Create(Path.Combine(ThemeInfos.DirectoryPath, "apply"));
                            File.Copy(Path.Combine(pluginFolder, "Themes", FileName), Path.Combine(ThemeInfos.DirectoryPath, FileName));
                        }
                        else
                        {
                            logger.Error($"ThemeModifier - File {Path.Combine(pluginFolder, "Themes", FileName)} not found");
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"ThemeModifier - Failed to set file");
                    return false;
                }
            }
            else
            {
                logger.Warn($"ThemeModifier - No ThemeManifest find for actual theme");
                return false;
            }

            return true;
        }

        public static bool RestoreThemeFile(string PlayniteConfigurationPath) {
            ThemeManifest ThemeInfos = GetActualTheme(PlayniteConfigurationPath);

            ThemeFileToBackup = new List<string>();
            ThemeFileToBackup.Add("DerivedStyles\\DetailsViewItemTemplate.xaml");

            if (ThemeInfos != null)
            {
                try
                {
                    foreach (string FileName in ThemeFileToBackup)
                    {
                        // Delete
                        if (File.Exists(Path.Combine(ThemeInfos.DirectoryPath, FileName)))
                        {
                            File.Delete(Path.Combine(ThemeInfos.DirectoryPath, FileName));
                        }
                        if (File.Exists(Path.Combine(ThemeInfos.DirectoryPath, "apply")))
                        {
                            File.Delete(Path.Combine(ThemeInfos.DirectoryPath, "apply"));
                        }

                        // Restore
                        if (File.Exists(Path.Combine(ThemeInfos.DirectoryPath, FileName.Replace(".xaml", ".xaml.bck"))))
                        {
                            File.Move(Path.Combine(ThemeInfos.DirectoryPath, FileName.Replace(".xaml", ".xaml.bck")), Path.Combine(ThemeInfos.DirectoryPath, FileName));
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"ThemeModifier - Failed to set file");
                    return false;
                }
            }
            else
            {
                logger.Warn($"ThemeModifier - No ThemeManifest find for actual theme");
                return false;
            }

            return true;
        }



        public static Grid CreateControl(ThemeModifierSettings settings, double MaxHeight, ImageSource OriginalSource)
        {
            try
            {
                string pluginFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string ImageName = string.Empty;

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


                Grid g = new Grid();
                g.Margin = new Thickness(0, 0, 10, 0);
                DockPanel.SetDock(g, Dock.Left);

                Image PART_ImageFrame = new Image();
                PART_ImageFrame.Name = "PART_ImageFrame";
                PART_ImageFrame.MaxHeight = MaxHeight;
                PART_ImageFrame.MaxWidth = MaxHeight;
                PART_ImageFrame.Source = new BitmapImage(new Uri($"{pluginFolder}\\Themes\\Images\\{ImageName}.png"));
                RenderOptions.SetBitmapScalingMode(PART_ImageFrame, BitmapScalingMode.Fant);

                Image PART_ImageIcon = new Image();
                PART_ImageIcon.Name = "PART_ImageIcon";
                PART_ImageIcon.Source = OriginalSource;
                PART_ImageIcon.Stretch = Stretch.Fill;
                PART_ImageIcon.MaxHeight = MaxHeight;
                PART_ImageIcon.MaxWidth = MaxHeight;
                RenderOptions.SetBitmapScalingMode(PART_ImageFrame, BitmapScalingMode.Fant);

                ImageBrush imgB = new ImageBrush();
                imgB.ImageSource = new BitmapImage(new Uri($"{pluginFolder}\\Themes\\Images\\{ImageName}Shape.png"));

                PART_ImageIcon.OpacityMask = imgB;

                Grid.SetColumn(PART_ImageFrame, 0);
                Grid.SetColumn(PART_ImageIcon, 0);

                g.Children.Add(PART_ImageFrame);
                g.Children.Add(PART_ImageIcon);

                return g;
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemeModifier", "Error on CreateControl()");
            }

            return null;
        }
    }
}
