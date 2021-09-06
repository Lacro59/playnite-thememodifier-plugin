using CommonPluginsShared.Collections;
using CommonPluginsShared.Controls;
using CommonPluginsShared.Interfaces;
using Playnite.SDK;
using Playnite.SDK.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace ThemeModifier.Controls
{
    /// <summary>
    /// Logique d'interaction pour PluginIcon.xaml
    /// </summary>
    public partial class PluginIcon : PluginUserControlExtendBase
    {
        private PluginIconDataContext ControlDataContext;
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


        public PluginIcon(IPlayniteAPI PlayniteApi, ThemeModifierSettingsViewModel PluginSettings)
        {
            this.PlayniteApi = PlayniteApi;
            this.PluginSettings = PluginSettings;
            this.PluginFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            InitializeComponent();

            Task.Run(() =>
            {
                this.Dispatcher.BeginInvoke((Action)delegate
                {
                    this.PluginSettings.PropertyChanged += PluginSettings_PropertyChanged;
                    this.PlayniteApi.Database.Games.ItemUpdated += Games_ItemUpdated;

                    // Apply settings
                    PluginSettings_PropertyChanged(null, null);
                });
            });
        }


        public override void SetDefaultDataContext()
        {
            ControlDataContext = new PluginIconDataContext
            {
                IsActivated = PluginSettings.Settings.EnableIntegrationIcon,

                ImageBitmap = null,
                ImageFrame = string.Empty,
                ImageShape = string.Empty
            };
        }
         

        public override Task<bool> SetData(Game newContext)
        {
            string Icon = GameContext.Icon;
            BitmapImage GameIcon = null;

            if (!Icon.IsNullOrEmpty())
            {
                string IconPath = PlayniteApi.Database.GetFullFilePath(Icon);

                if (File.Exists(IconPath))
                {
                    GameIcon = BitmapExtensions.BitmapFromFile(IconPath);
                }
            }

            if (GameIcon == null)
            {
                GameIcon = (BitmapImage)resources.GetResource("DefaultGameIcon");
            }

            return Task.Run(() =>
            {
                string ImageName = string.Empty;
                if (PluginSettings.Settings.UseIconCircle)
                {
                    ImageName = "circle";
                }
                if (PluginSettings.Settings.UseIconClock)
                {
                    ImageName = "clock";
                }
                if (PluginSettings.Settings.UseIconSquareCorne)
                {
                    ImageName = "squareCorne";
                }
                if (PluginSettings.Settings.UseIconWe4ponx)
                {
                    ImageName = "we4ponx";
                }

                string ImageFramePath = Path.Combine(PluginFolder, "Resources", "Images", ImageName + ".png");
                string ImageShapePath = Path.Combine(PluginFolder, "Resources", "Images", ImageName + "Shape.png");

                if (File.Exists(ImageFramePath) && File.Exists(ImageShapePath))
                {
                    ControlDataContext.ImageFrame = ImageFramePath;
                    ControlDataContext.ImageShape = ImageShapePath;
                }

                ControlDataContext.ImageBitmap = GameIcon;

                this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new ThreadStart(delegate
                {
                    this.DataContext = ControlDataContext;
                }));

                return true;
            });
        }
    }


    public class PluginIconDataContext : IDataContext
    {
        public bool IsActivated { get; set; }
        
        public BitmapImage ImageBitmap { get; set; }
        public string ImageFrame { get; set; }
        public string ImageShape { get; set; }
    }
}
