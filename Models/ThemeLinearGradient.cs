using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Media;

namespace ThemeModifier.Models
{
    public class ThemeLinearGradient
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public ThemeGradientColor GradientStop1 { get; set; }
        public ThemeGradientColor GradientStop2 { get; set; }

        [JsonIgnore]
        public LinearGradientBrush ToLinearGradientBrush
        {
            get
            {
                LinearGradientBrush linearGradientBrush = new LinearGradientBrush();

                linearGradientBrush.StartPoint = StartPoint;
                linearGradientBrush.EndPoint = EndPoint;

                GradientStop gs1 = new GradientStop();
                GradientStop gs2 = new GradientStop();

                gs1.Offset = GradientStop1.ColorOffset;
                gs2.Offset = GradientStop2.ColorOffset;

                gs1.Color = (Color)ColorConverter.ConvertFromString(GradientStop1.ColorString);
                gs2.Color = (Color)ColorConverter.ConvertFromString(GradientStop2.ColorString);

                linearGradientBrush.GradientStops.Add(gs1);
                linearGradientBrush.GradientStops.Add(gs2);

                return linearGradientBrush;
            }
        }
    }

    public class ThemeGradientColor
    {
        public string ColorString { get; set; }
        public Double ColorOffset { get; set; }
    }
}
