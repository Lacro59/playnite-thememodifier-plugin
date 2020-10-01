using Newtonsoft.Json;
using Playnite.SDK;
using PluginCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using ThemeModifier.Models;
using ThemeModifier.Services;

namespace ThemeModifier.Views
{
    public partial class ThemeModifierSettingsView : UserControl
    {
        private static readonly ILogger logger = LogManager.GetLogger();
        private static IResourceProvider resources = new ResourceProvider();
        private IPlayniteAPI _PlayniteAPI;
        private ThemeModifierSettings _settings;

        private List<ThemeElement> _ThemeDefault;
        private string _PluginUserDataPath;
        private string _PlayniteConfigurationPath;

        private TextBlock tbControl;
        private Label lControl;


        public ThemeModifierSettingsView(IPlayniteAPI PlayniteAPI, ThemeModifierSettings settings, List<ThemeElement> ThemeDefault, string PlayniteConfigurationPath, string PluginUserDataPath)
        {
            _PlayniteAPI = PlayniteAPI;
            _settings = settings;
            _ThemeDefault = ThemeDefault;
            _PlayniteConfigurationPath = PlayniteConfigurationPath;
            _PluginUserDataPath = PluginUserDataPath;

            InitializeComponent();


            SetMenuItems();
        }


        #region Theme color
        private void BtPickColor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tbControl = ((StackPanel)((FrameworkElement)sender).Parent).Children.OfType<TextBlock>().FirstOrDefault();
                lControl = ((StackPanel)((FrameworkElement)sender).Parent).Children.OfType<Label>().FirstOrDefault();

                if (tbControl.Background is SolidColorBrush)
                {
                    Color color = ((SolidColorBrush)tbControl.Background).Color;
                    PART_SelectorColorPicker.SetColors(color);
                }
                if (tbControl.Background is LinearGradientBrush)
                {
                    LinearGradientBrush linearGradientBrush = (LinearGradientBrush)tbControl.Background;
                    PART_SelectorColorPicker.SetColors(linearGradientBrush);
                }

                PART_SelectorColor.Visibility = Visibility.Visible;
                PART_ThemeColor.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemeModifier", "Error on BtPickColor_Click()");
            }
        }

        private void BtRestore_Click(object sender, RoutedEventArgs e)
        {
            try { 
                TextBlock tbControl = ((StackPanel)((FrameworkElement)sender).Parent).Children.OfType<TextBlock>().FirstOrDefault();
                Label lControl = ((StackPanel)((FrameworkElement)sender).Parent).Children.OfType<Label>().FirstOrDefault();

                ThemeElement finded = _ThemeDefault.Find(x => x.Name == lControl.Content.ToString());

                tbControl.Background = finded.Color;

                ThemeClass.SetThemeColor(lControl.Content.ToString(), null, _settings, finded.Color);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemeModifier", "Error on BtRestore_Click()");
            }
        }

        private void BtRestoreDefault_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ThemeClass.RestoreColor(_ThemeDefault, _settings);

                foreach (ThemeElement themeElement in _ThemeDefault)
                {
                    var control = this.FindName("tb" + themeElement.Name);
                    if (control is TextBlock)
                    {
                        ((TextBlock)control).Background = themeElement.Color;
                        ThemeClass.SetThemeColor(themeElement.Name, null, _settings, themeElement.Color);
                    }
                    else
                    {
                        logger.Warn($"ThemeModifier - Bad control {"tb" + themeElement.Name}: {control.ToString()}");
                    }                    
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemeModifier", "Error on BtRestoreDefault_Click()");
            }
        }
        #endregion


        #region Icon changer
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton tb = (ToggleButton)sender;
            if (!(bool)tb.IsChecked)
            {
                tb.IsChecked = true;
            }

            if (tb.Name != "tbUseIconCircle")
            {
                tbUseIconCircle.IsChecked = false;
            }
            if (tb.Name != "tbUseIconClock")
            {
                tbUseIconClock.IsChecked = false;
            }
            if (tb.Name != "tbUseIconSquareCorne")
            {
                tbUseIconSquareCorne.IsChecked = false;
            }
            if (tb.Name != "tbUseIconWe4ponx")
            {
                tbUseIconWe4ponx.IsChecked = false;
            }
        }

        private void BtSetIcons_Click(object sender, RoutedEventArgs e)
        {
            _settings.EnableInDescription = (bool)cEnableInDescription.IsChecked;

            _settings.UseIconCircle = (bool)tbUseIconCircle.IsChecked;
            _settings.UseIconClock = (bool)tbUseIconClock.IsChecked;
            _settings.UseIconSquareCorne = (bool)tbUseIconSquareCorne.IsChecked;
            _settings.UseIconWe4ponx = (bool)tbUseIconWe4ponx.IsChecked;

            ThemeClass.SetThemeFile(_PlayniteConfigurationPath, _settings);
        }

        private void BtRemoveIcons_Click(object sender, RoutedEventArgs e)
        {
            ThemeClass.RestoreThemeFile(_PlayniteConfigurationPath);
        }

        private void PART_TM_ColorOK_Click(object sender, RoutedEventArgs e)
        {
            Color color = default(Color);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();

            if (tbControl != null && lControl != null)
            {
                if (PART_SelectorColorPicker.IsSimpleColor)
                {
                    color = PART_SelectorColorPicker.SimpleColor;
                    tbControl.Background = new SolidColorBrush(color);
                    ThemeClass.SetThemeColor(lControl.Content.ToString(), new SolidColorBrush(color), _settings);
                }
                else
                {
                    linearGradientBrush = PART_SelectorColorPicker.linearGradientBrush;
                    tbControl.Background = linearGradientBrush;
                    ThemeClass.SetThemeColor(lControl.Content.ToString(), linearGradientBrush, _settings);
                }
            }
            else
            {
                logger.Warn("ThemeModifier - One control is undefined");
            }

            PART_SelectorColor.Visibility = Visibility.Collapsed;
            PART_ThemeColor.Visibility = Visibility.Visible;
        }

        private void PART_TM_ColorCancel_Click(object sender, RoutedEventArgs e)
        {
            PART_SelectorColor.Visibility = Visibility.Collapsed;
            PART_ThemeColor.Visibility = Visibility.Visible;
        }
        #endregion


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                if (((TabControl)sender).SelectedIndex == 1 || ((TabControl)sender).SelectedIndex == 2)
                {
                    PART_EditThemeMenu.IsEnabled = true;
                }
                else
                {
                    PART_EditThemeMenu.IsEnabled = false;
                }
            }
            catch
            {
            }
        }


        #region Menu
        private void SetMenuItems()
        {
            Thread.Sleep(500);
            var listItems = ThemeClass.GetListThemeColors(_PluginUserDataPath);

            if (listItems == null)
            {
                Thread.Sleep(500);
                listItems = ThemeClass.GetListThemeColors(_PluginUserDataPath);
            }

            listItems.Sort((x, y) => x.Name.CompareTo(y.Name));

            PART_EditThemeMenuLoad.ItemsSource = listItems;
            PART_EditThemeMenuRemove.ItemsSource = listItems;
            PART_EditThemeMenuExport.ItemsSource = listItems;
        }


        private void PART_EditThemeMenuBtSave_Click(object sender, RoutedEventArgs e)
        {
            string ThemeName = PART_EditThemeMenuSaveName.Text;
            if (ThemeClass.SaveThemeColors(this, ThemeName, _PluginUserDataPath))
            {
                _PlayniteAPI.Dialogs.ShowMessage(resources.GetString("LOCThemeModifierManageSaveOk"), "ThemeModifier");
                SetMenuItems();
            }
            else
            {
                _PlayniteAPI.Dialogs.ShowMessage(resources.GetString("LOCThemeModifierManageSaveKo"), "ThemeModifier");
            }
        }

        private void PART_EditThemeMenuSaveName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string ThemeName = PART_EditThemeMenuSaveName.Text;
            PART_EditThemeMenuBtSave.IsEnabled = (ThemeName.Trim().Length > 3 && !ThemeClass.ThemeFileExist(ThemeName, _PluginUserDataPath));
        }


        private void PART_EditThemeMenuBtLoad_Click(object sender, RoutedEventArgs e)
        {
            string PathFileName = ((FrameworkElement)sender).Tag.ToString();

            try
            {
                ThemeClass.LoadThemeColors(PathFileName, _settings, this);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemeModifier", $"Error on load {PathFileName}");
            }
        }


        private void PART_EditThemeMenuBtRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string PathFileName = ((FrameworkElement)sender).Tag.ToString();
                if (_PlayniteAPI.Dialogs.ShowMessage(resources.GetString("LOCRemoveLabel") + " " + PathFileName,  "ThemeModifier", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ThemeClass.DeleteThemeColors(PathFileName, _PluginUserDataPath);
                    SetMenuItems();
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemeModifier", $"Error on remove {((FrameworkElement)sender).Tag.ToString()}");
            }
        }


        private void PART_EditThemeMenuImport_Click(object sender, RoutedEventArgs e)
        {
            string targetPath = _PlayniteAPI.Dialogs.SelectFile("json file|*.json");

            if (!targetPath.IsNullOrEmpty())
            {
                ThemeColors themeColors = new ThemeColors();

                try
                {
                    themeColors = JsonConvert.DeserializeObject<ThemeColors>(File.ReadAllText(targetPath));
                }
                catch
                {
                    _PlayniteAPI.Dialogs.ShowErrorMessage(resources.GetString("LOCThemeModifierManageNoFile"), "ThemeModifier");
                    return;
                }

                if (themeColors.Name.IsNullOrEmpty())
                {
                    _PlayniteAPI.Dialogs.ShowErrorMessage(resources.GetString("LOCThemeModifierManageNoFile"), "ThemeModifier");
                    return;
                }

                string PathThemeColors = Path.Combine(_PluginUserDataPath, "ThemeColors");
                string PathThemeColorsFile = Path.Combine(PathThemeColors, themeColors.Name + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".json");
                File.Copy(targetPath, PathThemeColorsFile, true);

                SetMenuItems();
            }
        }


        private void PART_EditThemeMenuBtExport_Click(object sender, RoutedEventArgs e)
        {
            string PathFileName = ((FrameworkElement)sender).Tag.ToString();
            string targetPath = _PlayniteAPI.Dialogs.SaveFile("json file|*.json", true);            

            if (!targetPath.IsNullOrEmpty())
            {
                File.Copy(PathFileName, targetPath, true);
            }
        }
        #endregion
    }
}
