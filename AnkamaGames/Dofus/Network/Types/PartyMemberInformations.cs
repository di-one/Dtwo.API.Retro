using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dtwo.API.Retro.Network.Messages;

namespace Dtwo.API.Retro.AnkamaGames.Dofus.Network.Types
{
    public class PartyMemberInformations
    {
        public int id;
        public string name;
        public uint level;
        public uint lifePoints;
        public uint maxLifePoints;
        public uint prospecting;
        public uint initiative;
        public int alignmentSide;

        public static PartyMemberInformations Build(string message)
        {
            var values = RetroMessageParser.GetValues(message);
            if (values == null) return null;

            try
            {
                PartyMemberInformations infos = new PartyMemberInformations();
                infos.id = int.Parse(values[0]);
                infos.name = values[1];
                // 2 = skin
                // 3,4,5,6 = color
                var lifes = values[7].Split(',');
                infos.lifePoints = uint.Parse(lifes[0]);
                infos.maxLifePoints = uint.Parse(lifes[1]);
                infos.level = uint.Parse(values[8]);
                infos.initiative = uint.Parse(values[9]);
                infos.prospecting = uint.Parse(values[10]);
                infos.alignmentSide = int.Parse(values[11]);

                return infos;
            }
            catch (Exception ex)
            {

                LogManager.LogError($"{nameof(PartyMemberInformations)}.{Build}", ex.ToString());
                return null;
            }
        }

        public static PartyMemberInformations[] Builds(string message)
        {
            var array = RetroMessageParser.GetArray(message);
            if (array == null) return null;

            PartyMemberInformations[] members = new PartyMemberInformations[array.Length];

            if (array != null)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    PartyMemberInformations member = Build(array[i]);
                    if (member == null)
                    {
                        //error
                        return null;
                    }

                    members[i] = member;
                }
            }

            return members;
        }
    }
}
