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
        internal override IDataContext _ControlDataContext
        {
            get => ControlDataContext;
            set => ControlDataContext = (PluginIconDataContext)_ControlDataContext;
        }

        private ThemeModifierSettingsViewModel PluginSettings;

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


        public PluginIcon(ThemeModifierSettingsViewModel PluginSettings)
        {
            this.PluginSettings = PluginSettings;
        
            InitializeComponent();
            this.DataContext = ControlDataContext;
        
            this.PluginSettings.PropertyChanged += PluginSettings_PropertyChanged;
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
            this.Icon = !GameContext.Icon.IsNullOrEmpty() ? API.Instance.Database.GetFullFilePath(GameContext.Icon) : string.Empty;
        }


        private async void LoadNewIcon(object newSource, object oldSource)
        {
            if (newSource?.Equals(CurrentIcon) == true)
            {
                return;
            }

            CurrentIcon = newSource;
            BitmapImage image = null;

            PART_Image.Source = (BitmapImage)resources.GetResource("DefaultGameIcon");

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

                    return (BitmapImage)resources.GetResource("DefaultGameIcon");
                });
            }

            PART_Image.Source = image;
        }
    }


    public class PluginIconDataContext : ObservableObject, IDataContext
    {
        private bool _IsActivated;
        public bool IsActivated { get => _IsActivated; set => SetValue(ref _IsActivated, value);  }

        private BitmapImage _ImageFrame;
        public BitmapImage ImageFrame { get => _ImageFrame; set => SetValue(ref _ImageFrame, value); }

        private BitmapImage _ImageShape;
        public BitmapImage ImageShape { get => _ImageShape; set => SetValue(ref _ImageShape, value); }
    }
}
