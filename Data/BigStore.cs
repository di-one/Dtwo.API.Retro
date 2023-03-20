using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.Models
{
    public class BigStore
    {
        public int Quantity1 { get;  set; }
        public int Quantity2 { get;  set; }
        public int Quantity3 { get;  set; }
        public string[] Types { get;  set; }
        public int Tax { get;  set; }
        public int MaxLevel { get;  set; }
        public int MaxItemCount { get; set; }
        public int NpcID { get;  set; }
        public int MaxSellTime { get;  set; }

        public static BigStore Parse(string packet)
        {
            string[] _loc4_ = packet.Split(';');
            string[] _loc14_ = _loc4_[0].Split(',');
            BigStore bs = null;
            try
            {
                bs = new BigStore()
                {
                    Quantity1 = int.Parse(_loc14_[0]),
                    Quantity2 = int.Parse(_loc14_[1]),
                    Quantity3 = int.Parse(_loc14_[2]),
                    Types = _loc4_[1].Split(','),
                    Tax = int.Parse(_loc4_[2]),
                    MaxLevel = int.Parse(_loc4_[3]),
                    MaxItemCount = int.Parse(_loc4_[4]),
                    NpcID = int.Parse(_loc4_[5]),
                    MaxSellTime = int.Parse(_loc4_[6])
                };
            }

            catch
            {
                return null;
            }

            return bs;
        }
    }
}
