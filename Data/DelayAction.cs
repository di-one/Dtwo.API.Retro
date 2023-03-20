using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.Models
{
    public class DelayAction
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public static DelayAction CreateDefault()
        {
            return new DelayAction()
            {
                Min = 500,
                Max = 1000
            };
        }
    }
}
