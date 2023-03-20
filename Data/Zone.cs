using D_One.Core.DofusBehavior.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.Models
{
    public class Zone
    {
        public Dtwo.API.Retro.Data.Map Center { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Zone(string mapCenter, int width, int height)
        {
            Center = MapManager.GetMap(mapCenter);
            Width = width;
            Height = height;
        }
    }
}
