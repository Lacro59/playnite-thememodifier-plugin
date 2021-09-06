﻿using System.Collections.Generic;

namespace ThemeModifier.Models
{
    public class ThemeConstants
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<ElementConstants> Constants { get; set; }
    }

    public class ElementConstants
    {
        public string Name { get; set; }
        public string TypeResource { get; set; }
        public dynamic Element { get; set; }
        public double Opacity { get; set; } = 1;
    }

    public class ThemeConstantsDefined
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
