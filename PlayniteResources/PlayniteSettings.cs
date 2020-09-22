using System.IO;

namespace ThemeModifier.PlayniteResources
{
    public class PlayniteSettings
    {
        public static bool IsPortable
        {
            get
            {
                return !File.Exists(PlaynitePaths.UninstallerPath);
            }
        }
    }
}
