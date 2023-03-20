using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.Models
{
    public class InteractiveMapObject
    {
        public short[] Gfxs { get; private set; }
        public bool Walkable { get; private set; }
        public short[] Skills { get; private set; }
        public string Name { get; private set; }
        public bool Recoltable { get; private set; }
        
        public InteractiveMapObject(Models.InteractiveMapObjectData data)
        {
            Name = data.Name;

            if (!data.Gfx.Equals("-1") && !string.IsNullOrEmpty(data.Gfx))
            {
                string[] separator = data.Gfx.Split(',');
                Gfxs = new short[separator.Length];

                for (byte i = 0; i < Gfxs.Length; i++)
                    Gfxs[i] = short.Parse(separator[i]);
            }

            Walkable = data.Walkable;

            if (!string.IsNullOrEmpty(data.Skills) && !data.Skills.Equals("-1"))
            {
                string[] separator = data.Skills.Split(',');
                Skills = new short[separator.Length];

                for (byte i = 0; i < Skills.Length; ++i)
                    Skills[i] = short.Parse(separator[i]);
            }

            Recoltable = data.Recoltable;
        }
    }
}
