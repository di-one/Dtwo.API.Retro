using Dtwo.API.Retro.AnkamaGames.Dofus.Enums;

namespace Dtwo.API.Retro.AnkamaGames.Dofus.Managers
{
    public class ItemsManager
    {
        public static InventoryObjectType GetInventoryObjects(int type)
        {
            switch (type)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 23:
                case 22:
                case 83:
                    return InventoryObjectType.EQUIPMENT;

                case 12:
                case 13:
                case 33:
                case 85:
                case 86:
                case 42:
                case 43:
                case 44:
                case 45:
                case 49:
                case 89:
                    return InventoryObjectType.DIVERS;

                case 15:
                case 26:
                case 28:
                case 34:
                case 35:
                case 36:
                case 38:
                case 41:
                case 46:
                case 47:
                case 48:
                case 50:
                case 51:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                case 65:
                case 66:
                case 68:
                case 84:
                case 90:
                case 96:
                case 97:
                case 98:
                case 100:
                case 103:
                case 104:
                case 105:
                case 106:
                case 107:
                case 108:
                case 109:
                case 111:
                case 116:
                    return InventoryObjectType.RESOURCE;

                case 24:
                    return InventoryObjectType.QUEST;

                default:
                    return InventoryObjectType.UNKNOW;
            }
        }
    }
}
