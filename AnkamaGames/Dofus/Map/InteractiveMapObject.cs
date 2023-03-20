using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.DofusBehavior.Map
{
    public class InteractiveMapObject
    {
        public short Gfx { get; private set; }
        public Cell Cell { get; private set; }
        public Models.InteractiveMapObject Model { get; private set; }
        public bool IsUsable { get; set; } = false;

        public InteractiveMapObject(short gfx, Cell cell)
        {
            //Gfx = gfx;
            //Cell = cell;

            //Models.InteractiveMapObject model = Database.InteractiveMapObjects.FindByGfx(gfx);

            //if (model != null)
            //{
            //    Model = model;
            //    IsUsable = true;
            //}
        }
    }
}
