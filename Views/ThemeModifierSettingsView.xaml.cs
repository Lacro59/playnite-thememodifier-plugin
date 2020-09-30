using Newtonsoft.Json;
using Playnite.SDK;
using PluginCommon;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private ThemeModifierSettings _settings;
        private List<ThemeElement> _ThemeDefault;
        private string _PlayniteConfigurationPath;

        private TextBlock tbControl;
        private Label lControl;


        public ThemeModifierSettingsView(ThemeModifierSettings settings, List<ThemeElement> ThemeDefault, string PlayniteConfigurationPath)
        {
            _settings = settings;
            _ThemeDefault = ThemeDefault;
            _PlayniteConfigurationPath = PlayniteConfigurationPath;

            InitializeComponent();
        }

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
                    ((TextBlock)this.FindName("tb" + themeElement.Name)).Background = themeElement.Color;
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemeModifier", "Error on BtRestoreDefault_Click()");
            }
        }


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
                    ThemeClass.SetThemeColor(lControl.Content.ToString(), color, _settings);
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
    }
}
