using Playnite.SDK;
using Playnite.SDK.Data;
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
using CommonPlayniteShared;
using System.Text.RegularExpressions;
using CommonPlayniteShared.Manifests;
using System.Text;
using CommonPluginsShared.Models;
using System.Globalization;

namespace ThemeModifier.Services
{
    public class ThemeClass
    {
        private static ILogger Logger => LogManager.GetLogger();

        public static List<string> ThemeVariables { get; set; } = new List<string>
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
            "WindowPanelSeparatorBrush",
            "PopupBackgroundBrush",
            "CheckBoxCheckMarkBkBrush",

            "PositiveRatingBrush",
            "NegativeRatingBrush",
            "MixedRatingBrush",

            "WarningBrush",

            "ExpanderBackgroundBrush",
            "WindowBackgourndBrush",
        };

        private static List<string> ThemeFileToBackup { get; set; } = new List<string>();


        #region Theme colors
        public static List<ThemeElement> GetThemeDefault()
        {
            List<ThemeElement> ThemeDefault = new List<ThemeElement>();

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "ControlBackgroundBrush", Element = ResourceProvider.GetResource("ControlBackgroundBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists ControlBackgroundBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "TextBrush", Element = ResourceProvider.GetResource("TextBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists TextBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "TextBrushDarker", Element = ResourceProvider.GetResource("TextBrushDarker") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists TextBrushDarker");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "TextBrushDark", Element = ResourceProvider.GetResource("TextBrushDark") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists TextBrushDark");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "NormalBrush", Element = ResourceProvider.GetResource("NormalBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists NormalBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "NormalBrushDark", Element = ResourceProvider.GetResource("NormalBrushDark") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists NormalBrushDark");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "NormalBorderBrush", Element = ResourceProvider.GetResource("NormalBorderBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists NormalBorderBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "HoverBrush", Element = ResourceProvider.GetResource("HoverBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists HoverBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "GlyphBrush", Element = ResourceProvider.GetResource("GlyphBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists GlyphBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "HighlightGlyphBrush", Element = ResourceProvider.GetResource("HighlightGlyphBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists HighlightGlyphBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "PopupBorderBrush", Element = ResourceProvider.GetResource("PopupBorderBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists PopupBorderBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "TooltipBackgroundBrush", Element = ResourceProvider.GetResource("TooltipBackgroundBrush") });
            }
            catch
            {
                Logger.Warn($"ThemeModifier - Resources don't exists TooltipBackgroundBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "ButtonBackgroundBrush", Element = ResourceProvider.GetResource("ButtonBackgroundBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists ButtonBackgroundBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "GridItemBackgroundBrush", Element = ResourceProvider.GetResource("GridItemBackgroundBrush") });
            }
            catch
            {
                Logger.Warn($"ThemeModifier - Resources don't exists GridItemBackgroundBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "PanelSeparatorBrush", Element = ResourceProvider.GetResource("PanelSeparatorBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists PanelSeparatorBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "WindowPanelSeparatorBrush", Element = ResourceProvider.GetResource("WindowPanelSeparatorBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists WindowPanelSeparatorBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "PopupBackgroundBrush", Element = ResourceProvider.GetResource("PopupBackgroundBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists PopupBackgroundBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "CheckBoxCheckMarkBkBrush", Element = ResourceProvider.GetResource("CheckBoxCheckMarkBkBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists CheckBoxCheckMarkBkBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "PositiveRatingBrush", Element = ResourceProvider.GetResource("PositiveRatingBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists PositiveRatingBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "NegativeRatingBrush", Element = ResourceProvider.GetResource("NegativeRatingBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists NegativeRatingBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "MixedRatingBrush", Element = ResourceProvider.GetResource("MixedRatingBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists MixedRatingBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "ExpanderBackgroundBrush", Element = ResourceProvider.GetResource("ExpanderBackgroundBrush") });
            }
            catch
            {
                Logger.Warn($"ThemeModifier - Resources don't exists ExpanderBackgroundBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "WindowBackgourndBrush", Element = ResourceProvider.GetResource("WindowBackgourndBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists WindowBackgourndBrush");
            }

            try
            {
                ThemeDefault.Add(new ThemeElement { Name = "WarningBrush", Element = ResourceProvider.GetResource("WarningBrush") });
            }
            catch
            {
                Logger.Warn($"Resources don't exists WarningBrush");
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
                        Logger.Warn($"Color is {color.toString()}");
                    }
                }
                else if (colorDefault != null)
                {
                    resourcesLists.Add(new ResourcesList { Key = name, Value = colorDefault });
                }
                else
                {
                    Logger.Warn($"No default color");
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
                    case "WindowPanelSeparatorBrush":
                        settings.WindowPanelSeparatorBrush_Edit = colorString;
                        settings.WindowPanelSeparatorBrush_EditGradient = themeLinearGradient;
                        break;
                    case "PopupBackgroundBrush":
                        settings.PopupBackgroundBrush_Edit = colorString;
                        settings.PopupBackgroundBrush_EditGradient = themeLinearGradient;
                        break;
                    case "CheckBoxCheckMarkBkBrush":
                        settings.CheckBoxCheckMarkBkBrush_Edit = colorString;
                        settings.CheckBoxCheckMarkBkBrush_EditGradient = themeLinearGradient;
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
                Common.LogError(ex, false, true, "ThemeModifier");
            }
        }

        public static void RestoreColor(List<ThemeElement> themeDefault, ThemeModifierSettings settings, bool withSettings = false)
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

                    foreach (ThemeElement themeElement in themeDefault)
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
                            case "WindowPanelSeparatorBrush":
                                settings.WindowPanelSeparatorBrush_Edit = string.Empty;
                                settings.WindowPanelSeparatorBrush_EditGradient = themeLinearGradient;
                                break;
                            case "PopupBackgroundBrush":
                                settings.PopupBackgroundBrush_Edit = string.Empty;
                                settings.PopupBackgroundBrush_EditGradient = themeLinearGradient;
                                break;
                            case "CheckBoxCheckMarkBkBrush":
                                settings.CheckBoxCheckMarkBkBrush_Edit = string.Empty;
                                settings.CheckBoxCheckMarkBkBrush_EditGradient = themeLinearGradient;
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
                Common.LogError(ex, false, true, "ThemeModifier");
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
                        Key = "PanelSeparatorBrush",
                        Value = settings.PanelSeparatorBrush_EditGradient.ToLinearGradientBrush
                    });
                }

                if (!settings.WindowPanelSeparatorBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.WindowPanelSeparatorBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.WindowPanelSeparatorBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.WindowPanelSeparatorBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.WindowPanelSeparatorBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "WindowPanelSeparatorBrush",
                        Value = color
                    });
                }
                if (settings.WindowPanelSeparatorBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "WindowPanelSeparatorBrush",
                        Value = settings.WindowPanelSeparatorBrush_EditGradient.ToLinearGradientBrush
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

                if (!settings.CheckBoxCheckMarkBkBrush_Edit.IsNullOrEmpty())
                {
                    if (settings.CheckBoxCheckMarkBkBrush_Edit.IndexOf("-") > -1)
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.CheckBoxCheckMarkBkBrush_Edit.Split('-')[0]));
                        color.Opacity = double.Parse(settings.CheckBoxCheckMarkBkBrush_Edit.Split('-')[1]);
                    }
                    else
                    {
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings.CheckBoxCheckMarkBkBrush_Edit));
                    }

                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "CheckBoxCheckMarkBkBrush",
                        Value = color
                    });
                }
                if (settings.CheckBoxCheckMarkBkBrush_EditGradient.GradientStop1 != null)
                {
                    resourcesLists.Add(new ResourcesList
                    {
                        Key = "CheckBoxCheckMarkBkBrush",
                        Value = settings.CheckBoxCheckMarkBkBrush_EditGradient.ToLinearGradientBrush
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
                Common.LogError(ex, false, true, "ThemeModifier");
            }
        }
        #endregion


        #region Theme icons
        public static ThemeManifest GetActualTheme()
        {
            PlayniteTools.SetThemeInformation();
            return ThemeManager.CurrentTheme;
        }

        public static Grid CreateControl(ThemeModifierSettings settings, double maxHeight, ImageSource originalSource)
        {
            try
            {
                string pluginFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string imageName = string.Empty;

                if (settings.UseIconCircle)
                {
                    imageName = "circle";
                }
                if (settings.UseIconClock)
                {
                    imageName = "clock";
                }
                if (settings.UseIconSquareCorne)
                {
                    imageName = "squareCorne";
                }
                if (settings.UseIconWe4ponx)
                {
                    imageName = "we4ponx";
                }


                Grid g = new Grid
                {
                    Margin = new Thickness(0, 0, 10, 0)
                };
                DockPanel.SetDock(g, Dock.Left);

                Image PART_ImageFrame = new Image
                {
                    Name = "PART_ImageFrame",
                    MaxHeight = maxHeight,
                    MaxWidth = maxHeight,
                    Source = new BitmapImage(new Uri($"{pluginFolder}\\Themes\\Images\\{imageName}.png"))
                };
                RenderOptions.SetBitmapScalingMode(PART_ImageFrame, BitmapScalingMode.Fant);

                Image PART_ImageIcon = new Image
                {
                    Name = "PART_ImageIcon",
                    Source = originalSource,
                    Stretch = Stretch.Fill,
                    MaxHeight = maxHeight,
                    MaxWidth = maxHeight
                };
                RenderOptions.SetBitmapScalingMode(PART_ImageFrame, BitmapScalingMode.Fant);

                ImageBrush imgB = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri($"{pluginFolder}\\Themes\\Images\\{imageName}Shape.png"))
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
                Common.LogError(ex, false, true, "ThemeModifier");
            }

            return null;
        }
        #endregion


        #region Theme manage
        public static void LoadThemeColors(string pathFileName, ThemeModifierSettings settings, ThemeModifierSettingsView settingsView)
        {
            ThemeColors themeColors = Serialization.FromJsonFile<ThemeColors>(pathFileName);
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
                    Logger.Warn($"Bad control {"tb" + themeColorsElement.Name}: {control.ToString()}");
                }
            }
        }

        public static List<ThemeColors> GetListThemeColors(string pluginUserDataPath)
        {
            List<ThemeColors> listThemeColors = new List<ThemeColors>();

            string pathThemeColors = Path.Combine(pluginUserDataPath, "ThemeColors");

            if (!Directory.Exists(pathThemeColors))
            {
                Directory.CreateDirectory(pathThemeColors);
            }

            Parallel.ForEach(Directory.EnumerateFiles(pathThemeColors, "*.json"), (objectFile) =>
            {
                Common.LogDebug(true, $"GetListThemeColors() - {objectFile}");

                try
                {
                    ThemeColors themeColors = Serialization.FromJsonFile<ThemeColors>(objectFile);
                    themeColors.FileName = objectFile;
                    listThemeColors.Add(themeColors);
                }
                catch (Exception ex)
                {
                    Common.LogError(ex, false, true, "ThemeModifier");
                }
            });

            return listThemeColors;
        }

        public static void DeleteThemeColors(string pathFileName)
        {
            try
            {
                File.Delete(pathFileName);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, $"Error to delete file {pathFileName}", true, "ThemeModifier");
            }
        }

        public static void DeleteThemeColors(ThemeColors themeColors, string pluginUserDataPath)
        {
            string pathThemeColors = Path.Combine(pluginUserDataPath, "ThemeColors");

            if (!Directory.Exists(pathThemeColors))
            {
                Directory.CreateDirectory(pathThemeColors);
            }

            Parallel.ForEach(Directory.EnumerateFiles(pathThemeColors, "*.json"), (objectFile) =>
            {
                try
                {
                    ThemeColors themeColorsTEMP = Serialization.FromJsonFile<ThemeColors>(objectFile);
                    
                    if (themeColorsTEMP.FileName == themeColors.FileName)
                    {
                        File.Delete(objectFile);
                    }
                }
                catch (Exception ex)
                {
                    Common.LogError(ex, false, $"Error to delete file {objectFile}", true, "ThemeModifier");
                }
            });
        }

        public static void AddThemeColors(ThemeColors themeColors, string pluginUserDataPath)
        {
            string pathThemeColors = Path.Combine(pluginUserDataPath, "ThemeColors");
            string pathThemeColorsFile = Path.Combine(pathThemeColors, themeColors.Name + "_" + DateTime.Now.ToString("YYYY-MM-dd-HH-mm") + ".json");

            try
            {
                if (!Directory.Exists(pathThemeColors))
                {
                    Directory.CreateDirectory(pathThemeColors);
                }

                File.WriteAllText(pathThemeColorsFile, Serialization.ToJson(themeColors), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, $"Error to create file {pathThemeColorsFile} for {themeColors.Name}", true, "ThemeModifier");
            }
        }

        public static bool SaveThemeColors(ThemeModifierSettingsView settingsView, string themeName, string pluginUserDataPath)
        {
            ThemeColors themeColors = new ThemeColors { Name = themeName };

            string pathThemeColors = Path.Combine(pluginUserDataPath, "ThemeColors");
            string pathThemeColorsFile = Path.Combine(pathThemeColors, CommonPlayniteShared.Common.Paths.GetSafePathName(themeName) + ".json");

            try
            {
                if (!Directory.Exists(pathThemeColors))
                {
                    Directory.CreateDirectory(pathThemeColors);
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
                        Logger.Warn($"Bad control {"tb" + ControlName}: {control.ToString()}");
                    }
                }

                //SaveThemeColors object
                File.WriteAllText(pathThemeColorsFile, Serialization.ToJson(themeColors), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, $"Error for save theme {themeName}", true, "ThemeModifier");
                return false;
            }

            return true;
        }

        public static bool ThemeFileExist(string themeName, string pluginUserDataPath)
        {
            string pathThemeColors = Path.Combine(pluginUserDataPath, "ThemeColors");
            string pathThemeColorsFile = pathThemeColorsFile = Path.Combine(pathThemeColors, CommonPlayniteShared.Common.Paths.GetSafePathName(themeName) + ".json");

            return File.Exists(pathThemeColorsFile);
        }
        #endregion


        #region Theme constants
        public static List<ThemeConstantsDefined> GetThemeConstants()
        {
            dynamic thm = null;
            try
            {
                ThemeManifest themeInfos = GetActualTheme();
                List<ThemeConstantsDefined> themeConstantsDefined = new List<ThemeConstantsDefined>();
                string pathYaml = Path.Combine(themeInfos.DirectoryPath, "thememodifier.yaml");

                if (File.Exists(pathYaml))
                {
                    try
                    {
                        var deserializer = new DeserializerBuilder().Build();
                        thm = deserializer.Deserialize<ExpandoObject>(File.ReadAllText(pathYaml));

                        if (thm != null && thm.Constants != null)
                        {
                            var temp = (List<Object>)(thm.Constants);

                            foreach (dynamic el in temp)
                            {
                                if (el is string)
                                {
                                    themeConstantsDefined.Add(new ThemeConstantsDefined { Name = (string)el });
                                }
                                else
                                {
                                    foreach (var tt in el)
                                    {
                                        themeConstantsDefined.Add(new ThemeConstantsDefined { Name = (string)(tt.Key), Description = (string)(tt.Value) });
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogError(ex, false, true, "ThemeModifier");
                    }
                }

                return themeConstantsDefined;
            }
            catch(Exception ex)
            {
                Common.LogError(ex, false, true, "ThemeModifier");
                return new List<ThemeConstantsDefined>();
            }
        }

        public static List<ThemeElement> GetThemeDefaultConstants()
        {
            List<ThemeElement> themeDefaultConstants = new List<ThemeElement>();

            List<ThemeConstantsDefined> listThemeConstants = GetThemeConstants();
            foreach (ThemeConstantsDefined constantsDefined in listThemeConstants)
            {
                try
                {
                    string param = Regex.Match(constantsDefined.Name, @"(\([+-]?([0-9]*[.])?[0-9]+,[ ]?[+-]?([0-9]*[.])?[0-9]+\))").Value;
                    ThemeSliderLimit themeSliderLimit = new ThemeSliderLimit();
                    if (!param.IsNullOrEmpty())
                    {
                        constantsDefined.Name = constantsDefined.Name.Replace(param, string.Empty);

                        try
                        {
                            param = param.Replace("(", string.Empty).Replace(")", string.Empty);
                            var paramList = param.Split(',');

                            double.TryParse(paramList[0]
                                .Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Trim()
                                .Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Trim(), out double Min);
                            double.TryParse(paramList[1]
                                .Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Trim()
                                .Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Trim(), out double Max);

                            themeSliderLimit.Min = Min;
                            themeSliderLimit.Max = Max;
                        }
                        catch (Exception ex)
                        {
                            Common.LogError(ex, false, true, "ThemeModifier");
                        }
                    }

                    themeDefaultConstants.Add(new ThemeElement
                    {
                        Name = constantsDefined.Name,
                        Description = constantsDefined.Description,
                        Element = ResourceProvider.GetResource(constantsDefined.Name),
                        themeSliderLimit = themeSliderLimit
                    });
                }
                catch
                {
                    Logger.Warn($"Resources don't exists {constantsDefined.Name}");
                }
            }

            return themeDefaultConstants;
        }

        public static List<ThemeElement> GetThemeActualConstants(ThemeModifierSettings settings)
        {
            List<ThemeElement> themeActualConstants = new List<ThemeElement>();

            ThemeManifest themeInfos = GetActualTheme();
            ThemeConstants themeSettingsConstants = new ThemeConstants();

            if (!themeInfos.Id.IsNullOrEmpty())
            {
                themeSettingsConstants = settings.ThemesConstants.Find(x => x.Id == themeInfos.Id);
            }
            else
            {
                themeSettingsConstants = settings.ThemesConstants.Find(x => x.Name == themeInfos.Name);
            }

            if (themeSettingsConstants != null)
            {
                foreach (ElementConstants elementConstants in themeSettingsConstants.Constants)
                {
                    dynamic convertedResource = ConvertResourceWithString(elementConstants.Element, elementConstants.TypeResource, elementConstants.Opacity);
                    if (convertedResource != null)
                    {
                        themeActualConstants.Add(new ThemeElement
                        {
                            Name = elementConstants.Name,
                            Element = convertedResource
                        });
                    }
                }
            }

            return themeActualConstants;
        }

        public static void SetThemeSettingsConstants(List<ThemeElement> themeConstants)
        {
            try
            {
                UI ui = new UI();
                List<ResourcesList> resourcesLists = new List<ResourcesList>();
                
                foreach (ThemeElement themeElement in themeConstants.Where(x => x.Element != null).ToList())
                {
                    resourcesLists.Add(new ResourcesList { Key = themeElement.Name, Value = themeElement.Element });
                }

                ui.AddResources(resourcesLists);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, true, "ThemeModifier");
            }
        }

        public static List<ThemeConstants> GetThemesConstants(List<ThemeElement> themesElements, List<ThemeConstants> themesConstants)
        {
            ThemeManifest themeInfos = GetActualTheme();

            try
            {
                List<ElementConstants> constants = new List<ElementConstants>();
                foreach (ThemeElement themeElement in themesElements)
                {
                    constants.Add(ConvertResourceToThemeConstants(themeElement.Name, themeElement.Element));
                }

                if (themesConstants.Find(x => x.Id == themeInfos.Id) != null)
                {
                    themesConstants.Find(x => x.Id == themeInfos.Id).Constants = constants;
                }
                else
                {
                    themesConstants.Add(new ThemeConstants { Id = themeInfos.Id, Name = themeInfos.Name, Constants = constants });
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, true, "ThemeModifier");
            }

            return themesConstants;
        }
        #endregion


        private static dynamic ConvertResourceWithString(dynamic element, string elementType, double elementOpacity)
        {
            dynamic convertedResource = null;

            switch (elementType.ToLower())
            {
                case "bool":
                    convertedResource = (bool)element;
                    break;
                case "string":
                    convertedResource = (string)element;
                    break;
                case "double":
                    convertedResource = (double)element;
                    break;
                case "int":
                    convertedResource = (int)element;
                    break;

                case "color":
                    if (element is string)
                    {
                        convertedResource = (Color)ColorConverter.ConvertFromString((string)element);
                    }
                    if (element is Color)
                    {
                        convertedResource = (Color)element;
                    }
                    break;
                case "solidcolorbrush":
                    if (element is string)
                    {
                        convertedResource = new SolidColorBrush((Color)ColorConverter.ConvertFromString((string)element));
                        convertedResource.Opacity = elementOpacity;
                    }
                    if (element is Color)
                    {
                        convertedResource = new SolidColorBrush((Color)element);
                    }
                    break;
                case "lineargradientbrush":
                    convertedResource = Serialization.FromJson<ThemeLinearGradient>(Serialization.ToJson(element)).ToLinearGradientBrush;
                    break;

                case "visibility":
                    convertedResource = (Visibility)element;
                    break;

                case "verticalalignment":
                    convertedResource = (VerticalAlignment)element;
                    break;

                case "horizontalalignment":
                    convertedResource = (HorizontalAlignment)element;
                    break;

                default:
                    Logger.Warn($"Element type not supported: {elementType}");
                    break;
            }

            return convertedResource;
        }

        private static ElementConstants ConvertResourceToThemeConstants(string name, dynamic element)
        {
            ElementConstants convertedResource = new ElementConstants();

            if (element is bool)
            {
                convertedResource = new ElementConstants { Name = name, TypeResource = "bool", Element = (bool)element };
            }
            if (element is string)
            {
                convertedResource = new ElementConstants { Name = name, TypeResource = "string", Element = (string)element };
            }
            if (element is double)
            {
                convertedResource = new ElementConstants { Name = name, TypeResource = "double", Element = (double)element };
            }
            if (element is int)
            {
                convertedResource = new ElementConstants { Name = name, TypeResource = "int", Element = (int)element };
            }

            if (element is Color)
            {
                convertedResource = new ElementConstants { Name = name, TypeResource = "color", Element = (Color)element };
            }
            if (element is SolidColorBrush)
            {
                convertedResource = new ElementConstants { Name = name, TypeResource = "solidcolorbrush", Element = (Color)((SolidColorBrush)element).Color, Opacity = ((SolidColorBrush)element).Opacity };
            }
            if (element is LinearGradientBrush)
            {
                ThemeLinearGradient themeLinearGradient = new ThemeLinearGradient
                {
                    StartPoint = element.StartPoint,
                    EndPoint = element.EndPoint,
                    GradientStop1 = new ThemeGradientColor
                    {
                        ColorString = element.GradientStops[0].Color.ToString(),
                        ColorOffset = element.GradientStops[0].Offset
                    },
                    GradientStop2 = new ThemeGradientColor
                    {
                        ColorString = element.GradientStops[1].Color.ToString(),
                        ColorOffset = element.GradientStops[1].Offset
                    }
                };

                convertedResource = new ElementConstants { Name = name, TypeResource = "lineargradientbrush", Element = (ThemeLinearGradient)themeLinearGradient };
            }

            if (element is Visibility)
            {
                convertedResource = new ElementConstants { Name = name, TypeResource = "visibility", Element = (Visibility)element };
            }
            if (element is VerticalAlignment)
            {
                convertedResource = new ElementConstants { Name = name, TypeResource = "verticalalignment", Element = (VerticalAlignment)element };
            }
            if (element is HorizontalAlignment)
            {
                convertedResource = new ElementConstants { Name = name, TypeResource = "horizontalalignment", Element = (HorizontalAlignment)element };
            }

            return convertedResource;
        }
    }
}
