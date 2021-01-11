using Playnite.SDK;
using CommonPluginsShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ThemeModifier.Views.Interfaces
{
    /// <summary>
    /// Logique d'interaction pour FeaturesList.xaml
    /// </summary>
    public partial class FeaturesList : StackPanel
    {
        private static readonly ILogger logger = LogManager.GetLogger();

        public FeaturesList()
        {
            InitializeComponent();

            ThemeModifier.icoFeatures.PropertyChanged += OnPropertyChanged;
        }


        protected void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == "CurrentFeaturesList" || e.PropertyName == "PluginSettings")
                {
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new ThreadStart(delegate
                    {
                        if (ThemeModifier.icoFeatures.CurrentFeaturesList.Count == 0)
                        {
                            this.Visibility = Visibility.Collapsed;
                            return;
                        }
                        
                        PART_FeaturesList.ItemsSource = null;
                        PART_FeaturesList.ItemsSource = ThemeModifier.icoFeatures.CurrentFeaturesList;

                        this.Width = 40 * ThemeModifier.icoFeatures.CurrentFeaturesList.Count;

                        this.DataContext = new
                        {
                            CountItems = ThemeModifier.icoFeatures.CurrentFeaturesList.Count
                        };
                    }));
                }
                else
                {
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new ThreadStart(delegate
                    {
                        this.Visibility = Visibility.Collapsed;
                    }));
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex, "ThemModifier");
            }
        }
    }
}
