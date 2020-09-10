using ColorPickerWPF;
using ColorPickerWPF.Code;
using PluginCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThemeModifier.Models;
using ThemeModifier.Services;

namespace ThemeModifier.Views
{
    public partial class ThemeModifierSettingsView : UserControl
    {
        private ThemeModifierSettings settings;
        private List<ThemeElement> ThemeDefault;


        public ThemeModifierSettingsView(ThemeModifierSettings settings, List<ThemeElement> ThemeDefault)
        {
            this.settings = settings;
            this.ThemeDefault = ThemeDefault;

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

                    ThemeClass.SetColor(lControl.Content.ToString(), color, settings);
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

                ThemeElement finded = ThemeDefault.Find(x => x.name == lControl.Content.ToString());

                tbControl.Background = finded.color;

                ThemeClass.SetColor(lControl.Content.ToString(), null, settings, finded.color);
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
                ThemeClass.RestoreColor(ThemeDefault, settings);

                foreach (ThemeElement themeElement in ThemeDefault)
                {
                    ((TextBlock)this.FindName("tb" + themeElement.name)).Background = themeElement.color;
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemeModifier", "Error on BtRestoreDefault_Click()");
            }
        }
    }
}
