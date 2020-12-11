using Playnite.SDK;
using Playnite.SDK.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeModifier.Services
{
    public class IcoFeatures : ObservableObject
    {
        private static ILogger logger = LogManager.GetLogger();
        private static IResourceProvider resources = new ResourceProvider();

        private string _pluginFolder;

        private List<FeaturesItem> featuresItems = new List<FeaturesItem>() {
            new FeaturesItem { Name = "Achievements", Icon = "ico_achievements.png" },
            new FeaturesItem { Name = "Battle Royale", Icon = "" },
            new FeaturesItem { Name = "Captions Available", Icon = "ico_cc.png" },
            new FeaturesItem { Name = "Cloud Saves", Icon = "ico_cloud.png" },
            new FeaturesItem { Name = "Commentary Available", Icon = "ico_commentary.png" },
            new FeaturesItem { Name = "Controller support", Icon = "ico_controller.png" },
            new FeaturesItem { Name = "Co-Op", Icon = "ico_coop.png" },
            new FeaturesItem { Name = "Co-Operative", Icon = "ico_coop.png" },
            new FeaturesItem { Name = "Cross-Platform Multiplayer", Icon = "ico_multiPlayer.png" },
            new FeaturesItem { Name = "Full Controller Support", Icon = "ico_controller.png" },
            new FeaturesItem { Name = "Game Demo", Icon = "" },
            new FeaturesItem { Name = "In-App Purchases", Icon = "ico_cart.png" },
            new FeaturesItem { Name = "Includes Level Editor", Icon = "ico_editor.png" },
            new FeaturesItem { Name = "Includes Source SDK", Icon = "ico_sdk.png" },
            new FeaturesItem { Name = "LAN Co-Op", Icon = "ico_coop.png" },
            new FeaturesItem { Name = "LAN Pvp", Icon = "ico_multiPlayer.png" },
            new FeaturesItem { Name = "Leaderboards", Icon = "ico_leaderboards.png" },
            new FeaturesItem { Name = "Massively Multiplayer Online (MMO)", Icon = "ico_multiPlayer.png" },
            new FeaturesItem { Name = "MMO", Icon = "ico_multiPlayer.png" },
            new FeaturesItem { Name = "Mods", Icon = "ico_sdk.png" },
            new FeaturesItem { Name = "Multiplayer", Icon = "ico_multiPlayer.png" },
            new FeaturesItem { Name = "Online Co-Op", Icon = "ico_coop.png" },
            new FeaturesItem { Name = "Online Pvp", Icon = "ico_multiPlayer.png" },
            new FeaturesItem { Name = "Partial Controller Support", Icon = "ico_partial_controller.png" },
            new FeaturesItem { Name = "Pvp", Icon = "ico_multiPlayer.png" },
            new FeaturesItem { Name = "Remote Play On Phone", Icon = "ico_remote_play" },
            new FeaturesItem { Name = "Remote Play On Tablet", Icon = "ico_remote_play" },
            new FeaturesItem { Name = "Remote Play On TV", Icon = "ico_remote_play" },
            new FeaturesItem { Name = "Remote Play Together", Icon = "ico_remote_play_together.png" },
            new FeaturesItem { Name = "Shared/Split Screen", Icon = "ico_multiPlayer.png" },
            new FeaturesItem { Name = "Shared/Split Screen Co-Op", Icon = "ico_multiPlayer.png" },
            new FeaturesItem { Name = "Shared/Split Screen Pvp", Icon = "ico_multiPlayer.png" },
            new FeaturesItem { Name = "Single Player", Icon = "ico_singlePlayer.png" },
            new FeaturesItem { Name = "Split Screen", Icon = "ico_multiPlayer.png" },
            new FeaturesItem { Name = "Stats", Icon = "ico_stats.png" },
            new FeaturesItem { Name = "Trading Cards", Icon = "ico_cards.png" },
            new FeaturesItem { Name = "Valve Anti-Cheat Enabled", Icon = "ico_vac.png" },
            new FeaturesItem { Name = "VR", Icon = "" },
            new FeaturesItem { Name = "VR Gamepad", Icon = "ico_vr_input_motion.png" },
            new FeaturesItem { Name = "VR Keyboard / Mouse", Icon = "ico_vr_input_kbm.png" },
            new FeaturesItem { Name = "VR Motion Controllers", Icon = "ico_vr_input_motion.png" },
            new FeaturesItem { Name = "VR Room-Scale", Icon = "ico_vr_area_roomscale.png" },
            new FeaturesItem { Name = "VR Seated", Icon = "ico_vr_area_seated.png" },
            new FeaturesItem { Name = "VR Standing", Icon = "ico_vr_area_standing.png" },
            new FeaturesItem { Name = "Workshop", Icon = "ico_workshop.png" }
        };

        private List<FeaturesItem> _CurrentFeaturesList;
        public List<FeaturesItem> CurrentFeaturesList
        {
            get
            {
                return _CurrentFeaturesList;
            }

            set
            {
                _CurrentFeaturesList = value;
                OnPropertyChanged();
            }
        }


        public IcoFeatures(string pluginFolder)
        {
            _pluginFolder = pluginFolder;
        }


        public void SetCurrentFeaturesList(Game game)
        {
            List<FeaturesItem> TempCurrentFeaturesList = new List<FeaturesItem>();

            if (game.Features != null)
            {
                foreach(GameFeature gameFeature in game.Features)
                {
                    FeaturesItem TempFeaturesItem = GetFeature(gameFeature.Name);
                    if (TempFeaturesItem != null)
                    {
                        string IconPath = Path.Combine(_pluginFolder, "Resources", "white", TempFeaturesItem.Icon);
                        if (File.Exists(IconPath)) {
                            TempFeaturesItem.Icon = IconPath;
                            TempCurrentFeaturesList.Add(TempFeaturesItem);
                        }
                    }
                }
            }

            TempCurrentFeaturesList = TempCurrentFeaturesList.GroupBy(x => x.Icon).Select(x => x.First()).ToList();
            CurrentFeaturesList = TempCurrentFeaturesList;
        }

        private FeaturesItem GetFeature(string FeatureName)
        {
            return featuresItems.Find(x => x.Name.ToLower() == FeatureName.ToLower() && !x.Icon.IsNullOrEmpty());
        }
    }


    public class FeaturesItem
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool IsCustom { get; set; }
    }
}
