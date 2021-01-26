namespace ThemeModifier.Models
{
    public class ThemeElement
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public dynamic Element { get; set; }
        public double Opacity { get; set; } = 1;
        public ThemeSliderLimit themeSliderLimit { get; set; } = new ThemeSliderLimit();
    }

    public class ThemeSliderLimit
    {
        public double Min { get; set; } = 0;
        public double Max { get; set; } = 30;
    }
}
