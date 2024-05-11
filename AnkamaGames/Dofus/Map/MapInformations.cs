using D_One.Core.DofusBehavior.Cryptography;
using Dtwo.API;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.DofusBehavior.Map
{
    public class MapInformations
    {
        public Dtwo.API.Retro.Data.Map Map { get; private set; }
        public DofusBehavior.Map.Cell[] Cells { get; private set; }
        public ConcurrentDictionary<int, DofusBehavior.Map.InteractiveMapObject> InteractiveMapObjects { get; set; }

        public MapInformations(Dtwo.API.Retro.Data.Map map)
        {
            if (InteractiveMapObjects != null) InteractiveMapObjects.Clear();
            else InteractiveMapObjects = new ConcurrentDictionary<int, InteractiveMapObject>();
            if (map == null)
            {
                LogManager.LogError(nameof(MapInformations), "Map is null");
            }
            else
            {
                Map = map;
                if (map.Data != null)
                    ParseData();
            }
        }

        private void ParseData()
        {
            Cells = new DofusBehavior.Map.Cell[Map.Data.Length / 10];
            string str;

            for (int i = 0; i < Map.Data.Length; i += 10)
            {
                str = Map.Data.Substring(i, 10);
                Cells[i / 10] = ParseCell(str, Convert.ToInt16(i / 10));
            }
        }

        private  DofusBehavior.Map.Cell ParseCell(string cellStr, short id)
        {
            byte[] cellInfos = new byte[cellStr.Length];

            for (int i = 0; i < cellStr.Length; i++)
                cellInfos[i] = Convert.ToByte(Hash.GetHash(cellStr[i]));

            Models.Cell cell = new Models.Cell();

            cell.Id = id;
            cell.Type = (Models.Cell.CellType)((cellInfos[2] & 56) >> 3);
            cell.IsActive = (cellInfos[0] & 32) >> 5 != 0;
            cell.BlockVisionLine = (cellInfos[0] & 1) != 1;
            bool hasInteractiveObject = ((cellInfos[7] & 2) >> 1) != 0;
            cell.LayerObject2 = Convert.ToInt16(((cellInfos[0] & 2) << 12) + ((cellInfos[7] & 1) << 12) + (cellInfos[8] << 6) + cellInfos[9]);
            cell.LayerObject1 = Convert.ToInt16(((cellInfos[0] & 4) << 11) + ((cellInfos[4] & 1) << 12) + (cellInfos[5] << 6) + cellInfos[6]);
            cell.LayerGroundLevel = Convert.ToByte(cellInfos[1] & 15);
            cell.LayerGroundSlope = Convert.ToByte((cellInfos[4] & 60) >> 2);

            return new D_One.Core.DofusBehavior.Map.Cell(cell, this, hasInteractiveObject ? cell.LayerObject2 : Convert.ToInt16(-1));
        }

        public void Clear()
        {
            InteractiveMapObjects?.Clear();
            Cells = null;
            Map = null;
        }
    }
}
