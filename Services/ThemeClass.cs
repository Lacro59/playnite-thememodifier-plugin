using Newtonsoft.Json;
using Playnite.SDK;
using CommonPluginsShared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ThemeModifier.Models;
using ThemeModifier.Views;
using YamlDotNet.Serialization;
using CommonPluginsPlaynite;
using CommonPluginsPlaynite.Common;
using System.Text.RegularExpressions;
using CommonPlayniteShared.Manifests;

namespace ThemeModifier.Services
{
    public class ThemeClass
    {
        private static ILogger logger = LogManager.GetLogger();
        private static IResourceProvider resources = new ResourceProvider();

        public static readonly List<string> ThemeVariables = new List<string>
        {
            "ControlBackgroundBrush",
            "TextBrush",
            "TextBrushDarker",
            "TextBrushDark",
            "NormalBrush",
            "NormalBrushDark",
            "NormalBorderBrush",
            "HoverBrush",
            "GlyphBrush",
            "HighlightGlyphBrush",
            "PopupBorderBrush",
            "TooltipBackgroundBrush",
            "ButtonBackgroundBrush",
            "GridItemBackgroundBrush",
            "PanelSeparatorBrush",
            "PopupBackgroundBrush",
            "PositiveRatingBrush",
            "NegativeRatingBrush",
            "MixedRatingBrush",
            "ExpanderBackgroundBrush",
            "WindowBackgourndBrush",
            "WarningBrush"
        };

        private static List<string> ThemeFileToBackup = new List<string>();

        #region Theme colors
        public static List<ThemeElement> GetThemeDefault()
        {
            List<ThemeElement> ThemeDefault = new List<ThemeElement>();

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "ControlBackgroundBrush", Element = resources.GetResource("ControlBackgroundBrush") });
            }
            catch
            {
                logger.Warn($"Resources don't exists ControlBackgroundBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "TextBrush", Element = resources.GetResource("TextBrush") });
            }
            catch
            {
                logger.Warn($"Resources don't exists TextBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "TextBrushDarker", Element = resources.GetResource("TextBrushDarker") });
            }
            catch
            {
                logger.Warn($"Resources don't exists TextBrushDarker");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "TextBrushDark", Element = resources.GetResource("TextBrushDark") });
            }
            catch
            {
                logger.Warn($"Resources don't exists TextBrushDark");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "NormalBrush", Element = resources.GetResource("NormalBrush") });
            }
            catch
            {
                logger.Warn($"Resources don't exists NormalBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "NormalBrushDark", Element = resources.GetResource("NormalBrushDark") });
            }
            catch
            {
                logger.Warn($"Resources don't exists NormalBrushDark");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "NormalBorderBrush", Element = resources.GetResource("NormalBorderBrush") });
            }
            catch
            {
                logger.Warn($"Resources don't exists NormalBorderBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "HoverBrush", Element = resources.GetResource("HoverBrush") });
            }
            catch
            {
                logger.Warn($"Resources don't exists HoverBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "GlyphBrush", Element = resources.GetResource("GlyphBrush") });
            }
            catch
            {
                logger.Warn($"Resources don't exists GlyphBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "HighlightGlyphBrush", Element = resources.GetResource("HighlightGlyphBrush") });
            }
            catch
            {
                logger.Warn($"Resources don't exists HighlightGlyphBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "PopupBorderBrush", Element = resources.GetResource("PopupBorderBrush") });
            }
            catch
            {
                logger.Warn($"Resources don't exists PopupBorderBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "TooltipBackgroundBrush", Element = resources.GetResource("TooltipBackgroundBrush") });
            }
            catch
            {
                logger.Warn($"ThemeModifier - Resources don't exists TooltipBackgroundBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "ButtonBackgroundBrush", Element = resources.GetResource("ButtonBackgroundBrush") });
            }
            catch
            {
                logger.Warn($"Resources don't exists ButtonBackgroundBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "GridItemBackgroundBrush", Element = resources.GetResource("GridItemBackgroundBrush") });
            }
            catch
            {
                logger.Warn($"ThemeModifier - Resources don't exists GridItemBackgroundBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "PanelSeparatorBrush", Element = resources.GetResource("PanelSeparatorBrush") });
            }
            catch
            {
                logger.Warn($"Resources don't exists PanelSeparatorBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "PopupBackgroundBrush", Element = resources.GetResource("PopupBackgroundBrush") });
            }
            catch
            {
                logger.Warn($"Resources don't exists PopupBackgroundBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "PositiveRatingBrush", Element = resources.GetResource("PositiveRatingBrush") });
            }
            catch
            {
                logger.Warn($"Resources don't exists PositiveRatingBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "NegativeRatingBrush", Element = resources.GetResource("NegativeRatingBrush") });
            }
            catch
            {
                logger.Warn($"Resources don't exists NegativeRatingBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "MixedRatingBrush", Element = resources.GetResource("MixedRatingBrush") });
            }
            catch
            {
                logger.Warn($"Resources don't exists MixedRatingBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "ExpanderBackgroundBrush", Element = resources.GetResource("ExpanderBackgroundBrush") });
            }
            catch
            {
                logger.Warn($"ThemeModifier - Resources don't exists ExpanderBackgroundBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "WindowBackgourndBrush", Element = resources.GetResource("WindowBackgourndBrush") });
            }
            catch
            {
                logger.Warn($"Resources don't exists WindowBackgourndBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "WarningBrush", Element = resources.GetResource("WarningBrush") });
            }
            catch
            {
                logger.Warn($"Resources don't exists WarningBrush");
            }

            return ThemeDefault;
        }

        public static void SetThemeColor(string name, dynamic color, ThemeModifierSettings settings, dynamic colorDefault = null)
        {
            try
            {
                UI ui = new UI();
                List<ResourcesList> resourcesLists = new List<ResourcesList>();

                string colorString = string.Empty;
                ThemeLinearGradient themeLinearGradient = new ThemeLinearGradient();

                if (color != null)
                {
                    if (color is ThemeLinearGradient)
                    {
                        color = color.ToLinearGradientBrush;
                    }

                    if (color is SolidColorBrush)
                    {
                        colorString = ((SolidColorBrush)color).Color.ToString() + "-" + ((SolidColorBrush)color).Opacity.ToString();
                        resourcesLists.Add(new ResourcesList { Key = name, Value = color });
                    }
                    else if (color is LinearGradientBrush)
                    {
                        resourcesLists.Add(new ResourcesList { Key = name, Value = color });

                        themeLinearGradient = new ThemeLinearGradient
                        {
                            StartPoint = color.StartPoint,
                            EndPoint = color.EndPoint,
                            GradientStop1 = new ThemeGradientColor
                            {
                                ColorString = color.GradientStops[0].Color.ToString(),
                                ColorOffset = color.GradientStops[0].Offset
                            },
                            GradientStop2 = new ThemeGradientColor
                            {
                                ColorString = color.GradientStops[1].Color.ToString(),
                                ColorOffset = color.GradientStops[1].Offset
                            }
                        };
                    }
                    else
                    {
                        logger.Warn($"Color is {color.toString()}");
                    }
                }
                else if (colorDefault != null)
                {
                    resourcesLists.Add(new ResourcesList { Key = name, Value = colorDefault });
                }
                else
                {
                    logger.Warn($"No default color");
                }


                ui.AddResources(resourcesLists);


                switch (name)
                {
                    case "ControlBackgroundBrush":
                        settings.ControlBackgroundBrush_Edit = colorString;
                        settings.ControlBackgroundBrush_EditGradient = themeLinearGradient;
                        break;
                    case "TextBrush":
                        settings.TextBrush_Edit = colorString;
                        settings.TextBrush_EditGradient = themeLinearGradient;
                        break;
                    case "TextBrushDarker":
                        settings.TextBrushDarker_Edit = colorString;
                        settings.TextBrushDarker_EditGradient = themeLinearGradient;
                        break;
                    case "TextBrushDark":
                        settings.TextBrushDark_Edit = colorString;
                        settings.TextBrushDark_EditGradient = themeLinearGradient;
                        break;
                    case "NormalBrush":
                        settings.NormalBrush_Edit = colorString;
                        settings.NormalBrush_EditGradient = themeLinearGradient;
                        break;
                    case "NormalBrushDark":
                        settings.NormalBrushDark_Edit = colorString;
                        settings.NormalBrushDark_EditGradient = themeLinearGradient;
                        break;
                    case "NormalBorderBrush":
                        settings.NormalBorderBrush_Edit = colorString;
                        settings.NormalBorderBrush_EditGradient = themeLinearGradient;
                        break;
                    case "HoverBrush":
                        settings.HoverBrush_Edit = colorString;
                        settings.HoverBrush_EditGradient = themeLinearGradient;
                        break;
                    case "GlyphBrush":
                        settings.GlyphBrush_Edit = colorString;
                        settings.GlyphBrush_EditGradient = themeLinearGradient;
                        break;
                    case "HighlightGlyphBrush":
                        settings.HighlightGlyphBrush_Edit = colorString;
                        settings.HighlightGlyphBrush_EditGradient = themeLinearGradient;
                        break;
                    case "PopupBorderBrush":
                        settings.PopupBorderBrush_Edit = colorString;
                        settings.PopupBorderBrush_EditGradient = themeLinearGradient;
                        break;
                    case "TooltipBackgroundBrush":
                        settings.TooltipBackgroundBrush_Edit = colorString;
                        settings.TooltipBackgroundBrush_EditGradient = themeLinearGradient;
                        break;
                    case "ButtonBackgroundBrush":
                        settings.ButtonBackgroundBrush_Edit = colorString;
                        settings.ButtonBackgroundBrush_EditGradient = themeLinearGradient;
                        break;
                    case "GridItemBackgroundBrush":
                        settings.GridItemBackgroundBrush_Edit = colorString;
                        settings.GridItemBackgroundBrush_EditGradient = themeLinearGradient;
                        break;
                    case "PanelSeparatorBrush":
                        settings.PanelSeparatorBrush_Edit = colorString;
                        settings.PanelSeparatorBrush_EditGradient = themeLinearGradient;
                        break;
                    case "PopupBackgroundBrush":
                        settings.PopupBackgroundBrush_Edit = colorString;
                        settings.PopupBackgroundBrush_EditGradient = themeLinearGradient;
                        break;
                    case "PositiveRatingBrush":
                        settings.PositiveRatingBrush_Edit = colorString;
                        settings.PositiveRatingBrush_EditGradient = themeLinearGradient;
                        break;
                    case "NegativeRatingBrush":
                        settings.NegativeRatingBrush_Edit = colorString;
                        settings.NegativeRatingBrush_EditGradient = themeLinearGradient;
                        break;
                    case "MixedRatingBrush":
                        settings.MixedRatingBrush_Edit = colorString;
                        settings.MixedRatingBrush_EditGradient = themeLinearGradient;
                        break;
                    case "ExpanderBackgroundBrush":
                        settings.ExpanderBackgroundBrush_Edit = colorString;
                        settings.ExpanderBackgroundBrush_EditGradient = themeLinearGradient;
                        break;
                    case "WindowBackgourndBrush":
                        settings.WindowBackgourndBrush_Edit = colorString;
                        settings.WindowBackgourndBrush_EditGradient = themeLinearGradient;
                        break;
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, "Error on SetThemeColor()");
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
                    UI ui = new UI();
                    List<ResourcesList> resourcesLists = new List<ResourcesList>();

                    foreach (ThemeElement themeElement in ThemeDefault)
                    {
                        resourcesLists.Add(new ResourcesList { Key = themeElement.Name, Value = themeElement.Element });

                        ThemeLinearGradient themeLinearGradient = new ThemeLinearGradient();

                        switch (themeElement.Name)
                        {
                            case "ControlBackgroundBrush":
                                settings.ControlBackgroundBrush_Edit = string.Empty;
                                settings.ControlBackgroundBrush_EditGradient = themeLinearGradient;
                                break;
                            case "TextBrush":
                                settings.TextBrush_Edit = string.Empty;
                                settings.TextBrush_EditGradient = themeLinearGradient;
                                break;
                            case "TextBrushDarker":
                                settings.TextBrushDarker_Edit = string.Empty;
                                settings.TextBrushDarker_EditGradient = themeLinearGradient;
                                break;
                            case "TextBrushDark":
                                settings.TextBrushDark_Edit = string.Empty;
                                settings.TextBrushDark_EditGradient = themeLinearGradient;
                                break;
                            case "NormalBrush":
                                settings.NormalBrush_Edit = string.Empty;
                                settings.NormalBrush_EditGradient = themeLinearGradient;
                                break;
                            case "NormalBrushDark":
                                settings.NormalBrushDark_Edit = string.Empty;
                                settings.NormalBrushDark_EditGradient = themeLinearGradient;
                                break;
                            case "NormalBorderBrush":
                                settings.NormalBorderBrush_Edit = string.Empty;
                                settings.NormalBorderBrush_EditGradient = themeLinearGradient;
                                break;
                            case "HoverBrush":
                                settings.HoverBrush_Edit = string.Empty;
                                settings.HoverBrush_EditGradient = themeLinearGradient;
                                break;
                            case "GlyphBrush":
                                settings.GlyphBrush_Edit = string.Empty;
                                settings.GlyphBrush_EditGradient = themeLinearGradient;
                                break;
                            case "HighlightGlyphBrush":
                                settings.HighlightGlyphBrush_Edit = string.Empty;
                                settings.HighlightGlyphBrush_EditGradient = themeLinearGradient;
                                break;
                            case "PopupBorderBrush":
                                settings.PopupBorderBrush_Edit = string.Empty;
                                settings.PopupBorderBrush_EditGradient = themeLinearGradient;
                                break;
                            case "TooltipBackgroundBrush":
                                settings.TooltipBackgroundBrush_Edit = string.Empty;
                                settings.TooltipBackgroundBrush_EditGradient = themeLinearGradient;
                                break;
                            case "ButtonBackgroundBrush":
                                settings.ButtonBackgroundBrush_Edit = string.Empty;
                                settings.ButtonBackgroundBrush_EditGradient = themeLinearGradient;
                                break;
                            case "GridItemBackgroundBrush":
                                settings.GridItemBackgroundBrush_Edit = string.Empty;
                                settings.GridItemBackgroundBrush_EditGradient = themeLinearGradient;
                                break;
                            case "PanelSeparatorBrush":
                                settings.PanelSeparatorBrush_Edit = string.Empty;
                                settings.PanelSeparatorBrush_EditGradient = themeLinearGradient;
                                break;
                            case "PopupBackgroundBrush":
                                settings.PopupBackgroundBrush_Edit = string.Empty;
                                settings.PopupBackgroundBrush_EditGradient = themeLinearGradient;
                                break;
                            case "PositiveRatingBrush":
                                settings.PositiveRatingBrush_Edit = string.Empty;
                                settings.PositiveRatingBrush_EditGradient = themeLinearGradient;
                                break;
                            case "NegativeRatingBrush":
                                settings.NegativeRatingBrush_Edit = string.Empty;
                                settings.NegativeRatingBrush_EditGradient = themeLinearGradient;
                                break;
                            case "MixedRatingBrush":
                                settings.MixedRatingBrush_Edit = string.Empty;
                                settings.MixedRatingBrush_EditGradient = themeLinearGradient;
                                break;
                            case "WarningBrush":
                                settings.WarningBrush_Edit = string.Empty;
                                settings.WarningBrush_EditGradient = themeLinearGradient;
                                break;
                        }
                    }

                    ui.AddResources(resourcesLists);
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, "Error on RestoreColor()");
            }
        }

        public static void SetThemeSettings(ThemeModifierSettings settings)
        {
            try
            {
                UI ui = new UI();
                List<ResourcesList> resourcesLists = new List<ResourcesList>();

                SolidColorBrush color = new SolidColorBrush();

                if (!settings.ControlBackgroundBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.ControlBackgroundBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.ControlBackgroundBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.ControlBackgroundBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.ControlBackgroundBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "ControlBackgroundBrush",
                        Value = color
                    });
                }
                if (settings.ControlBackgroundBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "ControlBackgroundBrush",
                        Value = settings.ControlBackgroundBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.TextBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.TextBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.TextBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.TextBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.TextBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "TextBrush",
                        Value = color
                    });
                }
                if (settings.TextBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "TextBrush",
                        Value = settings.TextBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.TextBrushDarker_Edit.IsNullOrEmpty())
                {
                    if (settings.TextBrushDarker_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.TextBrushDarker_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.TextBrushDarker_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.TextBrushDarker_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "TextBrushDarker",
                        Value = color
                    });
                }
                if (settings.TextBrushDarker_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "TextBrushDarker",
                        Value = settings.TextBrushDarker_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.TextBrushDark_Edit.IsNullOrEmpty())
                {
                    if (settings.TextBrushDark_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.TextBrushDark_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.TextBrushDark_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.TextBrushDark_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "TextBrushDark",
                        Value = color
                    });
                }
                if (settings.TextBrushDark_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "TextBrushDark",
                        Value = settings.TextBrushDark_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.NormalBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.NormalBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.NormalBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.NormalBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.NormalBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "NormalBrush",
                        Value = color
                    });
                }
                if (settings.NormalBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "NormalBrush",
                        Value = settings.NormalBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.NormalBrushDark_Edit.IsNullOrEmpty())
                {
                    if (settings.NormalBrushDark_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.NormalBrushDark_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.NormalBrushDark_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.NormalBrushDark_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "NormalBrushDark",
                        Value = color
                    });
                }
                if (settings.NormalBrushDark_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "NormalBrushDark",
                        Value = settings.NormalBrushDark_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.NormalBorderBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.NormalBorderBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.NormalBorderBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.NormalBorderBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.NormalBorderBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "NormalBorderBrush",
                        Value = color
                    });
                }
                if (settings.NormalBorderBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "NormalBorderBrush",
                        Value = settings.NormalBorderBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.HoverBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.HoverBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.HoverBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.HoverBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.HoverBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "HoverBrush",
                        Value = color
                    });
                }
                if (settings.HoverBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "HoverBrush",
                        Value = settings.HoverBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.GlyphBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.GlyphBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.GlyphBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.GlyphBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.GlyphBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "GlyphBrush",
                        Value = color
                    });
                }
                if (settings.GlyphBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "GlyphBrush",
                        Value = settings.GlyphBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.HighlightGlyphBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.HighlightGlyphBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.HighlightGlyphBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.HighlightGlyphBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.HighlightGlyphBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "HighlightGlyphBrush",
                        Value = color
                    });
                }
                if (settings.HighlightGlyphBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "HighlightGlyphBrush",
                        Value = settings.HighlightGlyphBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.PopupBorderBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.PopupBorderBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.PopupBorderBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.PopupBorderBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.PopupBorderBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "PopupBorderBrush",
                        Value = color
                    });
                }
                if (settings.PopupBorderBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "PopupBorderBrush",
                        Value = settings.PopupBorderBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.TooltipBackgroundBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.TooltipBackgroundBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.TooltipBackgroundBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.TooltipBackgroundBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.TooltipBackgroundBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "TooltipBackgroundBrush",
                        Value = color
                    });
                }
                if (settings.TooltipBackgroundBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "TooltipBackgroundBrush",
                        Value = settings.TooltipBackgroundBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.ButtonBackgroundBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.ButtonBackgroundBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.ButtonBackgroundBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.ButtonBackgroundBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.ButtonBackgroundBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "ButtonBackgroundBrush",
                        Value = color
                    });
                }
                if (settings.ButtonBackgroundBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "ButtonBackgroundBrush",
                        Value = settings.ButtonBackgroundBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.GridItemBackgroundBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.GridItemBackgroundBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.GridItemBackgroundBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.GridItemBackgroundBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.GridItemBackgroundBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "GridItemBackgroundBrush",
                        Value = color
                    });
                }
                if (settings.GridItemBackgroundBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "GridItemBackgroundBrush",
                        Value = settings.GridItemBackgroundBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.PanelSeparatorBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.PanelSeparatorBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.PanelSeparatorBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.PanelSeparatorBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.PanelSeparatorBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "PanelSeparatorBrush",
                        Value = color
                    });
                }
                if (settings.PanelSeparatorBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "GridPanelSeparatorBrushItemBackgroundBrush",
                        Value = settings.PanelSeparatorBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.PopupBackgroundBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.PopupBackgroundBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.PopupBackgroundBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.PopupBackgroundBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.PopupBackgroundBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "PopupBackgroundBrush",
                        Value = color
                    });
                }
                if (settings.PopupBackgroundBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "PopupBackgroundBrush",
                        Value = settings.PopupBackgroundBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.PositiveRatingBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.PositiveRatingBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.PositiveRatingBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.PositiveRatingBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.PositiveRatingBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "PositiveRatingBrush",
                        Value = color
                    });
                }
                if (settings.PositiveRatingBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "PositiveRatingBrush",
                        Value = settings.PositiveRatingBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.NegativeRatingBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.NegativeRatingBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.NegativeRatingBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.NegativeRatingBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.NegativeRatingBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "NegativeRatingBrush",
                        Value = color
                    });
                }
                if (settings.NegativeRatingBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "NegativeRatingBrush",
                        Value = settings.NegativeRatingBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.MixedRatingBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.MixedRatingBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.MixedRatingBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.MixedRatingBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.MixedRatingBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "MixedRatingBrush",
                        Value = color
                    });
                }
                if (settings.MixedRatingBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "MixedRatingBrush",
                        Value = settings.MixedRatingBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.ExpanderBackgroundBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.ExpanderBackgroundBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.ExpanderBackgroundBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.ExpanderBackgroundBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.ExpanderBackgroundBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "ExpanderBackgroundBrush",
                        Value = color
                    });
                }
                if (settings.ExpanderBackgroundBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "ExpanderBackgroundBrush",
                        Value = settings.ExpanderBackgroundBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.WindowBackgourndBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.WindowBackgourndBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.WindowBackgourndBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.WindowBackgourndBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.WindowBackgourndBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "WindowBackgourndBrush",
                        Value = color
                    });
                }
                if (settings.WindowBackgourndBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "WindowBackgourndBrush",
                        Value = settings.WindowBackgourndBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.WarningBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.WarningBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.WarningBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.WarningBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.WarningBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "WarningBrush",
                        Value = color
                    });
                }
                if (settings.WarningBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "WarningBrush",
                        Value = settings.WarningBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                ui.AddResources(resourcesLists);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, "Error on SetThemeSettings()");
            }
        }
        #endregion

        #region Theme icons
        public static ThemeManifest GetActualTheme(IPlayniteAPI PlayniteApi)
        {
            PlayniteTools.SetThemeInformation(PlayniteApi);
            return ThemeManager.CurrentTheme;
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


                Grid g = new Grid
                {
                    Margin = new Thickness(0, 0, 10, 0)
                };
                DockPanel.SetDock(g, Dock.Left);

                Image PART_ImageFrame = new Image
                {
                    Name = "PART_ImageFrame",
                    MaxHeight = MaxHeight,
                    MaxWidth = MaxHeight,
                    Source = new BitmapImage(new Uri($"{pluginFolder}\\Themes\\Images\\{ImageName}.png"))
                };
                RenderOptions.SetBitmapScalingMode(PART_ImageFrame, BitmapScalingMode.Fant);

                Image PART_ImageIcon = new Image
                {
                    Name = "PART_ImageIcon",
                    Source = OriginalSource,
                    Stretch = Stretch.Fill,
                    MaxHeight = MaxHeight,
                    MaxWidth = MaxHeight
                };
                RenderOptions.SetBitmapScalingMode(PART_ImageFrame, BitmapScalingMode.Fant);

                ImageBrush imgB = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri($"{pluginFolder}\\Themes\\Images\\{ImageName}Shape.png"))
                };

                PART_ImageIcon.OpacityMask = imgB;

                Grid.SetColumn(PART_ImageFrame, 0);
                Grid.SetColumn(PART_ImageIcon, 0);

                g.Children.Add(PART_ImageFrame);
                g.Children.Add(PART_ImageIcon);

                return g;
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, "Error on CreateControl()");
            }

            return null;
        }
        #endregion

        #region Theme manage
        public static void LoadThemeColors(string PathFileName, ThemeModifierSettings settings, ThemeModifierSettingsView settingsView)
        {
            ThemeColors themeColors = JsonConvert.DeserializeObject<ThemeColors>(File.ReadAllText(PathFileName));
            LoadThemeColors(themeColors, settings, settingsView);
        }
        public static void LoadThemeColors(ThemeColors themeColors, ThemeModifierSettings settings, ThemeModifierSettingsView settingsView)
        {
            foreach(ThemeColorsElement themeColorsElement in themeColors.ThemeColorsElements)
            {
                var control = settingsView.FindName("tb" + themeColorsElement.Name);
                if (control is TextBlock)
                {
                    dynamic color = null;

                    if (!themeColorsElement.ColorString.IsNullOrEmpty())
                    {
                        if (themeColorsElement.ColorString.IndexOf("-") > -1)
                        {
                            color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(themeColorsElement.ColorString.Split('-')[0]));
                            color.Opacity = double.Parse(themeColorsElement.ColorString.Split('-')[1]);
                        }
                        else
                        {
                            color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(themeColorsElement.ColorString));
                        }
                    }
                    if (themeColorsElement.ColorLinear.GradientStop1 != null)
                    {
                        color = themeColorsElement.ColorLinear.ToLinearGradientBrush;
                    }

                    ((TextBlock)control).Background = color;
                    ThemeClass.SetThemeColor(themeColorsElement.Name, color, settings, null);
                }
                else
                {
                    logger.Warn($"Bad control {"tb" + themeColorsElement.Name}: {control.ToString()}");
                }
            }
        }

        public static List<ThemeColors> GetListThemeColors(string PluginUserDataPath)
        {
            List<ThemeColors> ListThemeColors = new List<ThemeColors>();

            string PathThemeColors = Path.Combine(PluginUserDataPath, "ThemeColors");

            if (!Directory.Exists(PathThemeColors))
            {
                Directory.CreateDirectory(PathThemeColors);
            }

            Parallel.ForEach(Directory.EnumerateFiles(PathThemeColors, "*.json"), (objectFile) =>
            {
                Common.LogDebug(true, $"GetListThemeColors() - {objectFile}");

                try
                {
                    ThemeColors themeColors = JsonConvert.DeserializeObject<ThemeColors>(File.ReadAllText(objectFile));
                    themeColors.FileName = objectFile;
                    ListThemeColors.Add(themeColors);
                }
                catch (Exception ex)
                {
                    Common.LogError(ex, false, $"Error to parse file {objectFile}");
                }
            });

            Common.LogDebug(true, $"GetListThemeColors() - {JsonConvert.SerializeObject(ListThemeColors)}");

            return ListThemeColors;
        }

        public static void DeleteThemeColors(string PathFileName, string PluginUserDataPath)
        {
            try
            {
                    File.Delete(PathFileName);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, $"Error to delete file {PathFileName}");
            }
        }
        public static void DeleteThemeColors(ThemeColors themeColors, string PluginUserDataPath)
        {
            string PathThemeColors = Path.Combine(PluginUserDataPath, "ThemeColors");

            if (!Directory.Exists(PathThemeColors))
            {
                Directory.CreateDirectory(PathThemeColors);
            }

            Parallel.ForEach(Directory.EnumerateFiles(PathThemeColors, "*.json"), (objectFile) =>
            {
                try
                {
                    ThemeColors themeColorsTEMP = JsonConvert.DeserializeObject<ThemeColors>(File.ReadAllText(objectFile));
                    
                    if (themeColorsTEMP.FileName == themeColors.FileName)
                    {
                        File.Delete(objectFile);
                    }
                }
                catch (Exception ex)
                {
                    Common.LogError(ex, false, $"Error to delete file {objectFile}");
                }
            });
        }

        public static void AddThemeColors(ThemeColors themeColors, string PluginUserDataPath)
        {
            string PathThemeColors = Path.Combine(PluginUserDataPath, "ThemeColors");
            string PathThemeColorsFile = Path.Combine(PathThemeColors, themeColors.Name + "_" + DateTime.Now.ToString("YYYY-MM-dd-HH-mm") + ".json");

            try
            {
                if (!Directory.Exists(PathThemeColors))
                {
                    Directory.CreateDirectory(PathThemeColors);
                }

                File.WriteAllText(PathThemeColorsFile, JsonConvert.SerializeObject(themeColors));
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, $"Error to create file {PathThemeColorsFile} for {themeColors.Name}");
            }
        }

        public static bool SaveThemeColors(ThemeModifierSettingsView settingsView, string ThemeName, string PluginUserDataPath)
        {
            ThemeColors themeColors = new ThemeColors { Name = ThemeName };

            string PathThemeColors = Path.Combine(PluginUserDataPath, "ThemeColors");
            string PathThemeColorsFile = PathThemeColorsFile = Path.Combine(PathThemeColors, Paths.GetSafeFilename(ThemeName) + ".json");

            try
            {
                if (!Directory.Exists(PathThemeColors))
                {
                    Directory.CreateDirectory(PathThemeColors);
                }

                // Create object
                foreach (string ControlName in ThemeVariables)
                {
                    var control = settingsView.FindName("tb" + ControlName);
                    if (control is TextBlock)
                    {
                        dynamic color = ((TextBlock)control).Background;

                        if (color is SolidColorBrush)
                        {
                            themeColors.ThemeColorsElements.Add(new ThemeColorsElement { Name = ControlName, ColorString = color.Color.ToString() + "-" + color.Opacity.ToString() });
                        }

                        if (color is LinearGradientBrush)
                        {
                            themeColors.ThemeColorsElements.Add(new ThemeColorsElement
                            {
                                Name = ControlName,
                                ColorLinear = new ThemeLinearGradient
                                {
                                    StartPoint = color.StartPoint,
                                    EndPoint = color.EndPoint,
                                    GradientStop1 = new ThemeGradientColor
                                    {
                                        ColorString = color.GradientStops[0].Color.ToString(),
                                        ColorOffset = color.GradientStops[0].Offset
                                    },
                                    GradientStop2 = new ThemeGradientColor
                                    {
                                        ColorString = color.GradientStops[1].Color.ToString(),
                                        ColorOffset = color.GradientStops[1].Offset
                                    }
                                }
                            });
                        }
                    }
                    else
                    {
                        logger.Warn($"Bad control {"tb" + ControlName}: {control.ToString()}");
                    }
                }

                //SaveThemeColors object
                File.WriteAllText(PathThemeColorsFile, JsonConvert.SerializeObject(themeColors));
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, $"Error for save theme {ThemeName}");
                return false;
            }

            return true;
        }

        public static bool ThemeFileExist(string ThemeName, string PluginUserDataPath)
        {
            string PathThemeColors = Path.Combine(PluginUserDataPath, "ThemeColors");
            string PathThemeColorsFile = PathThemeColorsFile = Path.Combine(PathThemeColors, Paths.GetSafeFilename(ThemeName) + ".json");

            return File.Exists(PathThemeColorsFile);
        }
        #endregion

        #region Theme constants
        public static List<ThemeConstantsDefined> GetThemeConstants(IPlayniteAPI PlayniteApi)
        {
            dynamic thm = null;
            try
            {
                ThemeManifest ThemeInfos = GetActualTheme(PlayniteApi);

                var deserializer = new DeserializerBuilder().Build();
                thm = deserializer.Deserialize<ExpandoObject>(File.ReadAllText(ThemeInfos.DescriptionPath));

                Common.LogDebug(true, $"thm: {JsonConvert.SerializeObject(thm)}");     

                var temp = (List<Object>)(thm.Constants);
                List<ThemeConstantsDefined> themeConstantsDefined = new List<ThemeConstantsDefined>();

                foreach (dynamic el in temp)
                {
                    if (el is string)
                    {
                        themeConstantsDefined.Add(new ThemeConstantsDefined { Name = (string)el });
                    }
                    else 
                    {
                        Common.LogDebug(true, $"el: {JsonConvert.SerializeObject(el)}");

                        foreach(var tt in el)
                        {
                            themeConstantsDefined.Add(new ThemeConstantsDefined { Name = (string)(tt.Key), Description = (string)(tt.Value) });
                        }
                    }
                }

                Common.LogDebug(true, $"temp: {JsonConvert.SerializeObject(themeConstantsDefined)}");
                return themeConstantsDefined;
            }
            catch(Exception ex)
            {
                logger.Warn($"No the constants defined");
                Common.LogError(ex, true, $"thm: {JsonConvert.SerializeObject(thm)}");

                return new List<ThemeConstantsDefined>();
            }
        }

        public static List<ThemeElement> GetThemeDefaultConstants(IPlayniteAPI PlayniteApi)
        {
            List<ThemeElement> ThemeDefaultConstants = new List<ThemeElement>();

            List<ThemeConstantsDefined> ListThemeConstants = GetThemeConstants(PlayniteApi);
            foreach (ThemeConstantsDefined ConstantsDefined in ListThemeConstants)
            {
                try
                {
                    string param = Regex.Match(ConstantsDefined.Name, @"(\([+-]?([0-9]*[.])?[0-9]+,[ ]?[+-]?([0-9]*[.])?[0-9]+\))").Value;
                    ThemeSliderLimit themeSliderLimit = new ThemeSliderLimit();
                    if (!param.IsNullOrEmpty())
                    {
                        ConstantsDefined.Name = ConstantsDefined.Name.Replace(param, string.Empty);

                        try
                        {
                            double Min = 0;
                            double Max = 30;

                            param = param.Replace("(", string.Empty).Replace(")", string.Empty);
                            double.TryParse(param.Split(',')[0], out Min);
                            double.TryParse(param.Split(',')[1], out Max);

                            themeSliderLimit.Min = Min;
                            themeSliderLimit.Max = Max;
                        }
                        catch (Exception ex)
                        {
                            Common.LogError(ex, false);
                        }
                    }

                    ThemeDefaultConstants.Add(new ThemeElement
                    {
                        Name = ConstantsDefined.Name,
                        Description = ConstantsDefined.Description,
                        Element = resources.GetResource(ConstantsDefined.Name),
                        themeSliderLimit = themeSliderLimit
                    });
                }
                catch
                {
                    logger.Warn($"Resources don't exists {ConstantsDefined.Name}");
                }
            }

            return ThemeDefaultConstants;
        }

        public static List<ThemeElement> GetThemeActualConstants(ThemeModifierSettings settings, IPlayniteAPI PlayniteApi)
        {
            List<ThemeElement> ThemeActualConstants = new List<ThemeElement>();

            ThemeManifest ThemeInfos = GetActualTheme(PlayniteApi);
            ThemeConstants ThemeSettingsConstants = new ThemeConstants();

            if (!ThemeInfos.Id.IsNullOrEmpty())
            {
                ThemeSettingsConstants = settings.ThemesConstants.Find(x => x.Id == ThemeInfos.Id);
            }
            else
            {
                ThemeSettingsConstants = settings.ThemesConstants.Find(x => x.Name == ThemeInfos.Name);
            }

            if (ThemeSettingsConstants != null)
            {
                foreach (ElementConstants elementConstants in ThemeSettingsConstants.Constants)
                {
                    dynamic ConvertedResource = ConvertResourceWithString(elementConstants.Element, elementConstants.TypeResource, elementConstants.Opacity);
                    if (ConvertedResource != null)
                    {
                        ThemeActualConstants.Add(new ThemeElement
                        {
                            Name = elementConstants.Name,
                            Element = ConvertedResource
                        });
                    }
                }
            }

            return ThemeActualConstants;
        }

        public static void SetThemeSettingsConstants(List<ThemeElement> ThemeConstants)
        {
            try
            {
                UI ui = new UI();
                List<ResourcesList> resourcesLists = new List<ResourcesList>();
                
                foreach (ThemeElement themeElement in ThemeConstants.Where(x => x.Element != null).ToList())
                {
                    resourcesLists.Add(new ResourcesList { Key = themeElement.Name, Value = themeElement.Element });
                }

                ui.AddResources(resourcesLists);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, "Error on SetThemeSettingsConstans()");
            }
        }

        public static List<ThemeConstants> GetThemesConstants(IPlayniteAPI PlayniteApi, List<ThemeElement> themesElements, List<ThemeConstants> ThemesConstants)
        {
            ThemeManifest ThemeInfos = GetActualTheme(PlayniteApi);

            try
            {
                List<ElementConstants> Constants = new List<ElementConstants>();
                foreach (ThemeElement themeElement in themesElements)
                {
                    Constants.Add(ConvertResourceToThemeConstants(themeElement.Name, themeElement.Element));
                }

                if (ThemesConstants.Find(x => x.Id == ThemeInfos.Id) != null)
                {
                    ThemesConstants.Find(x => x.Id == ThemeInfos.Id).Constants = Constants;
                }
                else
                {
                    ThemesConstants.Add(new ThemeConstants { Id = ThemeInfos.Id, Name = ThemeInfos.Name, Constants = Constants });
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, $"Error on GetThemesConstants()");
            }

            return ThemesConstants;
        }
#endregion


        private static dynamic ConvertResourceWithString(dynamic Element, string ElementType, double ElementOpacity)
        {
            dynamic ConvertedResource = null;

            switch (ElementType.ToLower())
            {
                case "bool":
                    ConvertedResource = (bool)Element;
                    break;
                case "string":
                    ConvertedResource = (string)Element;
                    break;
                case "double":
                    ConvertedResource = (double)Element;
                    break;
                case "int":
                    ConvertedResource = (int)Element;
                    break;

                case "color":
                    if (Element is string)
                    {
                        ConvertedResource = (Color)ColorConverter.ConvertFromString((string)Element);
                    }
                    if (Element is Color)
                    {
                        ConvertedResource = (Color)Element;
                    }
                    break;
                case "solidcolorbrush":
                    if (Element is string)
                    {
                        ConvertedResource = new SolidColorBrush((Color)ColorConverter.ConvertFromString((string)Element));
                        ConvertedResource.Opacity = ElementOpacity;
                    }
                    if (Element is Color)
                    {
                        ConvertedResource = new SolidColorBrush((Color)Element);
                    }
                    
                    break;
                case "lineargradientbrush":
                    ConvertedResource = (JsonConvert.DeserializeObject<ThemeLinearGradient>(JsonConvert.SerializeObject(Element))).ToLinearGradientBrush;
                    break;


                case "visibility":
                    ConvertedResource = (Visibility)Element;
                    break;

                case "verticalalignment":
                    ConvertedResource = (VerticalAlignment)Element;
                    break;

                case "horizontalalignment":
                    ConvertedResource = (HorizontalAlignment)Element;
                    break;

                default:
                    logger.Warn($"Element type not supported: {ElementType}");
                    break;
            }

            return ConvertedResource;
        }

        private static ElementConstants ConvertResourceToThemeConstants(string Name, dynamic Element)
        {
            ElementConstants ConvertedResource = new ElementConstants();

            if (Element is bool)
            {
                ConvertedResource = new ElementConstants { Name = Name, TypeResource = "bool", Element = (bool)Element };
            }
            if (Element is string)
            {
                ConvertedResource = new ElementConstants { Name = Name, TypeResource = "string", Element = (string)Element };
            }
            if (Element is double)
            {
                ConvertedResource = new ElementConstants { Name = Name, TypeResource = "double", Element = (double)Element };
            }
            if (Element is int)
            {
                ConvertedResource = new ElementConstants { Name = Name, TypeResource = "int", Element = (int)Element };
            }

            if (Element is Color)
            {
                ConvertedResource = new ElementConstants { Name = Name, TypeResource = "color", Element = (Color)Element };
            }
            if (Element is SolidColorBrush)
            {
                ConvertedResource = new ElementConstants { Name = Name, TypeResource = "solidcolorbrush", Element = (Color)((SolidColorBrush)Element).Color, Opacity = ((SolidColorBrush)Element).Opacity };
            }
            if (Element is LinearGradientBrush)
            {
                ThemeLinearGradient themeLinearGradient = new ThemeLinearGradient
                {
                    StartPoint = Element.StartPoint,
                    EndPoint = Element.EndPoint,
                    GradientStop1 = new ThemeGradientColor
                    {
                        ColorString = Element.GradientStops[0].Color.ToString(),
                        ColorOffset = Element.GradientStops[0].Offset
                    },
                    GradientStop2 = new ThemeGradientColor
                    {
                        ColorString = Element.GradientStops[1].Color.ToString(),
                        ColorOffset = Element.GradientStops[1].Offset
                    }
                };

                ConvertedResource = new ElementConstants { Name = Name, TypeResource = "lineargradientbrush", Element = (ThemeLinearGradient)themeLinearGradient };
            }

            if (Element is Visibility)
            {
                ConvertedResource = new ElementConstants { Name = Name, TypeResource = "visibility", Element = (Visibility)Element };
            }
            if (Element is VerticalAlignment)
            {
                ConvertedResource = new ElementConstants { Name = Name, TypeResource = "verticalalignment", Element = (VerticalAlignment)Element };
            }
            if (Element is HorizontalAlignment)
            {
                ConvertedResource = new ElementConstants { Name = Name, TypeResource = "horizontalalignment", Element = (HorizontalAlignment)Element };
            }

            return ConvertedResource;
        }
    }
}
