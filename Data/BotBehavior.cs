using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.Models
{
    public class BotBehavior
    {
        public DelayAction ReadyToFightDelay { get; set; }
        public DelayAction LaunchSpellDelay { get; set; }
        public DelayAction EndTurnFightDelay { get; set; }
        public DelayAction MoveFightDelay { get; set; }
        public DelayAction MoveDelay { get; set; }
        public DelayAction StoreBankItemDelay { get; set; }
        public DelayAction DeclineRequestDelay { get; set; }
        public DelayAction RecoltDelay { get; set; }
        public DelayAction MoveGroupDelay { get; set; }
        public string ChatPassword { get; set; }

        public bool AwakeMode { get; set; }

        public static BotBehavior CreateDefault()
        {
            return new BotBehavior()
            {
                ReadyToFightDelay = DelayAction.CreateDefault(),
                LaunchSpellDelay = DelayAction.CreateDefault(),
                EndTurnFightDelay = DelayAction.CreateDefault(),
                MoveFightDelay = DelayAction.CreateDefault(),
                MoveDelay = DelayAction.CreateDefault(),
                StoreBankItemDelay = DelayAction.CreateDefault(),
                DeclineRequestDelay = DelayAction.CreateDefault(),
                RecoltDelay = DelayAction.CreateDefault(),
                MoveGroupDelay = new DelayAction() { Min = 2500, Max = 2750 },
                AwakeMode = false
            };
        }
    }
}
