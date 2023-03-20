using Dtwo.API.Retro.Network.Messages;

namespace Dtwo.API.Retro.AnkamaGames.Dofus.Network.Messages
{
    //GS
    public class GameFightStartMessage : RetroMessage
    {
        public override bool Build(string message)
        {
            return true;
        }
    }
}
