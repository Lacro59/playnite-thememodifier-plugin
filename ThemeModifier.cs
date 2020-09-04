using Playnite.SDK;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using PluginCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using ThemeModifier.Views;

namespace ThemeModifier
{
    public class ThemeModifier : Plugin
    {
        private static readonly ILogger logger = LogManager.GetLogger();

        private ThemeModifierSettings settings { get; set; }

        public override Guid Id { get; } = Guid.Parse("ec2f4013-17e6-428a-b8a9-5e34a3b80009");

        public ThemeModifier(IPlayniteAPI api) : base(api)
        {
            settings = new ThemeModifierSettings(this);

            IntegrationUI ui = new IntegrationUI();
            List<ResourcesList> resourcesLists = new List<ResourcesList>();

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

            ui.AddResources(resourcesLists);
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
            return new ThemeModifierSettingsView(settings);
        }
    }
}