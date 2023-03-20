using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dtwo.API.Retro.Network.Messages;

namespace Dtwo.API.Retro.AnkamaGames.Dofus.Network.Messages
{
    //GA;103
    public class GameActionFightDeathMessage : GameActionMessage
    {
        public uint TargetId { get; private set; }
        public override bool Build(string message)
        {
            if (base.Build(message) == false)
            {
                return false;
            }

            try
            {
                var values = RetroMessageParser.GetValues(message.Substring(1, message.Length - 1));
                TargetId = uint.Parse(values[1]);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
