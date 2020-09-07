using PluginCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ThemeModifier.Models;

namespace ThemeModifier.Services
{
    public class ThemeClass
    {
        public static void SetColor(string name, Color? color, ThemeModifierSettings settings, dynamic colorDefault = null)
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
            }
        }

        public static void RestoreColor(List<ThemeElement> ThemeDefault, ThemeModifierSettings settings, bool withSettings = false)
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
            catch
            {

            }
        }
    }
}
