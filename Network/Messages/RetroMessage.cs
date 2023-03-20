using Dtwo.API.DofusBase.Network.Messages;

namespace Dtwo.API.Retro.Network.Messages
{
    public abstract class RetroMessage : DofusMessage
    {
        public abstract bool Build(string message);
    }
}
