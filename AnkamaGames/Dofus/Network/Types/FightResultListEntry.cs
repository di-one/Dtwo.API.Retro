using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dtwo.API.Retro.Network.Messages;

namespace Dtwo.API.Retro.AnkamaGames.Dofus.Network.Types
{
    //winner => 2;1;sowen  ;200;0;7407232000;7407232000;9223372036854775807; ; ; ; ;
    //looser => 0;2;Hymeduz;200;0;7407232000;7407232000;9223372036854775807; ; ; ;
    public class FightResultListEntry
    {
        public int Result { get; private set; }
        public uint PlayerId { get; private set; }
        public string PlayerName { get; private set; }
        public bool IsAlive { get; private set; }
        public uint ExperienceGain { get; private set; }
        public uint KamasGain { get; private set; }
        public FightLoot FightLoot { get; private set; }

        public FightResultListEntry(int result, uint playerId, string playerName, 
           bool isAlive, uint exp, uint kamas,  FightLoot fightLoot)
        {
            Result = result;
            PlayerId = playerId;
            PlayerName = playerName;
            IsAlive = isAlive;
            ExperienceGain = exp;
            KamasGain = kamas;
            FightLoot = fightLoot;
        }

        public static FightResultListEntry Build(string message)
        {
            var values = RetroMessageParser.GetValues(message);

            if (values == null)
            {
                // error
                return null;
            }

            try
            {
                var result = int.Parse(values[0]);
                var id = uint.Parse(values[1]);
                var name = values[2];
                // level = 3
                var aliveInt = int.Parse(values[4]);
                var alive = aliveInt == 0 ? false : true;
                // exp min = 5
                // exp = 6
                // exp max = 7
                var expGain = uint.Parse(values[7]);
                // exp guild = 8
                // exp mount = 9
                // 10 = loot
                var kamas = uint.Parse(values[11]);

                return new FightResultListEntry(result, id, name, alive, expGain, kamas, null);
            }
            catch (Exception ex)
            {
                //error
                return null;
            }

        }

        public static FightResultListEntry[] Build(ReadOnlySpan<string> array)
        {
            FightResultListEntry[] results = new FightResultListEntry[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                results[i] = Build(array[i]);
            }

            return results;
        }
    }
}
