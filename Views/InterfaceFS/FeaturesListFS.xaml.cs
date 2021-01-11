using Playnite.SDK.Models;
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

namespace ThemeModifier.Views.InterfaceFS
{
    /// <summary>
    /// Logique d'interaction pour FeaturesListFS.xaml
    /// </summary>
    public partial class FeaturesListFS : StackPanel
    {
        public FeaturesListFS()
        {
            InitializeComponent();
        }


        public void SetData(Game game)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new ThreadStart(delegate
            {
                ThemeModifier.icoFeatures.SetCurrentFeaturesList(game);
                if (ThemeModifier.icoFeatures.CurrentFeaturesList.Count == 0)
                {
                    this.Visibility = Visibility.Collapsed;
                    return;
                }
                
                PART_FeaturesList.ItemsSource = ThemeModifier.icoFeatures.CurrentFeaturesList;

                this.DataContext = new
                {
                    CountItems = ThemeModifier.icoFeatures.CurrentFeaturesList.Count
                };
            }));
        }
    }
}
