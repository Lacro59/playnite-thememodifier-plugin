using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Playnite.SDK;
using CommonPluginsShared;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using ThemeModifier.Models;
using ThemeModifier.Services;
using YamlDotNet.Serialization;

namespace ThemeModifier.Views
{
    public partial class ThemeModifierSettingsView : UserControl
    {
        private static readonly ILogger logger = LogManager.GetLogger();
        private static IResourceProvider resources = new ResourceProvider();
        private IPlayniteAPI _PlayniteApi;

        private ThemeModifierSettings _settings;

        private List<ThemeElement> _ThemeDefault;
        private string _PluginUserDataPath;
        private string _PlayniteConfigurationPath;

        private TextBlock tbControl;
        private Label lControl;

        public static List<ThemeElement> SettingsThemeConstants = new List<ThemeElement>();

        private dynamic colorDefault = null;


        public ThemeModifierSettingsView(IPlayniteAPI PlayniteApi, ThemeModifierSettings settings, List<ThemeElement> ThemeDefault, string PlayniteConfigurationPath, string PluginUserDataPath)
        {
            _PlayniteApi = PlayniteApi;
            _settings = settings;
            _ThemeDefault = ThemeDefault;
            _PlayniteConfigurationPath = PlayniteConfigurationPath;
            _PluginUserDataPath = PluginUserDataPath;

            SettingsThemeConstants = ThemeClass.GetThemeActualConstants(settings, PlayniteApi);

            InitializeComponent();

            SetMenuItems();
            SetThemeConstants();
        }


        #region Theme constants
        private void SetThemeConstants()
        {
            int iRow = 0;

            foreach (ThemeElement themeElement in ThemeModifier.ThemeDefaultConstants)
            {
                Grid gd = new Grid();
                gd.Margin = new Thickness(0, 0, 0, 10);
                ColumnDefinition c1 = new ColumnDefinition();
                c1.Width = new GridLength(350);
                ColumnDefinition c2 = new ColumnDefinition();
                c2.Width = new GridLength(1, GridUnitType.Star);
                ColumnDefinition c3 = new ColumnDefinition();
                c3.Width = new GridLength(41);
                ColumnDefinition c4 = new ColumnDefinition();
                c4.Width = new GridLength(41);
                gd.ColumnDefinitions.Add(c1);
                gd.ColumnDefinitions.Add(c2);
                gd.ColumnDefinitions.Add(c3);
                gd.ColumnDefinitions.Add(c4);


                TextBlock tb = new TextBlock();
                Grid.SetColumn(tb, 0);
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.Name = "PART_ThemeConstantsLabel";
                tb.Text = themeElement.Name;
                tb.Tag = themeElement.Name;
                tb.Margin = new Thickness(0, 0, 10, 0);


                if (!themeElement.Description.IsNullOrEmpty())
                {
                    Label lbDescription = new Label();
                    Grid.SetColumn(lbDescription, 0);
                    lbDescription.Content = "";
                    lbDescription.FontFamily = (FontFamily)resources.GetResource("CommonFont");
                    lbDescription.ToolTip = themeElement.Name;
                    lbDescription.Margin = new Thickness(0, 0, 10, 0);
                    lbDescription.HorizontalAlignment = HorizontalAlignment.Right;

                    tb.Text = themeElement.Description;

                    gd.Children.Add(lbDescription);
                }

                gd.Children.Add(tb);


                FrameworkElement control = new FrameworkElement();

                Button btPickColor = new Button();
                btPickColor.Width = 41;
                btPickColor.Content = "\u270f";
                btPickColor.Click += BtPickColorConstants_Click;
                Grid.SetColumn(btPickColor, 2);

                Button btRestore = new Button();
                btRestore.Width = 41;
                btRestore.Content = "\u274C";
                btRestore.Click += BtRestoreConstants_Click;
                Grid.SetColumn(btRestore, 3);

                bool HadPickColor = false;

                dynamic elSaved = themeElement.Element;
                if (ThemeModifier.ThemeActualConstants.Find(x => x.Name == themeElement.Name) != null)
                {
                    Type TypeActual = themeElement.Element.GetType();
                    Type TypeNew = ThemeModifier.ThemeActualConstants.Find(x => x.Name == themeElement.Name).Element.GetType();

                    if (TypeActual != TypeNew)
                    {
                        if ((TypeActual.Name == "SolidColorBrush" || TypeActual.Name == "LinearGradientBrush")
                            && (TypeNew.Name == "SolidColorBrush" || TypeNew.Name == "LinearGradientBrush"))
                        {
                            elSaved = ThemeModifier.ThemeActualConstants.Find(x => x.Name == themeElement.Name).Element;
                        }
                        else
                        {
                            logger.Warn($"Different type for {themeElement.Name}");
                        }
                    }
                    else
                    {
                        elSaved = ThemeModifier.ThemeActualConstants.Find(x => x.Name == themeElement.Name).Element;
                    }
                }

                // Create control
                bool IsTitle = true;

                if (elSaved is bool)
                {
                    IsTitle = false;

                    gd.Tag = "bool";

                    control = new CheckBox();
                    ((CheckBox)control).Click += CbThemeConstants_Click;
                    control.VerticalAlignment = VerticalAlignment.Center;
                    ((CheckBox)control).IsChecked = (bool)elSaved;
                }

                if (elSaved is string)
                {
                    IsTitle = false;

                    gd.Tag = "string";

                    control = new TextBox();
                    control.KeyUp += TbThemeConstants_KeyUp;

                    ((TextBox)control).Text = (string)elSaved;
                }

                if (elSaved is double)
                {
                    IsTitle = false;

                    gd.Tag = "double";

                    control = new Slider();
                    ((Slider)control).ValueChanged += sThemeConstants_ValueChanged;
                    ((Slider)control).SmallChange = 0.1;
                    ((Slider)control).Minimum = themeElement.themeSliderLimit.Min;
                    ((Slider)control).Maximum = themeElement.themeSliderLimit.Max;
                    ((Slider)control).AutoToolTipPlacement = AutoToolTipPlacement.TopLeft;
                    ((Slider)control).AutoToolTipPrecision = 1;
                    ((Slider)control).VerticalAlignment = VerticalAlignment.Center;

                    double ActualValue = (double)elSaved;
                    if (ActualValue < themeElement.themeSliderLimit.Min)
                    {
                        ActualValue = themeElement.themeSliderLimit.Min;
                    }
                    if (ActualValue > themeElement.themeSliderLimit.Max)
                    {
                        ActualValue = themeElement.themeSliderLimit.Max;
                    }

                    ((Slider)control).Value = ActualValue;
                }

                if (elSaved is int)
                {
                    IsTitle = false;

                    gd.Tag = "int";

                    control = new Slider();
                    ((Slider)control).ValueChanged += sThemeConstants_ValueChanged;
                    ((Slider)control).SmallChange = 1;
                    ((Slider)control).Minimum = themeElement.themeSliderLimit.Min;
                    ((Slider)control).Maximum = themeElement.themeSliderLimit.Max;
                    ((Slider)control).AutoToolTipPlacement = AutoToolTipPlacement.TopLeft;
                    ((Slider)control).VerticalAlignment = VerticalAlignment.Center;

                    int ActualValue = (int)elSaved;
                    if (ActualValue < themeElement.themeSliderLimit.Min)
                    {
                        ActualValue = (int)themeElement.themeSliderLimit.Min;
                    }
                    if (ActualValue > themeElement.themeSliderLimit.Max)
                    {
                        ActualValue = (int)themeElement.themeSliderLimit.Max;
                    }

                    ((Slider)control).Value = ActualValue;
                }

                if (elSaved is Color)
                {
                    IsTitle = false;

                    gd.Tag = "color";

                    control = new TextBlock();
                    control.Width = 100;
                    control.HorizontalAlignment = HorizontalAlignment.Left;

                    ((TextBlock)control).Background = new SolidColorBrush((Color)elSaved);

                    HadPickColor = true;
                }

                if (elSaved is SolidColorBrush)
                {
                    IsTitle = false;

                    gd.Tag = "solidcolorbrush";

                    control = new TextBlock();
                    control.Width = 100;
                    control.HorizontalAlignment = HorizontalAlignment.Left;

                    ((TextBlock)control).Background = (SolidColorBrush)elSaved;

                    HadPickColor = true;
                }

                if (elSaved is LinearGradientBrush)
                {
                    IsTitle = false;

                    gd.Tag = "lineargradientbrush";

                    control = new TextBlock();
                    control.Width = 100;
                    control.HorizontalAlignment = HorizontalAlignment.Left;

                    ((TextBlock)control).Background = (LinearGradientBrush)elSaved;

                    HadPickColor = true;
                }

                if (elSaved is Visibility)
                {
                    IsTitle = false;

                    gd.Tag = "visibility";

                    control = new ComboBox();
                    ((ComboBox)control).SelectionChanged += CbThemeConstants_SelectionChanged;
                    ComboBoxItem cbItem1 = new ComboBoxItem();
                    cbItem1.Tag = Visibility.Collapsed;
                    cbItem1.Content = "Collapsed";
                    //ComboBoxItem cbItem2 = new ComboBoxItem();
                    //cbItem2.Tag = Visibility.Hidden;
                    //cbItem2.Content = "Hidden";
                    ComboBoxItem cbItem3 = new ComboBoxItem();
                    cbItem3.Tag = Visibility.Visible;
                    cbItem3.Content = "Visible";

                    ((ComboBox)control).Items.Add(cbItem1);
                    //((ComboBox)control).Items.Add(cbItem2);
                    ((ComboBox)control).Items.Add(cbItem3);

                    switch (elSaved)
                    {
                        case Visibility.Collapsed:
                            ((ComboBox)control).SelectedIndex = 0;
                            break;
                        case Visibility.Hidden:
                            ((ComboBox)control).SelectedIndex = 1;
                            break;
                        case Visibility.Visible:
                            ((ComboBox)control).SelectedIndex = 2;
                            break;
                    }
                }

                if (elSaved is VerticalAlignment)
                {
                    IsTitle = false;

                    gd.Tag = "verticalalignment";

                    control = new ComboBox();
                    ((ComboBox)control).SelectionChanged += CbThemeConstants_SelectionChanged;
                    ComboBoxItem cbItem1 = new ComboBoxItem();
                    cbItem1.Tag = VerticalAlignment.Bottom;
                    cbItem1.Content = "Bottom";
                    ComboBoxItem cbItem2 = new ComboBoxItem();
                    cbItem2.Tag = VerticalAlignment.Center;
                    cbItem2.Content = "Center";
                    ComboBoxItem cbItem3 = new ComboBoxItem();
                    cbItem3.Tag = VerticalAlignment.Stretch;
                    cbItem3.Content = "Stretch";
                    ComboBoxItem cbItem4 = new ComboBoxItem();
                    cbItem4.Tag = VerticalAlignment.Top;
                    cbItem4.Content = "Top";

                    ((ComboBox)control).Items.Add(cbItem1);
                    ((ComboBox)control).Items.Add(cbItem2);
                    ((ComboBox)control).Items.Add(cbItem3);
                    ((ComboBox)control).Items.Add(cbItem4);

                    switch (elSaved)
                    {
                        case VerticalAlignment.Bottom:
                            ((ComboBox)control).SelectedIndex = 0;
                            break;
                        case VerticalAlignment.Center:
                            ((ComboBox)control).SelectedIndex = 1;
                            break;
                        case VerticalAlignment.Stretch:
                            ((ComboBox)control).SelectedIndex = 2;
                            break;
                        case VerticalAlignment.Top:
                            ((ComboBox)control).SelectedIndex = 2;
                            break;
                    }
                }

                if (elSaved is HorizontalAlignment)
                {
                    IsTitle = false;

                    gd.Tag = "horizontalalignment";

                    control = new ComboBox();
                    ((ComboBox)control).SelectionChanged += CbThemeConstants_SelectionChanged;
                    ComboBoxItem cbItem1 = new ComboBoxItem();
                    cbItem1.Tag = HorizontalAlignment.Center;
                    cbItem1.Content = "Center";
                    ComboBoxItem cbItem2 = new ComboBoxItem();
                    cbItem2.Tag = HorizontalAlignment.Left;
                    cbItem2.Content = "Left";
                    ComboBoxItem cbItem3 = new ComboBoxItem();
                    cbItem3.Tag = HorizontalAlignment.Right;
                    cbItem3.Content = "Right";
                    ComboBoxItem cbItem4 = new ComboBoxItem();
                    cbItem4.Tag = HorizontalAlignment.Stretch;
                    cbItem4.Content = "Stretch";

                    ((ComboBox)control).Items.Add(cbItem1);
                    ((ComboBox)control).Items.Add(cbItem2);
                    ((ComboBox)control).Items.Add(cbItem3);
                    ((ComboBox)control).Items.Add(cbItem4);

                    switch (elSaved)
                    {
                        case HorizontalAlignment.Center:
                            ((ComboBox)control).SelectedIndex = 0;
                            break;
                        case HorizontalAlignment.Left:
                            ((ComboBox)control).SelectedIndex = 1;
                            break;
                        case HorizontalAlignment.Right:
                            ((ComboBox)control).SelectedIndex = 2;
                            break;
                        case HorizontalAlignment.Stretch:
                            ((ComboBox)control).SelectedIndex = 2;
                            break;
                    }
                }

                if (IsTitle)
                {
                    tb.FontWeight = FontWeights.Bold;
                    tb.TextDecorations = TextDecorations.Underline;
                }

                control.Name = "PART_ThemeConstantsControl";
                control.Tag = themeElement.Name;
                Grid.SetColumn(control, 1);
                gd.Children.Add(control);
                if (HadPickColor)
                {
                    gd.Children.Add(btPickColor);
                }

                if (!IsTitle)
                {
                    gd.Children.Add(btRestore);
                }

                // Add control
                Grid.SetColumn(gd, 0);
                Grid.SetRow(gd, iRow);

                PART_ConstantsThemeEdit.Children.Add(gd);

                iRow++;
            }

            PART_ConstantsThemeEdit.UpdateLayout();
        }

        private void CbThemeConstants_Click(object sender, RoutedEventArgs e)
        {
            Grid gdParent = (Grid)((FrameworkElement)sender).Parent;

            string Name = (string)((Label)IntegrationUI.SearchElementByName("PART_ThemeConstantsLabel", gdParent)).Tag;
            bool Element = (bool)((CheckBox)sender).IsChecked;

            if (SettingsThemeConstants.Find(x => x.Name == Name) != null)
            {
                SettingsThemeConstants.Find(x => x.Name == Name).Element = Element;
            }
            else
            {
                SettingsThemeConstants.Add(new ThemeElement { Name = Name, Element = Element });
            }
            Common.LogDebug(true, $"SettingsThemeConstants: {JsonConvert.SerializeObject(SettingsThemeConstants)}");
        }

        private void TbThemeConstants_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                Grid gdParent = (Grid)((FrameworkElement)sender).Parent;

                string Name = (string)((Label)IntegrationUI.SearchElementByName("PART_ThemeConstantsLabel", gdParent)).Tag;
                string Element = ((TextBox)sender).Text;

                if (SettingsThemeConstants.Find(x => x.Name == Name) != null)
                {
                    SettingsThemeConstants.Find(x => x.Name == Name).Element = Element;
                }
                else
                {
                    SettingsThemeConstants.Add(new ThemeElement { Name = Name, Element = Element });
                }
                Common.LogDebug(true, $"SettingsThemeConstants: {JsonConvert.SerializeObject(SettingsThemeConstants)}");
            }
            catch (Exception ex)
            {
                Common.LogError(ex, true);
            }
        }

        private void CbThemeConstants_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Grid gdParent = (Grid)((FrameworkElement)sender).Parent;

                string Name = (string)((Label)IntegrationUI.SearchElementByName("PART_ThemeConstantsLabel", gdParent)).Tag;
                dynamic Element = ((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag;

                if (SettingsThemeConstants.Find(x => x.Name == Name) != null)
                {
                    SettingsThemeConstants.Find(x => x.Name == Name).Element = Element;
                }
                else
                {
                    SettingsThemeConstants.Add(new ThemeElement { Name = Name, Element = Element });
                }
                Common.LogDebug(true, $"SettingsThemeConstants: {JsonConvert.SerializeObject(SettingsThemeConstants)}");
            }
            catch (Exception ex)
            {
                Common.LogError(ex,true);
            }
        }

        private void sThemeConstants_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                Grid gdParent = (Grid)((FrameworkElement)sender).Parent;

                string Name = (string)((Label)IntegrationUI.SearchElementByName("PART_ThemeConstantsLabel", gdParent)).Tag;
                string Type = (string)gdParent.Tag;
                dynamic Element = null;
                if (Type.ToLower() == "int")
                {
                    Element = (int)((Slider)sender).Value;
                }
                if (Type.ToLower() == "double")
                {
                    Element = (double)Math.Round(((Slider)sender).Value, 1);
                }

                if (SettingsThemeConstants.Find(x => x.Name == Name) != null)
                {
                    SettingsThemeConstants.Find(x => x.Name == Name).Element = Element;
                }
                else
                {
                    SettingsThemeConstants.Add(new ThemeElement { Name = Name, Element = Element });
                }
                Common.LogDebug(true, $"SettingsThemeConstants: {JsonConvert.SerializeObject(SettingsThemeConstants)}");
            }
            catch (Exception ex)
            {
                Common.LogError(ex, true);
            }
        }

        private void BtPickColorConstants_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Grid gdParent = (Grid)((FrameworkElement)sender).Parent;

                tbControl = gdParent.Children.OfType<TextBlock>().FirstOrDefault();
                lControl = gdParent.Children.OfType<Label>().FirstOrDefault();

                if (tbControl.Background is SolidColorBrush)
                {
                    PART_SelectorColorPickerConstants.SetColors((SolidColorBrush)tbControl.Background);
                }
                if (tbControl.Background is LinearGradientBrush)
                {
                    LinearGradientBrush linearGradientBrush = (LinearGradientBrush)tbControl.Background;
                    PART_SelectorColorPickerConstants.SetColors(linearGradientBrush);
                }

                PART_SelectorColorConstants.Visibility = Visibility.Visible;
                PART_ThemeConstants.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false);
            }
        }

        private void BtRestoreConstants_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Grid gdParent = (Grid)((FrameworkElement)sender).Parent;

                string elName = (string)((Label)IntegrationUI.SearchElementByName("PART_ThemeConstantsLabel", gdParent)).Tag;
                string elType = (string)gdParent.Tag;
                FrameworkElement elControl = IntegrationUI.SearchElementByName("PART_ThemeConstantsControl", gdParent);

                dynamic elDefault = ThemeModifier.ThemeDefaultConstants.Find(x => x.Name == elName).Element;

                if (elDefault is bool)
                {
                    gdParent.Tag = "bool";
                    ((CheckBox)elControl).IsChecked = (bool)elDefault;
                    CbThemeConstants_Click(elControl, null);
                }

                if (elDefault is string)
                {
                    gdParent.Tag = "string";
                    ((TextBox)elControl).Text = (string)elDefault;
                    TbThemeConstants_KeyUp(elControl, null);
                }

                if (elDefault is double)
                {
                    gdParent.Tag = "double";
                    ((Slider)elControl).Value = (double)elDefault;
                    sThemeConstants_ValueChanged(elControl, null);
                }

                if (elDefault is int)
                {
                    gdParent.Tag = "int";
                    ((Slider)elControl).Value = (int)elDefault;
                    sThemeConstants_ValueChanged(elControl, null);
                }

                if (elDefault is Color)
                {
                    gdParent.Tag = "color";
                    ((TextBlock)elControl).Background = new SolidColorBrush((Color)elDefault);

                    tbControl = gdParent.Children.OfType<TextBlock>().FirstOrDefault();
                    lControl = gdParent.Children.OfType<Label>().FirstOrDefault();
                    colorDefault = (Color)elDefault;
                    PART_TM_ColorOKConstants_Click(elControl, null);
                }

                if (elDefault is SolidColorBrush)
                {
                    gdParent.Tag = "solidcolorbrush";
                    ((TextBlock)elControl).Background = (SolidColorBrush)elDefault;

                    tbControl = gdParent.Children.OfType<TextBlock>().FirstOrDefault();
                    lControl = gdParent.Children.OfType<Label>().FirstOrDefault();
                    colorDefault = (SolidColorBrush)elDefault;
                    PART_TM_ColorOKConstants_Click(elControl, null);
                }

                if (elDefault is LinearGradientBrush)
                {
                    gdParent.Tag = "lineargradientbrush";
                    ((TextBlock)elControl).Background = (LinearGradientBrush)elDefault;

                    tbControl = gdParent.Children.OfType<TextBlock>().FirstOrDefault();
                    lControl = gdParent.Children.OfType<Label>().FirstOrDefault();
                    colorDefault = (LinearGradientBrush)elDefault;
                    PART_TM_ColorOKConstants_Click(elControl, null);
                }

                if (elDefault is Visibility)
                {
                    gdParent.Tag = "visibility";

                    switch (elDefault)
                    {
                        case Visibility.Collapsed:
                            ((ComboBox)elControl).SelectedIndex = 0;
                            break;
                        case Visibility.Hidden:
                            ((ComboBox)elControl).SelectedIndex = 1;
                            break;
                        case Visibility.Visible:
                            ((ComboBox)elControl).SelectedIndex = 2;
                            break;
                    }

                    CbThemeConstants_SelectionChanged(elControl, null);
                }

                if (elDefault is VerticalAlignment)
                {
                    gdParent.Tag = "verticalalignment";

                    switch (elDefault)
                    {
                        case VerticalAlignment.Bottom:
                            ((ComboBox)elControl).SelectedIndex = 0;
                            break;
                        case VerticalAlignment.Center:
                            ((ComboBox)elControl).SelectedIndex = 1;
                            break;
                        case VerticalAlignment.Stretch:
                            ((ComboBox)elControl).SelectedIndex = 2;
                            break;
                        case VerticalAlignment.Top:
                            ((ComboBox)elControl).SelectedIndex = 2;
                            break;
                    }

                    CbThemeConstants_SelectionChanged(elControl, null);
                }

                if (elDefault is HorizontalAlignment)
                {
                    gdParent.Tag = "horizontalalignment";

                    switch (elDefault)
                    {
                        case HorizontalAlignment.Center:
                            ((ComboBox)elControl).SelectedIndex = 0;
                            break;
                        case HorizontalAlignment.Left:
                            ((ComboBox)elControl).SelectedIndex = 1;
                            break;
                        case HorizontalAlignment.Right:
                            ((ComboBox)elControl).SelectedIndex = 2;
                            break;
                        case HorizontalAlignment.Stretch:
                            ((ComboBox)elControl).SelectedIndex = 2;
                            break;
                    }

                    CbThemeConstants_SelectionChanged(elControl, null);
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false);
            }
        }

        private void PART_TM_ColorOKConstants_Click(object sender, RoutedEventArgs e)
        {
            Color color = default(Color);
            SolidColorBrush colorBrush = default(SolidColorBrush);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();

            if (tbControl != null && lControl != null)
            {
                dynamic elSaved = null;
                string Name = (string)tbControl.Tag;
                string Type = (string)((Grid)tbControl.Parent).Tag;
                double Opacity = 1;

                if (colorDefault == null)
                {
                    if (PART_SelectorColorPickerConstants.IsSimpleColor)
                    {
                        color = PART_SelectorColorPickerConstants.SimpleColor;
                        colorBrush = PART_SelectorColorPickerConstants.SimpleSolidColorBrush;
                        tbControl.Background = colorBrush;

                        if (Type == "color")
                        {
                            elSaved = color;
                        }
                        else
                        {
                            ((Grid)tbControl.Parent).Tag = "solidcolorbrush";
                            Opacity = colorBrush.Opacity;
                            elSaved = colorBrush;
                        }
                    }
                    else
                    {
                        linearGradientBrush = PART_SelectorColorPickerConstants.linearGradientBrush;
                        tbControl.Background = linearGradientBrush;

                        if (Type == "color")
                        {
                            elSaved = linearGradientBrush.GradientStops[0].Color;
                            tbControl.Background = new SolidColorBrush(elSaved);
                        }
                        else
                        {
                            ((Grid)tbControl.Parent).Tag = "lineargradientbrush";
                            elSaved = linearGradientBrush;
                        }
                    }
                }

                if (colorDefault != null)
                {
                    elSaved = colorDefault;
                    colorDefault = null;
                }

                if (SettingsThemeConstants.Find(x => x.Name == Name) != null)
                {
                    SettingsThemeConstants.Find(x => x.Name == Name).Element = elSaved;
                    SettingsThemeConstants.Find(x => x.Name == Name).Opacity = Opacity;
                }
                else
                {
                    SettingsThemeConstants.Add(new ThemeElement { Name = Name, Element = elSaved, Opacity = Opacity });
                }
            }
            else
            {
                logger.Warn("One control is undefined");
            }

            PART_SelectorColorConstants.Visibility = Visibility.Collapsed;
            PART_ThemeConstants.Visibility = Visibility.Visible;
        }

        private void PART_TM_ColorCancelConstants_Click(object sender, RoutedEventArgs e)
        {
            PART_SelectorColorConstants.Visibility = Visibility.Collapsed;
            PART_ThemeConstants.Visibility = Visibility.Visible;
        }
        #endregion


        #region Theme color
        private void BtPickColor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tbControl = ((StackPanel)((FrameworkElement)sender).Parent).Children.OfType<TextBlock>().FirstOrDefault();
                lControl = ((StackPanel)((FrameworkElement)sender).Parent).Children.OfType<Label>().FirstOrDefault();

                if (tbControl.Background is SolidColorBrush)
                {
                    //Color color = ((SolidColorBrush)tbControl.Background).Color;
                    //PART_SelectorColorPicker.SetColors(color);
                    PART_SelectorColorPicker.SetColors((SolidColorBrush)tbControl.Background);
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
                Common.LogError(ex, false);
            }
        }

        private void BtRestore_Click(object sender, RoutedEventArgs e)
        {
            try { 
                TextBlock tbControl = ((StackPanel)((FrameworkElement)sender).Parent).Children.OfType<TextBlock>().FirstOrDefault();
                Label lControl = ((StackPanel)((FrameworkElement)sender).Parent).Children.OfType<Label>().FirstOrDefault();

                ThemeElement finded = _ThemeDefault.Find(x => x.Name == lControl.Content.ToString());

                tbControl.Background = finded.Element;

                ThemeClass.SetThemeColor(lControl.Content.ToString(), null, _settings, finded.Element);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false);
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
                        ((TextBlock)control).Background = themeElement.Element;
                        ThemeClass.SetThemeColor(themeElement.Name, null, _settings, themeElement.Element);
                    }
                    else
                    {
                        logger.Warn($"Bad control {"tb" + themeElement.Name}: {control.ToString()}");
                    }                    
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false);
            }
        }


        private void PART_TM_ColorOK_Click(object sender, RoutedEventArgs e)
        {
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();

            if (tbControl != null && lControl != null)
            {
                if (PART_SelectorColorPicker.IsSimpleColor)
                {
                    tbControl.Background = PART_SelectorColorPicker.SimpleSolidColorBrush;
                    ThemeClass.SetThemeColor(lControl.Content.ToString(), PART_SelectorColorPicker.SimpleSolidColorBrush, _settings);
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
                logger.Warn("One control is undefined");
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


        #region Icon changer
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ThemeModifierSettingsViewModel PluginSettings = (ThemeModifierSettingsViewModel)this.DataContext;

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

            PluginSettings.Settings.UseIconCircle = (bool)tbUseIconCircle.IsChecked;
            PluginSettings.Settings.UseIconClock = (bool)tbUseIconClock.IsChecked;
            PluginSettings.Settings.UseIconSquareCorne = (bool)tbUseIconSquareCorne.IsChecked;
            PluginSettings.Settings.UseIconWe4ponx = (bool)tbUseIconWe4ponx.IsChecked;
        }

        private void BtSetIcons_Click(object sender, RoutedEventArgs e)
        {
            _settings.UseIconCircle = (bool)tbUseIconCircle.IsChecked;
            _settings.UseIconClock = (bool)tbUseIconClock.IsChecked;
            _settings.UseIconSquareCorne = (bool)tbUseIconSquareCorne.IsChecked;
            _settings.UseIconWe4ponx = (bool)tbUseIconWe4ponx.IsChecked;
        }
        #endregion


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
                _PlayniteApi.Dialogs.ShowMessage(resources.GetString("LOCThemeModifierManageSaveOk"), "ThemeModifier");
                SetMenuItems();
            }
            else
            {
                _PlayniteApi.Dialogs.ShowMessage(resources.GetString("LOCThemeModifierManageSaveKo"), "ThemeModifier");
            }

            PART_EditThemeMenuSaveName.Text = string.Empty;
            PART_EditThemeMenuBtSave.IsEnabled = false;
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
                Common.LogError(ex, false, $"Error on load {PathFileName}");
            }
        }

        private void PART_EditThemeMenuBtRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string PathFileName = ((FrameworkElement)sender).Tag.ToString();
                if (_PlayniteApi.Dialogs.ShowMessage(resources.GetString("LOCRemoveLabel") + " " + PathFileName,  "ThemeModifier", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ThemeClass.DeleteThemeColors(PathFileName, _PluginUserDataPath);
                    SetMenuItems();
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, $"Error on remove {((FrameworkElement)sender).Tag.ToString()}");
            }
        }

        private void PART_EditThemeMenuImport_Click(object sender, RoutedEventArgs e)
        {
            string targetPath = _PlayniteApi.Dialogs.SelectFile("json file|*.json");

            if (!targetPath.IsNullOrEmpty())
            {
                ThemeColors themeColors = new ThemeColors();

                try
                {
                    themeColors = JsonConvert.DeserializeObject<ThemeColors>(File.ReadAllText(targetPath));
                }
                catch
                {
                    _PlayniteApi.Dialogs.ShowErrorMessage(resources.GetString("LOCThemeModifierManageNoFile"), "ThemeModifier");
                    return;
                }

                if (themeColors.Name.IsNullOrEmpty())
                {
                    _PlayniteApi.Dialogs.ShowErrorMessage(resources.GetString("LOCThemeModifierManageNoFile"), "ThemeModifier");
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
            string targetPath = _PlayniteApi.Dialogs.SaveFile("json file|*.json", true);            

            if (!targetPath.IsNullOrEmpty())
            {
                File.Copy(PathFileName, targetPath, true);
            }
        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((TabControl)sender).SelectedIndex == 1 || ((TabControl)sender).SelectedIndex == 3)
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
        #endregion
    }
}
