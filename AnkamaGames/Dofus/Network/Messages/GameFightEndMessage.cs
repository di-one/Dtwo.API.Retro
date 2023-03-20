using Dtwo.API.Retro.AnkamaGames.Dofus.Network.Types;
using Dtwo.API.Retro.Network.Messages;

namespace Dtwo.API.Retro.AnkamaGames.Dofus.Network.Messages
{
    //GE4166|958|0|2;1424;Leo-mars;181;0;1317997000;1332575192;1355584000         ; ; ; ;; |0;958;Abcdefghijkl;1  ;0;0         ;0         ;110                ; ; ; ; |
    //GE3665|1  |0|2;1   ;sowen   ;200;0;7407232000;7407232000;9223372036854775807; ; ; ;; |0;2  ;Hymeduz     ;200;0;7407232000;7407232000;9223372036854775807; ; ; ;
    //GE<duration>|leaderWinnerId|agression|
    public class GameFightEndMessage : RetroMessage
    {
        public uint Duration { get; private set; }
        public uint LeaderWinnerId { get; private set; }
        public bool Agression { get; private set; }

        public ReadOnlySpan<FightResultListEntry> FightResultEntries => m_fightResultEntries;
        private FightResultListEntry[] m_fightResultEntries;

        public override bool Build(string message)
        {
            //var array = RetroMessageParser.GetArray(message);

            //if (array == null)
            //{
            //    // error
            //    return false;
            //}

            //Duration = uint.Parse(array[0]);
            //LeaderWinnerId = uint.Parse(array[1]);
            //int agressionInt = int.Parse(array[2]);
            //Agression = agressionInt == 0 ? false : true;

            //array = RetroMessageParser.SkipSpan(3, array);

            //if (Agression)
            //{
            //    if (ParseEntriesAgression(array) == false)
            //    {
            //        return false;
            //    }
            //}
            //else
            //{
            //    if (ParseEntriesDefault(array) == false)
            //    {
            //        return false;
            //    }
            //}
            return true;
        }

        private bool ParseEntriesAgression(ReadOnlySpan<string> array)
        {
            return true;
        }

        private bool ParseEntriesDefault(ReadOnlySpan<string> array)
        {
            m_fightResultEntries = FightResultListEntry.Build(array);

            return m_fightResultEntries != null;
        }
    }
}
