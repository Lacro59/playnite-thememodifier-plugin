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

                SetColor(lControl.Content.ToString(), color);
            }
        }

        private void SetColor (string Name, Color color)
        {
            IntegrationUI ui = new IntegrationUI();
            List<ResourcesList> resourcesLists = new List<ResourcesList>();
            resourcesLists.Add(new ResourcesList { Key = Name, Value = new SolidColorBrush(color) });
            ui.AddResources(resourcesLists);

            switch (Name)
            {
                case "ControlBackgroundBrush":
                    settings.ControlBackgroundBrush_Edit = color.ToString();
                    break;
                case "TextBrush":
                    settings.TextBrush_Edit = color.ToString();
                    break;
                case "TextBrushDarker":
                    settings.TextBrushDarker_Edit = color.ToString();
                    break;
                case "TextBrushDark":
                    settings.TextBrushDark_Edit = color.ToString();
                    break;
                case "NormalBrush":
                    settings.NormalBrush_Edit = color.ToString();
                    break;
                case "NormalBrushDark":
                    settings.NormalBrushDark_Edit = color.ToString();
                    break;
                case "NormalBorderBrush":
                    settings.NormalBorderBrush_Edit = color.ToString();
                    break;
                case "HoverBrush":
                    settings.HoverBrush_Edit = color.ToString();
                    break;
                case "GlyphBrush":
                    settings.GlyphBrush_Edit = color.ToString();
                    break;
                case "HighlightGlyphBrush":
                    settings.HighlightGlyphBrush_Edit = color.ToString();
                    break;
                case "PopupBorderBrush":
                    settings.PopupBorderBrush_Edit = color.ToString();
                    break;
                case "TooltipBackgroundBrush":
                    settings.TooltipBackgroundBrush_Edit = color.ToString();
                    break;
                case "ButtonBackgroundBrush":
                    settings.ButtonBackgroundBrush_Edit = color.ToString();
                    break;
                case "GridItemBackgroundBrush":
                    settings.GridItemBackgroundBrush_Edit = color.ToString();
                    break;
                case "PanelSeparatorBrush":
                    settings.PanelSeparatorBrush_Edit = color.ToString();
                    break;
                case "PopupBackgroundBrush":
                    settings.PopupBackgroundBrush_Edit = color.ToString();
                    break;
                case "PositiveRatingBrush":
                    settings.PositiveRatingBrush_Edit = color.ToString();
                    break;
                case "NegativeRatingBrush":
                    settings.NegativeRatingBrush_Edit = color.ToString();
                    break;
                case "MixedRatingBrush":
                    settings.MixedRatingBrush_Edit = color.ToString();
                    break;
            }
        }

        private void BtRestore_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}