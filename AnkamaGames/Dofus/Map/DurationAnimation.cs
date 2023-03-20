using System;
using System.Collections.Generic;
using System.Text;

namespace D_One.Core.DofusBehavior.Map.PathFinding
{
    public class DurationAnimation
    {
        public int Linear { get; private set; }
        public int Horizontal { get; private set; }
        public int Vertical { get; private set; }

        public DurationAnimation(int linear, int horizontal, int vertical)
        {
            Linear = linear;
            Horizontal = horizontal;
            Vertical = vertical;
        }
    }
}
