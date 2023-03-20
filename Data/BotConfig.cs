using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.Models
{
    public class BotConfig
    {
        public List<BotBank> BotBanks { get; set; }
    }

    public class BotBank
    {
        public string AccountName { get; set; }
        public string CharacterName { get; set; }
        public string Password { get; set; }
    }
}
