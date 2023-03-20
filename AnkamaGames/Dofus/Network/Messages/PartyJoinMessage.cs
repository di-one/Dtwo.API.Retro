using Dtwo.API.Retro.AnkamaGames.Dofus.Network.Types;
using Dtwo.API.Retro.Network.Messages;

namespace Dtwo.API.Retro.AnkamaGames.Dofus.Network.Messages
{
    // PM+<member>|<etc...>
    public class PartyJoinMessage : RetroMessage
    {
        public ReadOnlySpan<PartyMemberInformations> Members => m_members;
        private PartyMemberInformations[] m_members;

        public override bool Build(string message)
        {
            m_members = PartyMemberInformations.Builds(message);

            return m_members != null;
        }
    }
}
