using Playnite.SDK;
using Playnite.SDK.Models;
using CommonPluginsShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ThemeModifier.Views.InterfaceFS;
using ThemeModifier.Views.Interfaces;

namespace ThemeModifier.Services
{
    public class ThemeModifierUI : PlayniteUiHelper
    {
        ThemeModifierSettings _settings;


        public override string _PluginUserDataPath { get; set; } = string.Empty;

        public override bool IsFirstLoad { get; set; } = true;

        public override string BtActionBarName { get; set; } = string.Empty;
        public override FrameworkElement PART_BtActionBar { get; set; }

        public override string SpDescriptionName { get; set; } = string.Empty;
        public override FrameworkElement PART_SpDescription { get; set; }


        public override string SpInfoBarFSName { get; set; } = string.Empty;
        public override FrameworkElement PART_SpInfoBarFS { get; set; }

        public override string BtActionBarFSName { get; set; } = string.Empty;
        public override FrameworkElement PART_BtActionBarFS { get; set; }

        public override List<CustomElement> ListCustomElements { get; set; } = new List<CustomElement>();


        public ThemeModifierUI(IPlayniteAPI PlayniteApi, string PluginUserDataPath, ThemeModifierSettings settings) : base(PlayniteApi, PluginUserDataPath)
        {
            _settings = settings;
            _PluginUserDataPath = PluginUserDataPath;

            BtActionBarName = "PART_TmButton";
            SpDescriptionName = "PART_TmDescriptionIntegration";

            SpInfoBarFSName = "PART_TmSpInfoBar";
        }


        public override void Initial()
        {
        }


        public override DispatcherOperation AddElements()
        {
            if (_PlayniteApi.ApplicationInfo.Mode == ApplicationMode.Desktop)
            {
                if (IsFirstLoad)
                {
#if DEBUG
                    logger.Debug($"ThemeMofidier [Ignored] - IsFirstLoad");
#endif
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new ThreadStart(delegate
                    {
                        System.Threading.SpinWait.SpinUntil(() => IntegrationUI.SearchElementByName("PART_HtmlDescription") != null, 5000);
                    })).Wait();
                    IsFirstLoad = false;
                }

                return Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new ThreadStart(delegate
                {
                    CheckTypeView();

                    if (_settings.EnableIntegrationFeatures)
                    {
#if DEBUG
                        logger.Debug($"ThemeMofidier [Ignored] - AddSpDescription()");
#endif
                        AddBtActionBar();
                    }

                    if (_settings.EnableIntegrationInCustomTheme)
                    {
#if DEBUG
                        logger.Debug($"ThemeMofidier [Ignored] - AddCustomElements()");
#endif
                        AddCustomElements();
                    }
                }));
            }

            return null;
        }

        public override void RefreshElements(Game GameSelected, bool Force = false)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;

            Task TaskRefreshBtActionBar = Task.Run(() =>
            {
                try
                {
                    Initial();

                    // Reset resources
                    List<ResourcesList> resourcesLists = new List<ResourcesList>();
                    resourcesLists.Add(new ResourcesList { Key = "Tm_HasData", Value = false });
                    resourcesLists.Add(new ResourcesList { Key = "Tm_FeaturesList", Value = new List<FeaturesItem>() });
                    resourcesLists.Add(new ResourcesList { Key = "Tm_FeaturesListCount", Value = 0 });
                    ui.AddResources(resourcesLists);

                    ThemeModifier.icoFeatures.SetCurrentFeaturesList(GameSelected);

                    if (ThemeModifier.icoFeatures.CurrentFeaturesList.Count > 0)
                    {
                        resourcesLists = new List<ResourcesList>();
                        resourcesLists.Add(new ResourcesList { Key = "Tm_HasData", Value = true });
                        resourcesLists.Add(new ResourcesList { Key = "Tm_FeaturesList", Value = ThemeModifier.icoFeatures.CurrentFeaturesList });
                        resourcesLists.Add(new ResourcesList { Key = "Tm_FeaturesListCount", Value = ThemeModifier.icoFeatures.CurrentFeaturesList.Count });
                        ui.AddResources(resourcesLists);
                    }
                }
                catch (Exception ex)
                {
                    Common.LogError(ex, "ThemeModifier", $"Error on TaskRefreshBtActionBar()");
                }
            }, ct);

            taskHelper.Add(TaskRefreshBtActionBar, tokenSource);
        }


        #region BtActionBar
        public override void InitialBtActionBar()
        {
        }

        public override void AddBtActionBar()
        {
            if (PART_BtActionBar != null)
            {
#if DEBUG
                logger.Debug($"ThemeModifier [Ignored] - PART_BtActionBar allready insert");
#endif
                return;
            }

            try
            {
                FeaturesList featuresList = new FeaturesList();
                featuresList.Name = BtActionBarName;

                ui.AddButtonInGameSelectedActionBarButtonOrToggleButton(featuresList);
                PART_BtActionBar = IntegrationUI.SearchElementByName(BtActionBarName);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemeModifier");
            }
        }

        public override void RefreshBtActionBar()
        {
        }
        #endregion


        #region SpDescription
        public override void InitialSpDescription()
        {
        }

        public override void AddSpDescription()
        {
            if (PART_SpDescription != null)
            {
#if DEBUG
                logger.Debug($"ThemeModifier [Ignored] - PART_SpDescription allready insert");
#endif
                return;
            }

            try
            {
                FeaturesList SpDescription = new FeaturesList();
                SpDescription.Name = SpDescriptionName;

                ui.AddElementInGameSelectedDescription(SpDescription, true);
                PART_SpDescription = IntegrationUI.SearchElementByName(SpDescriptionName);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemeModifier");
            }
        }

        public override void RefreshSpDescription()
        {
        }
        #endregion


        #region CustomElements
        public override void InitialCustomElements()
        {
        }

        public override void AddCustomElements()
        {
            if (ListCustomElements.Count > 0)
            {
#if DEBUG
                logger.Debug($"ThemeModifier [Ignored] - CustomElements allready insert - {ListCustomElements.Count}");
#endif
                return;
            }

            FrameworkElement PART_TmFeaturesList = null;

            try
            {
                PART_TmFeaturesList = IntegrationUI.SearchElementByName("PART_TmFeaturesList", false, true);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemeModifier", $"Error on find custom element");
            }

            if (PART_TmFeaturesList != null)
            {
                PART_TmFeaturesList = new FeaturesList();
                try
                {
                    ui.AddElementInCustomTheme(PART_TmFeaturesList, "PART_TmFeaturesList");
                    ListCustomElements.Add(new CustomElement { ParentElementName = "PART_TmFeaturesList", Element = PART_TmFeaturesList });
                }
                catch (Exception ex)
                {
                    Common.LogError(ex, "ThemeModifier", "Error on AddCustomElements()");
                }
            }
            else
            {
#if DEBUG
                logger.Debug($"ThemeModifier [Ignored] - PART_TmFeaturesList not find");
#endif
            }
        }

        public override void RefreshCustomElements()
        {
        }
        #endregion




        public override DispatcherOperation AddElementsFS()
        {
            if (_PlayniteApi.ApplicationInfo.Mode == ApplicationMode.Fullscreen)
            {
                if (IsFirstLoad)
                {
#if DEBUG
                    logger.Debug($"ThemeModifier [Ignored] - IsFirstLoad");
#endif
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new ThreadStart(delegate
                    {
                        System.Threading.SpinWait.SpinUntil(() => IntegrationUI.SearchElementByName("PART_ButtonContext") != null, 5000);
                    })).Wait();
                    IsFirstLoad = false;
                }

                return Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new ThreadStart(delegate
                {
                    if (_settings.EnableIntegrationFS)
                    {
#if DEBUG
                        logger.Debug($"ThemeModifier [Ignored] - AddBtInfoBarFS()");
#endif
                        AddSpInfoBarFS();
                    }
                }));
            }

            return null;
        }


        #region SpInfoBarFS
        public override void InitialBtActionBarFS()
        {
        }

        public override void AddBtActionBarFS()
        {
        }

        public override void RefreshBtActionBarFS()
        {
        }
        #endregion  


        #region BtActionBarFS
        public override void InitialSpInfoBarFS()
        {
        }

        public override void AddSpInfoBarFS()
        {
            if (PART_SpInfoBarFS != null)
            {
#if DEBUG
                logger.Debug($"ThemeModifier [Ignored] - PART_BtInfoBar allready insert");
#endif

                ((FeaturesListFS)PART_SpInfoBarFS).SetData(ThemeModifier.GameSelected);
                return;
            }

            FrameworkElement SpInfoBar;
            SpInfoBar = new FeaturesListFS();

            SpInfoBar.Name = SpInfoBarFSName;
            SpInfoBar.Margin = new Thickness(50, 0, 0, 0);

            try
            {
                ui.AddStackPanelInGameSelectedInfoBarFS(SpInfoBar);
                PART_SpInfoBarFS = IntegrationUI.SearchElementByName(SpInfoBarFSName);

                if (PART_SpInfoBarFS != null)
                {
                    ((FeaturesListFS)PART_SpInfoBarFS).SetData(ThemeModifier.GameSelected);
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemeModifier");
            }
        }

        public override void RefreshSpInfoBarFS()
        {
        }
        #endregion  
    }
}
