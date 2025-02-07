﻿using Playnite.SDK;
using Playnite.SDK.Data;
using CommonPluginsShared;
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
        private static ILogger Logger => LogManager.GetLogger();

        private ThemeModifierSettings Settings { get; set; }

        private List<ThemeElement> ThemeDefault { get; set; }
        private string PluginUserDataPath { get; set; }
        private string PlayniteConfigurationPath { get; set; }

        private TextBlock TbControl { get; set; }
        private Label LControl { get; set; }

        public static List<ThemeElement> SettingsThemeConstants { get; set; } = new List<ThemeElement>();

        private dynamic ColorDefault { get; set; } = null;


        public ThemeModifierSettingsView(ThemeModifierSettings settings, List<ThemeElement> themeDefault, string playniteConfigurationPath, string pluginUserDataPath)
        {
            Settings = settings;
            ThemeDefault = themeDefault;
            PlayniteConfigurationPath = playniteConfigurationPath;
            PluginUserDataPath = pluginUserDataPath;

            SettingsThemeConstants = ThemeClass.GetThemeActualConstants(settings);

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
                tb.Tag = themeElement.Name;
                tb.Margin = new Thickness(0, 0, 10, 0);
                tb.TextWrapping = TextWrapping.Wrap;

                tb.Text = themeElement.Name;
                if (!themeElement.Description.IsNullOrEmpty())
                {
                    tb.Text = themeElement.Description;
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
                            Logger.Warn($"Different type for {themeElement.Name}");
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
                    ComboBoxItem cbItem3 = new ComboBoxItem();
                    cbItem3.Tag = Visibility.Visible;
                    cbItem3.Content = "Visible";

                    ((ComboBox)control).Items.Add(cbItem1);
                    ((ComboBox)control).Items.Add(cbItem3);

                    switch (elSaved)
                    {
                        case Visibility.Collapsed:
                            ((ComboBox)control).SelectedIndex = 0;
                            break;
                        case Visibility.Visible:
                            ((ComboBox)control).SelectedIndex = 1;
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
                    if (iRow != 0)
                    {
                        PART_ConstantsThemeEdit.RowDefinitions[iRow].Height = new GridLength(20);
                        iRow++;
                    }

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
            try
            {
                Grid gdParent = (Grid)((FrameworkElement)sender).Parent;

                string name = (string)((TextBlock)UI.SearchElementByName("PART_ThemeConstantsLabel", gdParent))?.Tag;
                bool element = (bool)((CheckBox)sender).IsChecked;

                if (SettingsThemeConstants == null || name.IsNullOrEmpty())
                {
                    return;
                }

                if (SettingsThemeConstants.Find(x => x.Name == name) != null)
                {
                    SettingsThemeConstants.Find(x => x.Name == name).Element = element;
                }
                else
                {
                    SettingsThemeConstants.Add(new ThemeElement { Name = name, Element = element });
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, true);
            }
        }

        private void TbThemeConstants_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                Grid gdParent = (Grid)((FrameworkElement)sender).Parent;

                string name = (string)((TextBlock)UI.SearchElementByName("PART_ThemeConstantsLabel", gdParent)).Tag;
                string element = ((TextBox)sender).Text;

                if (SettingsThemeConstants == null || name.IsNullOrEmpty())
                {
                    return;
                }

                if (SettingsThemeConstants.Find(x => x.Name == name) != null)
                {
                    SettingsThemeConstants.Find(x => x.Name == name).Element = element;
                }
                else
                {
                    SettingsThemeConstants.Add(new ThemeElement { Name = name, Element = element });
                }
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

                string name = (string)((TextBlock)UI.SearchElementByName("PART_ThemeConstantsLabel", gdParent))?.Tag;
                dynamic element = ((ComboBoxItem)((ComboBox)sender).SelectedItem)?.Tag;

                if (SettingsThemeConstants == null || name.IsNullOrEmpty() || element == null)
                {
                    return;
                }

                if (SettingsThemeConstants.Find(x => x.Name == name) != null)
                {
                    SettingsThemeConstants.Find(x => x.Name == name).Element = element;
                }
                else
                {
                    SettingsThemeConstants.Add(new ThemeElement { Name = name, Element = element });
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, true, "ThemeModifier");
            }
        }

        private void sThemeConstants_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                Grid gdParent = (Grid)((FrameworkElement)sender).Parent;
                if ((TextBlock)UI.SearchElementByName("PART_ThemeConstantsLabel", gdParent) == null)
                {
                    return;
                }

                string name = (string)((TextBlock)UI.SearchElementByName("PART_ThemeConstantsLabel", gdParent)).Tag;
                string type = (string)gdParent.Tag;
                dynamic element = null;
                if (type.ToLower() == "int")
                {
                    element = (int)((Slider)sender).Value;
                }
                if (type.ToLower() == "double")
                {
                    element = (double)Math.Round(((Slider)sender).Value, 1);
                }

                if (SettingsThemeConstants.Find(x => x.Name == name) != null)
                {
                    SettingsThemeConstants.Find(x => x.Name == name).Element = element;
                }
                else
                {
                    SettingsThemeConstants.Add(new ThemeElement { Name = name, Element = element });
                }
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
                TbControl = (TextBlock)UI.SearchElementByName("PART_ThemeConstantsControl", gdParent);

                if (TbControl.Background is SolidColorBrush)
                {
                    PART_SelectorColorPickerConstants.IsSimpleColor = true;
                    PART_SelectorColorPickerConstants.SetColors((SolidColorBrush)TbControl.Background);
                }
                if (TbControl.Background is LinearGradientBrush)
                {
                    PART_SelectorColorPickerConstants.IsSimpleColor = false;
                    LinearGradientBrush linearGradientBrush = (LinearGradientBrush)TbControl.Background;
                    PART_SelectorColorPickerConstants.SetColors(linearGradientBrush);
                }

                PART_SelectorColorConstants.Visibility = Visibility.Visible;
                PART_ThemeConstants.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, true, "ThemeModifier");
            }
        }

        private void BtRestoreConstants_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Grid gdParent = (Grid)((FrameworkElement)sender).Parent;

                string elName = (string)((TextBlock)UI.SearchElementByName("PART_ThemeConstantsLabel", gdParent)).Tag;
                string elType = (string)gdParent.Tag;
                FrameworkElement elControl = UI.SearchElementByName("PART_ThemeConstantsControl", gdParent);

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

                    TbControl = gdParent.Children.OfType<TextBlock>().FirstOrDefault();
                    LControl = gdParent.Children.OfType<Label>().FirstOrDefault();
                    ColorDefault = (Color)elDefault;
                    PART_TM_ColorOKConstants_Click(elControl, null);
                }

                if (elDefault is SolidColorBrush)
                {
                    gdParent.Tag = "solidcolorbrush";
                    ((TextBlock)elControl).Background = (SolidColorBrush)elDefault;

                    TbControl = gdParent.Children.OfType<TextBlock>().FirstOrDefault();
                    LControl = gdParent.Children.OfType<Label>().FirstOrDefault();
                    ColorDefault = (SolidColorBrush)elDefault;
                    PART_TM_ColorOKConstants_Click(elControl, null);
                }

                if (elDefault is LinearGradientBrush)
                {
                    gdParent.Tag = "lineargradientbrush";
                    ((TextBlock)elControl).Background = (LinearGradientBrush)elDefault;

                    TbControl = gdParent.Children.OfType<TextBlock>().FirstOrDefault();
                    LControl = gdParent.Children.OfType<Label>().FirstOrDefault();
                    ColorDefault = (LinearGradientBrush)elDefault;
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
                        case Visibility.Visible:
                            ((ComboBox)elControl).SelectedIndex = 1;
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
                Common.LogError(ex, false, true, "ThemeModifier");
            }
        }

        private void PART_TM_ColorOKConstants_Click(object sender, RoutedEventArgs e)
        {
            Color color = default;
            SolidColorBrush colorBrush = default;
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();

            if (TbControl != null)
            {
                dynamic elSaved = null;
                string name = (string)TbControl.Tag;
                string type = (string)((Grid)TbControl.Parent).Tag;
                double opacity = 1;

                if (ColorDefault == null)
                {
                    if (PART_SelectorColorPickerConstants.IsSimpleColor)
                    {
                        color = PART_SelectorColorPickerConstants.SimpleColor;
                        colorBrush = PART_SelectorColorPickerConstants.GetSolidColorBrush();
                        TbControl.Background = colorBrush;

                        if (type == "color")
                        {
                            elSaved = color;
                        }
                        else
                        {
                            ((Grid)TbControl.Parent).Tag = "solidcolorbrush";
                            opacity = colorBrush.Opacity;
                            elSaved = colorBrush;
                        }
                    }
                    else
                    {
                        linearGradientBrush = PART_SelectorColorPickerConstants.GetLinearGradientBrush();
                        TbControl.Background = linearGradientBrush;

                        if (type == "color")
                        {
                            elSaved = linearGradientBrush.GradientStops[0].Color;
                            TbControl.Background = new SolidColorBrush(elSaved);
                        }
                        else
                        {
                            ((Grid)TbControl.Parent).Tag = "lineargradientbrush";
                            elSaved = linearGradientBrush;
                        }
                    }
                }

                if (ColorDefault != null)
                {
                    elSaved = ColorDefault;
                    ColorDefault = null;
                }

                if (SettingsThemeConstants.Find(x => x.Name == name) != null)
                {
                    SettingsThemeConstants.Find(x => x.Name == name).Element = elSaved;
                    SettingsThemeConstants.Find(x => x.Name == name).Opacity = opacity;
                }
                else
                {
                    SettingsThemeConstants.Add(new ThemeElement { Name = name, Element = elSaved, Opacity = opacity });
                }
            }
            else
            {
                Logger.Warn("One control is undefined");
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
                TbControl = null;
                LControl = null;

                TbControl = ((StackPanel)((FrameworkElement)sender).Parent).Children.OfType<TextBlock>().FirstOrDefault();
                LControl = ((StackPanel)((FrameworkElement)sender).Parent).Children.OfType<Label>().FirstOrDefault();

                if (TbControl.Background is SolidColorBrush brush)
                {
                    PART_SelectorColorPicker.SetColors(brush);
                }
                if (TbControl.Background is LinearGradientBrush linearGradientBrush)
                {
                    PART_SelectorColorPicker.SetColors(linearGradientBrush);
                }

                PART_SelectorColor.Visibility = Visibility.Visible;
                PART_ThemeColor.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, true, "ThemeModifier");
            }
        }

        private void BtRestore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBlock tbControl = ((StackPanel)((FrameworkElement)sender).Parent).Children.OfType<TextBlock>().FirstOrDefault();
                Label lControl = ((StackPanel)((FrameworkElement)sender).Parent).Children.OfType<Label>().FirstOrDefault();

                ThemeElement finded = ThemeDefault.Find(x => x.Name == lControl.Content.ToString());

                tbControl.Background = finded.Element;

                ThemeClass.SetThemeColor(lControl.Content.ToString(), null, Settings, finded.Element);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, true, "ThemeModifier");
            }
        }

        private void BtRestoreDefault_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ThemeClass.RestoreColor(ThemeDefault, Settings);

                foreach (ThemeElement themeElement in ThemeDefault)
                {
                    object control = FindName("tb" + themeElement.Name);
                    if (control is TextBlock block)
                    {
                        block.Background = themeElement.Element;
                        ThemeClass.SetThemeColor(themeElement.Name, null, Settings, themeElement.Element);
                    }
                    else
                    {
                        Logger.Warn($"Bad control {"tb" + themeElement.Name}: {control}");
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, true, "ThemeModifier");
            }
        }


        private void PART_TM_ColorOK_Click(object sender, RoutedEventArgs e)
        {
            if (TbControl != null && LControl != null)
            {
                if (PART_SelectorColorPicker.IsSimpleColor)
                {
                    TbControl.Background = PART_SelectorColorPicker.GetSolidColorBrush();
                    ThemeClass.SetThemeColor(LControl.Content.ToString(), PART_SelectorColorPicker.GetSolidColorBrush(), Settings);
                }
                else
                {
                    TbControl.Background = PART_SelectorColorPicker.GetLinearGradientBrush();
                    ThemeClass.SetThemeColor(LControl.Content.ToString(), PART_SelectorColorPicker.GetLinearGradientBrush(), Settings);
                }
            }
            else
            {
                Logger.Warn("One control is undefined");
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
            ThemeModifierSettingsViewModel pluginSettings = (ThemeModifierSettingsViewModel)DataContext;

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

            pluginSettings.Settings.UseIconCircle = (bool)tbUseIconCircle.IsChecked;
            pluginSettings.Settings.UseIconClock = (bool)tbUseIconClock.IsChecked;
            pluginSettings.Settings.UseIconSquareCorne = (bool)tbUseIconSquareCorne.IsChecked;
            pluginSettings.Settings.UseIconWe4ponx = (bool)tbUseIconWe4ponx.IsChecked;
        }

        private void BtSetIcons_Click(object sender, RoutedEventArgs e)
        {
            Settings.UseIconCircle = (bool)tbUseIconCircle.IsChecked;
            Settings.UseIconClock = (bool)tbUseIconClock.IsChecked;
            Settings.UseIconSquareCorne = (bool)tbUseIconSquareCorne.IsChecked;
            Settings.UseIconWe4ponx = (bool)tbUseIconWe4ponx.IsChecked;
        }
        #endregion


        #region Menu
        private void SetMenuItems()
        {
            Thread.Sleep(500);
            List<ThemeColors> listItems = ThemeClass.GetListThemeColors(PluginUserDataPath);

            if (listItems == null)
            {
                Thread.Sleep(500);
                listItems = ThemeClass.GetListThemeColors(PluginUserDataPath);
            }

            listItems.Sort((x, y) => x.Name.CompareTo(y.Name));

            PART_EditThemeMenuLoad.ItemsSource = listItems;
            PART_EditThemeMenuRemove.ItemsSource = listItems;
            PART_EditThemeMenuExport.ItemsSource = listItems;
        }


        private void PART_EditThemeMenuBtSave_Click(object sender, RoutedEventArgs e)
        {
            string themeName = PART_EditThemeMenuSaveName.Text;
            if (ThemeClass.SaveThemeColors(this, themeName, PluginUserDataPath))
            {
                _ = API.Instance.Dialogs.ShowMessage(ResourceProvider.GetString("LOCThemeModifierManageSaveOk"), "ThemeModifier");
                SetMenuItems();
            }
            else
            {
                _ = API.Instance.Dialogs.ShowMessage(ResourceProvider.GetString("LOCThemeModifierManageSaveKo"), "ThemeModifier");
            }

            PART_EditThemeMenuSaveName.Text = string.Empty;
            PART_EditThemeMenuBtSave.IsEnabled = false;
        }

        private void PART_EditThemeMenuSaveName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string themeName = PART_EditThemeMenuSaveName.Text;
            PART_EditThemeMenuBtSave.IsEnabled = themeName.Trim().Length > 3 && !ThemeClass.ThemeFileExist(themeName, PluginUserDataPath);
        }

        private void PART_EditThemeMenuBtLoad_Click(object sender, RoutedEventArgs e)
        {
            string pathFileName = ((FrameworkElement)sender).Tag.ToString();

            try
            {
                ThemeClass.LoadThemeColors(pathFileName, Settings, this);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, $"Error on load {pathFileName}", true, "ThemeModifier");
            }
        }

        private void PART_EditThemeMenuBtRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string pathFileName = ((FrameworkElement)sender).Tag.ToString();
                if (API.Instance.Dialogs.ShowMessage(ResourceProvider.GetString("LOCRemoveLabel") + " " + pathFileName, "ThemeModifier", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ThemeClass.DeleteThemeColors(pathFileName);
                    SetMenuItems();
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false, $"Error on remove {((FrameworkElement)sender).Tag.ToString()}", true, "ThemeModifier");
            }
        }

        private void PART_EditThemeMenuImport_Click(object sender, RoutedEventArgs e)
        {
            string targetPath = API.Instance.Dialogs.SelectFile("json file|*.json");

            if (!targetPath.IsNullOrEmpty())
            {
                ThemeColors themeColors = new ThemeColors();

                try
                {
                    themeColors = Serialization.FromJsonFile<ThemeColors>(targetPath);
                }
                catch
                {
                    _ = API.Instance.Dialogs.ShowErrorMessage(ResourceProvider.GetString("LOCThemeModifierManageNoFile"), "ThemeModifier");
                    return;
                }

                if (themeColors.Name.IsNullOrEmpty())
                {
                    _ = API.Instance.Dialogs.ShowErrorMessage(ResourceProvider.GetString("LOCThemeModifierManageNoFile"), "ThemeModifier");
                    return;
                }

                string pathThemeColors = Path.Combine(PluginUserDataPath, "ThemeColors");
                string pathThemeColorsFile = Path.Combine(pathThemeColors, themeColors.Name + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".json");
                File.Copy(targetPath, pathThemeColorsFile, true);

                SetMenuItems();
            }
        }

        private void PART_EditThemeMenuBtExport_Click(object sender, RoutedEventArgs e)
        {
            string pathFileName = ((FrameworkElement)sender).Tag.ToString();
            string targetPath = API.Instance.Dialogs.SaveFile("json file|*.json", true);

            if (!targetPath.IsNullOrEmpty())
            {
                File.Copy(pathFileName, targetPath, true);
            }
        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (PART_EditThemeMenu != null)
                {
                    PART_EditThemeMenu.IsEnabled = ((TabControl)sender).SelectedIndex == 1 || ((TabControl)sender).SelectedIndex == 3;
                }
            }
            catch
            {
            }
        }
        #endregion
    }
}
