using CommonPlayniteShared;
using CommonPluginsShared;
using CommonPluginsShared.Controls;
using CommonPluginsShared.Interfaces;
using Playnite.SDK;
using Playnite.SDK.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ThemeModifier.Controls
{
    /// <summary>
    /// Logique d'interaction pour PluginIcon.xaml
    /// </summary>
    public partial class PluginIcon : PluginUserControlExtendBase
    {
        private PluginIconDataContext ControlDataContext = new PluginIconDataContext();
        internal override IDataContext controlDataContext
        {
            get => ControlDataContext;
            set => ControlDataContext = (PluginIconDataContext)controlDataContext;
        }

        private ThemeModifierSettingsViewModel PluginSettings { get; set; }

        private object CurrentIcon { get; set; }


        #region Properties
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            nameof(Icon),
            typeof(string),
            typeof(PluginIcon),
            new FrameworkPropertyMetadata(string.Empty, IconChanged)
        );
        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        private static void IconChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                PluginIcon control = (PluginIcon)obj;
                control.LoadNewIcon(args.NewValue, args.OldValue);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false);
            }
        }
        #endregion


        public PluginIcon(ThemeModifierSettingsViewModel pluginSettings)
        {
            PluginSettings = pluginSettings;

            InitializeComponent();
            DataContext = ControlDataContext;

            PluginSettings.PropertyChanged += PluginSettings_PropertyChanged;
            API.Instance.Database.Games.ItemUpdated += Games_ItemUpdated;

            // Apply settings
            PluginSettings_PropertyChanged(null, null);
        }


        public override void SetDefaultDataContext()
        {
            ControlDataContext.IsActivated = PluginSettings.Settings.EnableIntegrationIcon;

            ControlDataContext.ImageFrame = PluginSettings.Settings.BitmapFrame;
            ControlDataContext.ImageShape = PluginSettings.Settings.BitmapShape;
        }


        public override void SetData(Game newContext)
        {
            Icon = !GameContext.Icon.IsNullOrEmpty() ? API.Instance.Database.GetFullFilePath(GameContext.Icon) : string.Empty;
        }


        private async void LoadNewIcon(object newSource, object oldSource)
        {
            if (newSource?.Equals(CurrentIcon) == true)
            {
                return;
            }

            CurrentIcon = newSource;
            BitmapImage image = null;

            PART_Image.Source = (BitmapImage)ResourceProvider.GetResource("DefaultGameIcon");

            if (newSource != null)
            {
                image = await Task.Factory.StartNew(() =>
                {
                    if (newSource is string str)
                    {
                        BitmapImage tmpImage = ImageSourceManager.GetImage(str, false);

                        if (tmpImage != null)
                        {
                            return tmpImage;
                        }
                    }

                    return (BitmapImage)ResourceProvider.GetResource("DefaultGameIcon");
                });
            }

            PART_Image.Source = image;
        }
    }


    public class PluginIconDataContext : ObservableObject, IDataContext
    {
        private bool _isActivated;
        public bool IsActivated { get => _isActivated; set => SetValue(ref _isActivated, value); }

        private BitmapImage _imageFrame;
        public BitmapImage ImageFrame { get => _imageFrame; set => SetValue(ref _imageFrame, value); }

        private BitmapImage _imageShape;
        public BitmapImage ImageShape { get => _imageShape; set => SetValue(ref _imageShape, value); }
    }
}
