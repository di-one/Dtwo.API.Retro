using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.DofusBehavior.Map
{
    public class Cell
    {
        public Models.Cell CellData { get; private set; }

        public bool IsTeleport => Teleports.Contains(CellData.LayerObject1) || Teleports.Contains(CellData.LayerObject2);
        public bool IsInteractive => CellData.Type == Models.Cell.CellType.INTERACTIVE_OBJECT || CellData.InteractiveMapObject != null;
        public bool IsWalkable => CellData.IsActive && CellData.Type != Models.Cell.CellType.NO_WALKABLE && !IsInteractiveAndWalkable;
        public bool IsInteractiveAndWalkable => CellData.Type == Models.Cell.CellType.INTERACTIVE_OBJECT || CellData.InteractiveMapObject != null && CellData.InteractiveMapObject.Model != null && !CellData.InteractiveMapObject.Model.Walkable;


        public int GetDistanceBetween(Cell dest) => Math.Abs(CellData.X - dest.CellData.X) + Math.Abs(CellData.Y - dest.CellData.Y);
        public bool IsInLine(Cell cell) => CellData.X == cell.CellData.X || CellData.Y == cell.CellData.Y;

        public static readonly int[] Teleports = { 1030, 1029, 1764, 2298, 745 };

        public int CostH { get; set; } = 0;
        public int CostG { get; set; } = 0;
        public int CostF { get; set; } = 0;
        public Cell CellRoot { get; set; } = null;

        internal Cell(Models.Cell cellData, MapInformations mapData, short interactiveMapObjectId)
        {
            CellData = cellData;

            if (interactiveMapObjectId != -1)
            {
                CellData.InteractiveMapObject = new Map.InteractiveMapObject(interactiveMapObjectId, this);
                //if (mapData.InteractiveMapObjects == null)
                //    mapData.InteractiveMapObjects = new System.Collections.Concurrent.ConcurrentDictionary<int, InteractiveMapObject>();

                mapData.InteractiveMapObjects.TryAdd(cellData.Id, CellData.InteractiveMapObject);
            }

            int width = mapData.Map.Width;
            int _loc5 = CellData.Id / ((width * 2) - 1);
            int _loc6 = CellData.Id - (_loc5 * ((width * 2) - 1));
            int _loc7 = _loc6 % width;
            CellData.Y = _loc5 - _loc7;
            CellData.X = (CellData.Id - ((width - 1) * CellData.Y)) / width;
        }

        public char GetdirectionChar(Cell cell)
        {
            if (CellData.X == cell.CellData.X)
                return cell.CellData.Y < CellData.Y ? (char)(3 + 'a') : (char)(7 + 'a');
            else if (CellData.Y == cell.CellData.Y)
                return cell.CellData.X < CellData.X ? (char)(1 + 'a') : (char)(5 + 'a');

            else if (CellData.X > cell.CellData.X)
                return CellData.Y > cell.CellData.Y ? (char)(2 + 'a') : (char)(0 + 'a');
            else if (CellData.X < cell.CellData.X)
                return CellData.Y < cell.CellData.Y ? (char)(6 + 'a') : (char)(4 + 'a');

            throw new Exception("Error dirrection not found");
        }
    }
}
