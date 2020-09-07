using ColorPickerWPF;
using ColorPickerWPF.Code;
using PluginCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void ButtonRestore_Click(object sender, RoutedEventArgs e)
        {
            TextBlock tbControl = ((StackPanel)((FrameworkElement)sender).Parent).Children.OfType<TextBlock>().FirstOrDefault();
            Label lControl = ((StackPanel)((FrameworkElement)sender).Parent).Children.OfType<Label>().FirstOrDefault();


            ThemeElement finded = ThemeDefault.Find(x => x.name == lControl.Content.ToString());

            tbControl.Background = finded.color;

            ThemeClass.SetColor(lControl.Content.ToString(), null, settings, finded.color);
        }

        private void BtRestore_Click(object sender, RoutedEventArgs e)
        {
            ThemeClass.RestoreColor(ThemeDefault, settings);

            foreach (ThemeElement themeElement in ThemeDefault)
            {
                ((TextBlock)this.FindName("tb" + themeElement.name)).Background = themeElement.color;
            }
        }
    }
}