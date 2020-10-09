namespace ThemeModifier.Models
{
    public class ThemeElement
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public dynamic Element { get; set; }
    }
}
