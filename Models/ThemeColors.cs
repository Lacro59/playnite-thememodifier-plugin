using Playnite.SDK.Data;
using System.Collections.Generic;

namespace ThemeModifier.Models
{
    public class ThemeColors
    {
        [DontSerialize]
        public string FileName { get; set; }
        public string Name { get; set; }
        public List<ThemeColorsElement> ThemeColorsElements { get; set; } = new List<ThemeColorsElement>();
    }

    public class ThemeColorsElement
    {
        public string Name { get; set; }
        public string ColorString { get; set; } = string.Empty;
        public ThemeLinearGradient ColorLinear { get; set; } = new ThemeLinearGradient();
    }
}
