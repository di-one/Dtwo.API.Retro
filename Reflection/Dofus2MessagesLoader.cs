using System.Reflection;
using Dtwo.API.DofusBase.Reflection;
using Dtwo.API.Retro.Network.Messages;

namespace Dtwo.API.Retro.Reflection
{
    public class RetroMessagesLoader : MessagesLoader<RetroMessage>
    {
        public static RetroMessagesLoader Instance { get; private set; }
        protected override Assembly m_assembly => Assembly.GetAssembly(typeof(RetroMessage));

        public RetroMessagesLoader()
        {
            Instance = this;
        }

        public RetroMessage? GetMessageStartWidth(string message, out string key)
        {
            key = null;

            for (int i = 0; i < Messages.Count; i++)
            {
                var elem = Messages.ElementAt(i);

                if (message.StartsWith(elem.Key))
                {
                    key = elem.Key;
                    return elem.Value();
                }
            }

            return null;
        }
    }
}
