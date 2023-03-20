using System.Data.Common;
using Dtwo.API.Retro.AnkamaGames.Dofus.Enums;
using Dtwo.API.Retro.AnkamaGames.Dofus.Managers;

namespace Dtwo.API.Retro.Data
{
    public class Item : DatabaseItem
    {
        public enum CaracteristicItemType
        {
            PA,
            PM,
            PO,
            VIE,
            VITALITE,
            SAGESSE,
            INTELLIGENCE,
            AGIILITE,
            FORCE,
            CHANCE,
            DOMMAGE,
            DOMMAGE_PER,
            DOMMAGE_PIEGE_PER,
            DOMMAGE_PIEGE_FIXE,
            RES_FIXE_TERRE,
            RES_FIXE_EAU,
            RES_FIXE_FEU,
            RES_FIXE_AIR,
            RES_FIXE_NEUTRE,
            RES_PER_TERRE,
            RES_PER_EAU,
            RES_PER_AIR,
            RES_PER_FEU,
            RES_PER_NEUTRE,
            RENVOI_DOMMAGE,
            PROSPECTION,
            INVOC,
            INITIATIVE,
            PODS,
            COUP_CRITIQUE,
            ECHEC_CRITIQUE,
            SOIN,
            REGEN,

        }
        public class CaracteristicItem
        {
            public CaracteristicItemType Type;
            public int Value;
        }

        public class CaracteristicItemBase : CaracteristicItem
        {
            public int ValueMin { get => Value; set => Value = value; }
            public int ValueMax;
        }

        public string Name { get; private set; }
        public int Pods { get; private set; }
        public int Level { get; private set; }
        public int Type { get; private set; }
        public string PathImage { get; private set; }
        public InventoryObjectType InventoryObjectType { get; private set; }

        public List<CaracteristicItem> ActualCaracs = new List<CaracteristicItem>();
        public List<CaracteristicItemBase> BaseCaracs = new List<CaracteristicItemBase>();

        public override void Init<T>(DbDataReader resultQuery)
        {
            base.Init<T>(resultQuery);

            if (resultQuery != null)
            {
                for (int i = 0; i < resultQuery.FieldCount; i++)
                {
                    switch (resultQuery.GetName(i)?.ToLower())
                    {
                        case "name":
                            Name = resultQuery[i].GetType() == typeof(DBNull) ? "UNKNOW" : resultQuery.GetString(i);
                            break;
                        case "pods":
                            Pods = resultQuery[i].GetType() == typeof(DBNull) ? 0 : resultQuery.GetInt32(i);
                            break;
                        case "level":
                            Level = resultQuery[i].GetType() == typeof(DBNull) ? 0 : resultQuery.GetInt32(i);
                            break;
                        case "type":
                            Type = resultQuery[i].GetType() == typeof(DBNull) ? -1 : resultQuery.GetInt32(i);
                            break;
                    }
                }
            }
            else
            {
                Name = "UNKNOW";
                Pods = 0;
                Level = 0;
                Type = 0;
            }

            InventoryObjectType = ItemsManager.GetInventoryObjects(Type);
            //PathImage = File.Exists(Manager.ASSETS_PATH + "/Items/" + Id + ".png") ? Manager.ASSETS_PATH + "/Items/" + Id + ".png" : Manager.ASSETS_PATH + "/Items/unknow.png";
        }

        public static List<CaracteristicItem> ParseCarac(string txt, bool baseCaracs)
        {
            List<CaracteristicItem> caracs = new List<CaracteristicItem>();
            // 76 # a      # 0      # 0      # 0d0+1  , next
            // 7b # 1      # 0      # 0      # 0d0+1  , next
            // ID # Divers # Divers # Divers # Aléatoire (exemple CAC) , Caractéristique suivante

            // 1d3+5 = Un chiffre alétoire entre 1 à 3, puis rajoute à ça +5
            if (txt != "")
            {
                string[] separateCaracteristique = txt.Split(','); // 76#a#0#0,7b#1#0#0#0d0+1

                try
                {
                    {
                        //var withBlock = resultat;
                        for (int i = 0; i <= separateCaracteristique.Count() - 1; i++) // 76#a#0#0
                        {
                            if (separateCaracteristique[i] != "")
                            {
                                string[] separate = separateCaracteristique[i].Split('#');

                                long choixCaractéristique = separate[0] != "-1" ? Convert.ToInt64(separate[0], 16) : -1; // 76

                                long valeur1 = Convert.ToInt64(separate[1], 16);
                                long valeur2 = 0;
                                long valeur3 = Convert.ToInt64(separate[3], 16);

                                if (baseCaracs)
                                    valeur2 = Convert.ToInt64(separate[1], 16);

                                switch (choixCaractéristique)
                                {
                                    case -1:
                                        {
                                            break;
                                        }

                                    case 93:
                                        {

                                            break;
                                        }

                                    case 96:
                                        {

                                            break;
                                        }

                                    case 97:
                                        {

                                            break;
                                        }

                                    case 98 // Dégât air
                             :
                                        {

                                            break;
                                        }

                                    case 99:
                                        {

                                            break;
                                        }

                                    case 100 // 64 = Dommage neutre ?
                             :
                                        {

                                            break;
                                        }

                                    case 101 // 65 = PA -
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)-valeur1;
                                            carac.Type = CaracteristicItemType.PA;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 110:
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.VIE;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 118 // 76 = Force +
                             :
                                    case 157 // 9d = Force -
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.VITALITE;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 125:
                                    case 153:// 7d = Vitalité +
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.VITALITE;
                                            caracs.Add(carac);

                                            break;
                                        }


                                    case 124:
                                    case 156:// 7c = Sagesse +
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.SAGESSE;
                                            caracs.Add(carac);

                                            break;
                                        }


                                    case 126:
                                    case 155:// 7e = Intelligence +
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.INTELLIGENCE;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 123 // 7b = Chance +
                             :

                                    case 152 // 98 = Chance -
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.CHANCE;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 119 // 77 = Agilité +
                             :

                                    case 154 // 9a = Agilité -
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.AGIILITE;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 111 // 6f = PA +
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.PA;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 128 // 80 = PM +
                             :

                                    case 127 // 7f = PM -
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.PM;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 117 // 75 = PO +
                             :

                                    case 116 // 74 = PO -
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.PO;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 182 // b6 = Invocation +
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.INVOC;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 174 // ae = Initiative +
                             :

                                    case 175 // af = Initiative -
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.INITIATIVE;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 176 // b0 = Prospection +
                             :

                                    case 177 // b1 = Prospection -
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.PROSPECTION;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 158 // 9e = Pods +
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.PODS;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 115 // 73 = Coups Critiques +   
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.COUP_CRITIQUE;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 112 // 70 = Dommage +
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.DOMMAGE;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 138 // 8a = %Dommage +
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.DOMMAGE_PER;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 225 // e1 = Dommage Piège +
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.DOMMAGE_PIEGE_FIXE;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 226 // e2 = %Dommage Piège +
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.DOMMAGE_PIEGE_PER;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 178 // b2 = Soin +
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.SOIN;
                                            caracs.Add(carac);

                                            break;
                                        }


                                    case 193:
                                        {
                                            break;
                                        }

                                    case 240 // f0 = Résistance Terre +
                           :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.RES_FIXE_TERRE;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 241 // f1 = Résistance Eau +
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.RES_FIXE_EAU;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 242 // f2 = Résistance Air +
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.RES_FIXE_AIR;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 243 // f3 = Résistance Feu +
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.RES_FIXE_FEU;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 244 // f4 = Résistance Neutre +
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.RES_FIXE_NEUTRE;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 210 // d2 = %Résistance Terre +
                             :
                                    case 215 // d7 = %Résistance Terre -
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.RES_PER_TERRE;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 211 // d3 = %Résistance Eau +
                             :

                                    case 216 // d8 = %Résistance Eau -
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.RES_PER_EAU;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 212 // d4 = %Résistance Air  +
                             :

                                    case 217 // d9 = %Résistance Air  -
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.RES_PER_AIR;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 213 // d5 = %Résistance Feu +
                             :
                                    case 218 // da = %Résistance Feu -
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.RES_PER_FEU;
                                            caracs.Add(carac);

                                            break;
                                        }

                                    case 214 // d6 = %Résistance Neutre +
                             :

                                    case 219 // db = %Résistance Neutre -
                             :
                                        {
                                            CaracteristicItem carac = null;
                                            if (baseCaracs == false)
                                            {
                                                carac = new CaracteristicItem();
                                            }

                                            else
                                            {
                                                carac = new CaracteristicItemBase();
                                                ((CaracteristicItemBase)carac).ValueMax = (int)valeur2;
                                            }

                                            carac.Value = (int)valeur1;
                                            carac.Type = CaracteristicItemType.RES_PER_NEUTRE;
                                            caracs.Add(carac);

                                            break;
                                        }


                                    case 108 // 6c = PDV rendus : X à Y
                           :
                                        {
                                            break;
                                        }

                                    case 600:
                                        {
                                            break;
                                        }

                                    case 601 // 259
                           :
                                        {

                                            // resultat &= "Potion de cite : "

                                            //switch (Convert.ToInt64(separate[2], 16))
                                            //{
                                            //    case 6167:
                                            //        {
                                            //            break;
                                            //        }

                                            //    case 6159:
                                            //        {
                                            //            break;
                                            //        }

                                            //    default:
                                            //        {
                                            //            ErreurFichier(0, "Unknow", "ItemsCharacteristics", caracteristique + Constants.vbCrLf + separateCaracteristique[i]);
                                            //            break;
                                            //        }
                                            //}

                                            break;
                                        }

                                    case 605 // 25d
                             :
                                        {
                                            break;
                                        }

                                    case 614:
                                        {
                                            break;
                                        }

                                    case 622:
                                        {
                                            break;
                                        }

                                    case 623 // 26f = Pierre d'âme 
                           :
                                        {

                                            // 26f#0#0#93,26f#0#0#94,26f#0#0#94,26f#0#0#94,26f#0#0#65,26f#0#0#65,26f#0#0#65,26f#0#0#65;
                                            //if (IsNothing(withBlock.PierreAme))
                                            //    withBlock.PierreAme = new List<string>();

                                            //withBlock.PierreAme.Add(valeur3);
                                            break;
                                        }

                                    case 699:
                                        {
                                            break;
                                        }

                                    case 701:
                                        {
                                            //withBlock.Puissance = valeur1;
                                            break;
                                        }

                                    case 795:
                                        {
                                            break;
                                        }

                                    case 800 // 320 = Point de vie +
                           :
                                        {
                                            break;
                                        }

                                    case 806 // 326 = 'Repas et Corpulence 
                           :
                                        {

                                            // 326#1#0#1ab

                                            //valeur2 = Convert.ToInt64(separate[2], 16);

                                            //if (valeur3 >= 7)
                                            //{
                                            //    valeur3 = valeur3 > 100 ? 100 : valeur3;

                                            //    withBlock.FamilierRepas = -valeur3;
                                            //    withBlock.FamilierCorpulence = "Maigrichon";
                                            //}
                                            //else if (valeur2 >= 7)
                                            //{
                                            //    withBlock.FamilierRepas = valeur3;
                                            //    withBlock.FamilierCorpulence = "Obese";
                                            //}
                                            //else
                                            //{
                                            //    withBlock.FamilierRepas = "0";
                                            //    withBlock.FamilierCorpulence = "Normal";
                                            //}

                                            break;
                                        }

                                    case 807 // 327 = Dernier Repas (objet utilisé)
                             :
                                        {

                                            //// 327#0#0#734

                                            //switch (valeur3)
                                            //{
                                            //    case 2114:
                                            //        {
                                            //            withBlock.FamilierDernierRepas = "Aliment inconnu";
                                            //            break;
                                            //        }

                                            //    case "0":
                                            //        {
                                            //            withBlock.FamilierDernierRepas = "Aucun";
                                            //            break;
                                            //        }

                                            //    default:
                                            //        {
                                            //            withBlock.FamilierDernierRepas = VarItems(valeur3).Nom;
                                            //            break;
                                            //        }
                                            //}

                                            break;
                                        }

                                    case 808 // "328" 'Date / Heure  
                             :
                                        {

                                            //// 328 # 28a   # cc          # 398   = A mangé le : 04/03/650 9:20
                                            //// 328 # Année # Mois + Jour # Heure

                                            //valeur2 = Convert.ToInt64(separate[2], 16);

                                            //int Année = valeur1 + 1370;

                                            //int Mois = valeur2 < 100 ? 1 : Strings.Mid(valeur2, 1, valeur2.ToString().Length - 2) + 1;
                                            //int Jour = valeur2 < 100 ? valeur2 : Strings.Mid(valeur2, valeur2.ToString().Length - 1, 2);

                                            //string Heure = valeur3.ToString().Insert(valeur3.ToString().Length - 2, ":");
                                            //if (Heure.Length == 3)
                                            //    Heure = "00" + Heure;

                                            //withBlock.FamilierDateRepas = Jour + "/" + Mois + "/" + Année + " " + Heure;
                                            break;
                                        }

                                    case 812:
                                        {
                                            //withBlock.ResistanceItem = valeur1 + "/" + valeur3;
                                            break;
                                        }

                                    case 830:
                                        {

                                            // resultat &= "Potion de : "

                                            switch (valeur3)
                                            {
                                                case 1:
                                                    {
                                                        break;
                                                    }

                                                case 2:
                                                    {
                                                        break;
                                                    }
                                            }

                                            break;
                                        }

                                    case 940 // "3ac" 'Capacité accrue Familier
                             :
                                        {

                                            // 3ac#0#0#a
                                            // a = 10, donc le familier peut avoir +10 en caract, etc... selon le familier.
                                            //withBlock.FamilierCapaciteAccrue = true;
                                            break;
                                        }

                                    case 948:
                                        {
                                            break;
                                        }

                                    case 970:
                                        {
                                            break;
                                        }

                                    case 971:
                                        {
                                            break;
                                        }

                                    case 972:
                                        {
                                            break;
                                        }

                                    case 973:
                                        {
                                            break;
                                        }

                                    case 974:
                                        {
                                            break;
                                        }

                                    case 985:
                                        {
                                            break;
                                        }

                                    case 988:
                                        {
                                            break;
                                        }

                                    case 994 // 3e2
                           :
                                        {
                                            //withBlock.DragodindeDate = DateTime.TimeOfDay;
                                            break;
                                        }

                                    case 995 // 3e3 = ID de la dragodinde pour avoir les caractéristiques (quand elle se trouve dans l'inventaire)
                             :
                                        {
                                            // 3e3#c0a#1710bbb0c60#0

                                            //withBlock.DragodindeIdUnique = "Rd" + valeur1 + Constants.vbCrLf + "|" + separate[2];
                                            break;
                                        }

                                    case 996 // 3e4 = Nom du joueur qui posséde la dragodinde.
                             :
                                        {
                                            // 3e4#0#0#0#Linaculer

                                            //withBlock.DragodindePossesseur = separate[4];
                                            break;
                                        }

                                    case 997 // 3e5 = Nom de la dragodinde
                             :
                                        {
                                            // 3e5#15#0#0#Linaculeur

                                            //withBlock.DragodindeNom = separate[4];
                                            break;
                                        }

                                    case 998 // "3e6" ' Jour/ heure / minute restant.
                             :
                                        {
                                            // 3e6#13#17#3b

                                            //withBlock.DragodindeDateEnParchemin = DateTime.DateAdd(DateInterval.Day, valeur1, DateTime.Today).ToString() + " " + Convert.ToInt64(separate[2], 16) + ":" + valeur3;
                                            break;
                                        }

                                    case 805 // "325" 'Divers
                             :
                                        {
                                            break;
                                        }

                                    default:
                                        {
                                            //ErreurFichier(0, "Unknow", "ItemsCharacteristics", caracteristique + Constants.vbCrLf + separateCaracteristique[i]);
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return caracs;
        }
    }
}
