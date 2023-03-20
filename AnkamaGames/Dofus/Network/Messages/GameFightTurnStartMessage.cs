using Dtwo.API.Retro.Network.Messages;

namespace Dtwo.API.Retro.AnkamaGames.Dofus.Network.Messages
{
    //GTS<id>|time
    public class GameFightTurnStartMessage : RetroMessage
    {
        public int Id { get; private set; }
        public uint RemainingTime { get; private set; }

        public override bool Build(string message)
        {
            try
            {
                var array = RetroMessageParser.GetArray(message);
                Id = int.Parse(array[0]);
                RemainingTime = uint.Parse(array[1]);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
