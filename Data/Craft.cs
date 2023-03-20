using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.Models
{
    public class Craft
    {
        public int Id { get; set; }
        public string Data { get; set; }

        public Dictionary<int, int> GetDataParsed() //id, quantity
        {
            Dictionary<int, int> ret = new Dictionary<int, int>();
            string[] split = Data.Split(';');
            if (split != null)
            {
                for (int i = 0; i < split.Length; i++)
                {
                    string[] split2 = split[i].Split('*');
                    if (split2.Length == 2)
                    {
                        ret.Add(int.Parse(split2[0]), int.Parse(split2[1]));
                    }
                }
            }

            return ret;
        }
    }
}
