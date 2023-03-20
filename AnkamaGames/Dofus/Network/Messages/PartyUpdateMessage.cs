using Dtwo.API.Retro.AnkamaGames.Dofus.Network.Types;
using Dtwo.API.Retro.Network.Messages;

namespace Dtwo.API.Retro.AnkamaGames.Dofus.Network.Messages
{
    // PM~id;name;skin;color1;color2;color3;accesoire1,2,3,etc...;life;maxlife;level;initiaitve;prospection;sex
    public class PartyUpdateMessage : RetroMessage
    {
        public PartyMemberInformations MemberInformations { get; private set; }

        public override bool Build(string message)
        {
            MemberInformations = PartyMemberInformations.Build(message);

            return MemberInformations != null;
        }
    }
}
