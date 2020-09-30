using System;
using System.Windows;

namespace ThemeModifier.Models
{
    public class ThemeLinearGradient
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public ThemeGradientColor GradientStop1 { get; set; }
        public ThemeGradientColor GradientStop2 { get; set; }
    }

    public class ThemeGradientColor
    {
        public string ColorString { get; set; }
        public Double ColorOffset { get; set; }
    }
}
