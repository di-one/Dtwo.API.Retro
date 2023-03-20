using D_One.Core.DofusBehavior.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.Models
{
    public class RaceStatsCalcul
    {
        //[JsonConverter(typeof(StringEnumConverter))]
        //public RaceType Race { get; set; }
        public List<RaceStatsCalcul_Type> Types { get; set; }
    }

    public class RaceStatsCalcul_Type
    {
        //[JsonConverter(typeof(StringEnumConverter))]
        //public DofusBehavior.Enums.StatsType Type { get; set; }
        public List<RaceStatsCalcul_Level> Levels { get; set; }
    }

    public class RaceStatsCalcul_Level
    {
        public string Range { get; set; } // "min..max" / "min.." (min to infiniy)
        public int Bonus { get; set; }
        public int Cost { get; set; }
    }
}
