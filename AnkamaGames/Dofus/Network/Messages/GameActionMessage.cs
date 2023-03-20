using Dtwo.API.Retro.Network.Messages;

namespace Dtwo.API.Retro.AnkamaGames.Dofus.Network.Messages
{
    // GA
    public class GameActionMessage : RetroMessage
    {
        public int SourceId { get; private set; }
        public override bool Build(string message)
        {
            try
            {
                var values = RetroMessageParser.GetValues(message.Substring(1, message.Length - 1));
                SourceId = int.Parse(values[0]);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
