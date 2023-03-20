using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.Models
{
    public class CharacterConfig
    {
        public string AccountName { get; set; }
        public string CharacterName { get; set; }
        public int SpellAutoBoostId { get; set; } = 0;
        public int CaracAutoBoostId { get; set; } = 0;
        public bool Awake { get; set; } = false;
        public int RaceType { get; set; }
        public int SexType { get; set; }
    }
}
