using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtwo.API.Retro.AnkamaGames.Dofus.Network.Types
{
    public class CharacterInformations
    {
        public readonly ulong Id;
        public readonly string Name;
        public readonly bool Sex;
        public readonly ushort Breed;
        public int Level { get; private set; }
        public CharacterInformations(bool sex, ushort breed, int level, string name, ulong id)
        {
            Id = id;
            Name = name;
            Sex = sex;
            Breed = breed;
            Level = level;
        }
    }
}
