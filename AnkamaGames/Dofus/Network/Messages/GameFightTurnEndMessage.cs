using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dtwo.API.Retro.Network.Messages;

namespace Dtwo.API.Retro.AnkamaGames.Dofus.Network.Messages
{
    //GTF<id>
    public class GameFightTurnEndMessage : RetroMessage
    {
        public int Id { get; private set; }
        public override bool Build(string message)
        {
            try
            {
                Id = int.Parse(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
