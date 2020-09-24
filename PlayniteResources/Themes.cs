using Playnite.SDK;
using Playnite.SDK.Models;
using PluginCommon;
using PluginCommon.PlayniteResources;
using PluginCommon.PlayniteResources.API;
using PluginCommon.PlayniteResources.Common;
using PluginCommon.PlayniteResources.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using System.Linq;
using System.Windows;
using PlaynitePaths = PluginCommon.PlayniteResources.PlaynitePaths;
using PlayniteSettings = PluginCommon.PlayniteResources.PlayniteSettings;

namespace ThemeModifier.PlayniteResources
{
    public class BaseExtensionManifest
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Version { get; set; }

        public List<Link> Links { get; set; }

        [YamlIgnore]
        public string DirectoryPath { get; set; }

        [YamlIgnore]
        public string DirectoryName { get; set; }

        [YamlIgnore]
        public string DescriptionPath { get; set; }

        [YamlIgnore]
        public string LegacyDirId
        {
            get
            {
                if (Name.IsNullOrEmpty())
                {
                    return string.Empty;
                }
                else
                {
                    return Paths.GetSafeFilename(Name).Replace(" ", string.Empty) + "_" + (Name + Author).MD5();
                }
            }
        }

        public void VerifyManifest()
        {
            if (!System.Version.TryParse(Version, out var extver))
            {
                throw new Exception("Extension version string must be a real version!");
            }
        }
    }

    public class ThemeManifest : BaseExtensionManifest
    {
        public string ThemeApiVersion { get; set; }

        public ApplicationMode Mode { get; set; }

        [YamlIgnore]
        public bool IsBuiltInTheme { get; }

        [YamlIgnore]
        public bool IsCustomTheme => !IsBuiltInTheme;

        [YamlIgnore]
        public bool IsCompatible { get; } = false;

        public ThemeManifest()
        {
        }

        public ThemeManifest(string manifestPath)
        {
            var thm = Serialization.FromYaml<ThemeManifest>(File.ReadAllText(manifestPath));
            thm.CopyProperties(this, false);
            DescriptionPath = manifestPath;
            DirectoryPath = Path.GetDirectoryName(manifestPath);
            DirectoryName = Path.GetFileNameWithoutExtension(DirectoryPath);
            // TODO New SDK
            if (Mode == ApplicationMode.Desktop)
            {
                //IsBuiltInTheme = BuiltinExtensions.BuiltinDesktopThemeFolders.Contains(DirectoryName);
            }
            else
            {
                //IsBuiltInTheme = BuiltinExtensions.BuiltinFullscreenThemeFolders.Contains(DirectoryName);
            }

            var apiVesion = Mode == ApplicationMode.Desktop ? ThemeManager.DesktopApiVersion : ThemeManager.FullscreenApiVersion;
            if (!ThemeApiVersion.IsNullOrEmpty())
            {
                var themeVersion = new Version(ThemeApiVersion);
                if (themeVersion.Major == apiVesion.Major && themeVersion <= apiVesion)
                {
                    IsCompatible = true;
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ThemeManager
    {
        private static ILogger logger = LogManager.GetLogger();
        public static System.Version DesktopApiVersion => new System.Version("1.6.0");
        public static System.Version FullscreenApiVersion => new System.Version("1.6.0");
        public static ThemeManifest CurrentTheme { get; private set; }
        public static ThemeManifest DefaultTheme { get; private set; }

        public static System.Version GetApiVersion(ApplicationMode mode)
        {
            return mode == ApplicationMode.Desktop ? DesktopApiVersion : FullscreenApiVersion;
        }

        public static string GetThemeRootDir(ApplicationMode mode)
        {
            return mode == ApplicationMode.Desktop ? "Desktop" : "Fullscreen";
        }

        public static void SetCurrentTheme(ThemeManifest theme)
        {
            CurrentTheme = theme;
        }

        public static void SetDefaultTheme(ThemeManifest theme)
        {
            DefaultTheme = theme;
        }

        /*
        public static void ApplyFullscreenButtonPrompts(Application app, FullscreenButtonPrompts prompts)
        {
            if (prompts == FullscreenSettings.DefaultButtonPrompts)
            {
                var defaultXaml = $"{FullscreenSettings.DefaultButtonPrompts.ToString()}.xaml";
                foreach (var dir in PlayniteApplication.CurrentNative.Resources.MergedDictionaries.ToList())
                {
                    if (dir.Source == null)
                    {
                        continue;
                    }

                    if (dir.Source.OriginalString.Contains("ButtonPrompts") &&
                        !dir.Source.OriginalString.EndsWith(defaultXaml))
                    {
                        PlayniteApplication.CurrentNative.Resources.MergedDictionaries.Remove(dir);
                    }
                }
            }
            else
            {
                var promptsPath = Path.Combine(ThemeManager.DefaultTheme.DirectoryPath, "Images", "ButtonPrompts");
                foreach (var dir in Directory.GetDirectories(promptsPath))
                {
                    var dirInfo = new DirectoryInfo(dir);
                    var promptXaml = Path.Combine(dir, $"{dirInfo.Name}.xaml");
                    if (File.Exists(promptXaml) && dirInfo.Name == prompts.ToString())
                    {
                        var xaml = Xaml.FromFile(promptXaml);
                        if (xaml is ResourceDictionary xamlDir)
                        {
                            xamlDir.Source = new Uri(promptXaml, UriKind.Absolute);
                            PlayniteApplication.CurrentNative.Resources.MergedDictionaries.Add(xamlDir);
                        }
                    }
                }
            }
        }
        */

        /*
        public static bool ApplyTheme(Application app, ThemeManifest theme, ApplicationMode mode)
        {
            var apiVesion = mode == ApplicationMode.Desktop ? DesktopApiVersion : FullscreenApiVersion;
            if (!theme.ThemeApiVersion.IsNullOrEmpty())
            {
                var themeVersion = new Version(theme.ThemeApiVersion);
                if (themeVersion.Major != apiVesion.Major || themeVersion > apiVesion)
                {
                    logger.Error($"Failed to apply {theme.Name} theme, unsupported API version {theme.ThemeApiVersion}.");
                    return false;
                }
            }

            var allLoaded = true;
            var loadedXamls = new List<ResourceDictionary>();
            var acceptableXamls = new List<string>();
            var defaultRoot = $"Themes/{mode.GetDescription()}/{DefaultTheme.DirectoryName}/";
            foreach (var dict in app.Resources.MergedDictionaries)
            {
                if (dict.Source.OriginalString.StartsWith("Themes") && dict.Source.OriginalString.EndsWith("xaml"))
                {
                    acceptableXamls.Add(dict.Source.OriginalString.Replace(defaultRoot, "").Replace('/', '\\'));
                }
            }

            foreach (var accXaml in acceptableXamls)
            {
                var xamlPath = Path.Combine(theme.DirectoryPath, accXaml);
                if (!File.Exists(xamlPath))
                {
                    continue;
                }

                try
                {
                    var xaml = Xaml.FromFile(xamlPath);
                    if (xaml is ResourceDictionary xamlDir)
                    {
                        xamlDir.Source = new Uri(xamlPath, UriKind.Absolute);
                        loadedXamls.Add(xamlDir as ResourceDictionary);
                    }
                    else
                    {
                        logger.Error($"Skipping theme file {xamlPath}, it's not resource dictionary.");
                    }
                }
                catch (Exception e) when (!PlayniteEnvironment.ThrowAllErrors)
                {
                    logger.Error(e, $"Failed to load xaml {xamlPath}");
                    allLoaded = false;
                    break;
                }
            }

            if (allLoaded)
            {
                loadedXamls.ForEach(a => app.Resources.MergedDictionaries.Add(a));
                return true;
            }

            return false;
        }
        */

        public static List<ThemeManifest> GetAvailableThemes(ApplicationMode mode)
        {
            var modeDir = GetThemeRootDir(mode);
            var added = new List<string>();
            var themes = new List<ThemeManifest>();

            var userPath = Path.Combine(PlaynitePaths.ThemesUserDataPath, modeDir);
            if (!PlayniteSettings.IsPortable && Directory.Exists(userPath))
            {
                foreach (var dir in Directory.GetDirectories(userPath))
                {
                    try
                    {
                        var descriptorPath = Path.Combine(dir, PlaynitePaths.ThemeManifestFileName);
                        if (File.Exists(descriptorPath))
                        {
                            var info = new FileInfo(descriptorPath);
                            added.Add(info.Directory.Name);
                            themes.Add(new ThemeManifest(descriptorPath));
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, $"Failed to load theme info {dir}");
                    }
                }
            }

            var programPath = Path.Combine(PlaynitePaths.ThemesProgramPath, modeDir);
            if (Directory.Exists(programPath))
            {
                foreach (var dir in Directory.GetDirectories(programPath))
                {
                    try
                    {
                        var descriptorPath = Path.Combine(dir, PlaynitePaths.ThemeManifestFileName);
                        if (File.Exists(descriptorPath))
                        {
                            var info = new FileInfo(descriptorPath);
                            if (!added.Contains(info.Directory.Name))
                            {
                                themes.Add(new ThemeManifest(descriptorPath));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, $"Failed to load theme info {dir}");
                    }
                }
            }

            return themes;
        }
    }
}
