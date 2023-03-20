using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.Models
{
    public class Cell
    {
        public enum CellType
        {
            NO_WALKABLE = 0,
            INTERACTIVE_OBJECT = 1,
            TELEPORT = 2,
            UNKNOWN1 = 3,
            WALKABLE = 4,
            UNKNOWN2 = 5,
            PATH_1 = 6,
            PATH_2 = 7
        }

        public short Id { get;  set; } = 0;
        public DofusBehavior.Map.InteractiveMapObject InteractiveMapObject { get;  set; }
        public int X { get;  set; } = 0;
        public int Y { get;  set; } = 0;
        public bool IsActive { get;  set; } = false;
        public CellType Type { get;  set; } = CellType.NO_WALKABLE;
        public bool BlockVisionLine { get;  set; } = false;

        public byte LayerGroundLevel { get; set; }
        public byte LayerGroundSlope { get; set; }
        public short LayerObject1 { get; set; }
        public short LayerObject2 { get; set; }
    }
}
