using ColorPickerWPF;
using ColorPickerWPF.Code;
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
        private ThemeModifierSettings _settings;
        private List<ThemeElement> _ThemeDefault;
        private string _PlayniteConfigurationPath;


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
                Color color;
                bool ok = ColorPickerWindow.ShowDialog(out color, ColorPickerDialogOptions.SimpleView);

                if (ok)
                {
                    TextBlock tbControl = ((StackPanel)((FrameworkElement)sender).Parent).Children.OfType<TextBlock>().FirstOrDefault();
                    Label lControl = ((StackPanel)((FrameworkElement)sender).Parent).Children.OfType<Label>().FirstOrDefault();

                    tbControl.Background = new SolidColorBrush(color);

                    ThemeClass.SetColor(lControl.Content.ToString(), color, _settings);
                }
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

                ThemeElement finded = _ThemeDefault.Find(x => x.name == lControl.Content.ToString());

                tbControl.Background = finded.color;

                ThemeClass.SetColor(lControl.Content.ToString(), null, _settings, finded.color);
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
                    ((TextBlock)this.FindName("tb" + themeElement.name)).Background = themeElement.color;
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
    }
}
