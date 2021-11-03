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
            get
            {
                return ControlDataContext;
            }
            set
            {
                ControlDataContext = (PluginIconDataContext)_ControlDataContext;
            }
        }

        private IPlayniteAPI PlayniteApi;
        private ThemeModifierSettingsViewModel PluginSettings;
        private string PluginFolder;

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
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        private static void IconChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                var control = (PluginIcon)obj;
                control.LoadNewIcon(args.NewValue, args.OldValue);
            }
            catch (Exception ex)
            {
                Common.LogError(ex, false);
            }
        }
        #endregion


        public PluginIcon(IPlayniteAPI PlayniteApi, ThemeModifierSettingsViewModel PluginSettings)
        {
            this.PlayniteApi = PlayniteApi;
            this.PluginSettings = PluginSettings;
            this.PluginFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            InitializeComponent();

            this.DataContext = ControlDataContext;

            this.PluginSettings.PropertyChanged += PluginSettings_PropertyChanged;
            this.PlayniteApi.Database.Games.ItemUpdated += Games_ItemUpdated;

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
            this.Icon = PlayniteApi.Database.GetFullFilePath(GameContext.Icon);
        }


        private async void LoadNewIcon(object newSource, object oldSource)
        {
            if (newSource?.Equals(CurrentIcon) == true)
            {
                return;
            }

            CurrentIcon = newSource;
            BitmapImage image = null;

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
        private bool _IsActivated { get; set; }
        public bool IsActivated
        {
            get => _IsActivated;
            set
            {
                _IsActivated = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _ImageFrame { get; set; }
        public BitmapImage ImageFrame
        {
            get => _ImageFrame;
            set
            {
                if (value?.UriSource?.Equals(_ImageFrame?.UriSource) == true)
                {
                    return;
                }

                _ImageFrame = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _ImageShape { get; set; }
        public BitmapImage ImageShape
        {
            get => _ImageShape;
            set
            {
                if (value?.UriSource?.Equals(_ImageShape?.UriSource) == true)
                {
                    return;
                }

                _ImageShape = value;
                OnPropertyChanged();
            }
        }
    }
}
