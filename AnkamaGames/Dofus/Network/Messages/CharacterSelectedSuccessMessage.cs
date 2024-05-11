using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_One.Core.DofusBehavior.Map.PathFinding;
using Dtwo.API.Retro.AnkamaGames.Dofus.Network.Types;
using Dtwo.API.Retro.Network.Messages;

namespace Dtwo.API.Retro.AnkamaGames.Dofus.Network.Messages
{
    // ASK
    public class CharacterSelectedSuccessMessage : RetroMessage
    {
        public CharacterInformations Infos { get; private set; }

        public override bool Build(string message)
        {
            try
            {
                string[] _loc4 = message.Substring(1, message.Length - 1).Split('|');


                int id = int.Parse(_loc4[0]);
                string name = _loc4[1];
                byte level = byte.Parse(_loc4[2]);
                byte race = byte.Parse(_loc4[3]);
                byte sex = byte.Parse(_loc4[4]);

                bool bSex = sex == 0 ? false : true;

                Infos = new CharacterInformations(bSex, race, level, name, (ulong)id);
            }
            catch (Exception ex)
            {
                LogManager.LogError($"{nameof(CharacterSelectedSuccessMessage)}.{Build}", ex.Message);
                return false;
            }

            return true;
        }
    }
}
