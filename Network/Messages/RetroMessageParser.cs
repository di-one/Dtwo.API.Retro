using System.Collections;
using System.Text;
using Dtwo.API.DofusBase.Network.Messages;
using Dtwo.API.Retro.Reflection;


namespace Dtwo.API.Retro.Network.Messages
{
    public class RetroMessageParser : MessageParser
    {
        private byte[] m_buffer;

        public override void OnGetPacket(byte[] data, int length)
        {
            m_buffer = data;
            string dataStr = Encoding.UTF8.GetString(m_buffer, 0, length);
            List<string> packets = dataStr.Replace("\x0a", string.Empty).Split('\0').Where(x => x != string.Empty).ToList();
            if (packets != null)
            {
                for (int i = 0; i < packets.Count; i++)
                {
                    try
                    {
                        var packet = packets[i];
                        Console.WriteLine(packet);


                        string identifier = null;
                        RetroMessage? message = RetroMessagesLoader.Instance.GetMessageStartWidth(packet, out identifier);

                        if (message == null)
                        {
                            // message not found
                            continue;
                        }

                        packet = packet.Substring(identifier.Length, packet.Length - identifier.Length);

                        if (message.Build(packet) == false)
                        {
                            LogManager.LogError("Can't build the message " + message.GetType().ToString());
                            // cant parse message
                            continue;
                        }

                        OnGetMessage?.Invoke(identifier, message);
                    }
                    catch (Exception ex)
                    {
                        LogManager.LogError("Error on get packet : " + ex.ToString());
                        continue;
                    }
                }           
            }
        }

        public static ReadOnlySpan<string> GetArray(string msg)
        {
            return msg.Split("|");
        }

        public static ReadOnlySpan<string> GetValueArray(string msg)
        {
            return msg.Split(",");
        }

        public static ReadOnlySpan<string> GetValues(string msg)
        {
            return msg.Split(';');
        }

        public static ReadOnlySpan<string> SkipSpan(int to, ReadOnlySpan<string> source)
        {
            string[] dest = new string[source.Length - to];

            for (int i = to; i < source.Length; i++)
            {
                dest[i] = source[i];
            }

            return dest;
        }
    }
}
