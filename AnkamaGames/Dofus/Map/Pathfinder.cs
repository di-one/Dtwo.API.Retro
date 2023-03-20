using D_One.Core.DofusBehavior.Cryptography;
using D_One.Core.DofusBehavior.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.DofusBehavior.Map.PathFinding
{
    public class Pathfinder : IDisposable
    {
        private Cell[] m_cells { get; set; }
        private MapManager m_mapManager { get; set; }

        private bool m_disposed;

        internal void SetMapManager(MapManager mapManager)
        {
            m_mapManager = mapManager;
            m_cells = m_mapManager.MapData.Cells;
        }

        public List<Cell> GetPath(Cell cellStart, Cell cellTarget, List<Cell> impasibles, bool stopInFront, byte distanceFront, bool force = false)
        {
            if (cellStart == null || cellTarget == null)
            {
                Console.WriteLine("CELLSTART = NULL || CELLTARGET = NULL");
                return null;
            }

            List<Cell> pasibles = new List<Cell>() { cellStart };

            if (impasibles.Contains(cellTarget))
                impasibles.Remove(cellTarget);

            while (pasibles.Count > 0)
            {
                int index = 0;
                for (int i = 1; i < pasibles.Count; i++)
                {
                    if (pasibles[i].CostF < pasibles[index].CostF)
                        index = i;

                    if (pasibles[i].CostF != pasibles[index].CostF) continue;
                    if (pasibles[i].CostG > pasibles[index].CostG)
                        index = i;

                    if (pasibles[i].CostG == pasibles[index].CostG)
                        index = i;

                    if (pasibles[i].CostG == pasibles[index].CostG)
                        index = i;
                }

                Cell actual = pasibles[index];

                if (stopInFront && GetDistance(actual, cellTarget) <= distanceFront && !cellTarget.IsWalkable)
                    return GetBackwardPath(cellStart, actual);

                if (actual == cellTarget)
                    return GetBackwardPath(cellStart, cellTarget);

                pasibles.Remove(actual);
                impasibles.Add(actual);

                foreach (Cell neigbor in GetNeighbors(actual))
                {
                    if (!neigbor.IsTeleport)
                    {
                        if (impasibles.Contains(neigbor) || !neigbor.IsWalkable)
                        {
                            if (force && cellTarget.IsInteractive && !cellTarget.CellData.InteractiveMapObject.Model.Recoltable && GetNeighbors(cellTarget).Contains(neigbor) && neigbor.IsWalkable)
                            {
                                Console.WriteLine("__ PATH CODE 1");
                            }
                            else
                            {
                                if (stopInFront && GetDistance(neigbor, cellTarget) <= distanceFront && cellTarget.CellData.InteractiveMapObject.Model.Recoltable)
                                {
                                    // monster on final cell
                                    if (m_mapManager.GetMonsters().Find(x => x.Cell.CellData.Id == neigbor.CellData.Id) != null)
                                    {
                                        continue;
                                    }
                                }

                                if (neigbor == cellTarget && cellTarget.IsWalkable)
                                {

                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }

                    if (neigbor.IsTeleport && neigbor != cellTarget)
                            continue;

                    int temporal_g = actual.CostG + GetDistance(neigbor, actual);
                   
                    if (!pasibles.Contains(neigbor))
                        pasibles.Add(neigbor);
                    else if (temporal_g >= neigbor.CostG)
                        continue;

                    neigbor.CostG = temporal_g;
                    neigbor.CostH = GetDistance(neigbor, cellTarget);
                    neigbor.CostF = neigbor.CostG + neigbor.CostH;
                    neigbor.CellRoot = actual;
                }
            }

            Console.WriteLine("RETURN NULL");
            return null;
        }

        private List<Cell> GetBackwardPath(Cell cellStart, Cell cellDest)
        {
            Cell cell = cellDest;
            List<Cell> backwardPath = new List<Cell>();

            while (cell != cellStart)
            {
                backwardPath.Add(cell);
                cell = cell.CellRoot;
            }

            backwardPath.Add(cellStart);
            backwardPath.Reverse();

            return backwardPath;
        }

        public List<Cell> GetNeighbors(Cell cell)
        {

            List<Cell> neighbors = new List<Cell>();

            Cell RightCell = m_cells.FirstOrDefault(node => 
            node.CellData.X == cell.CellData.X + 1 && node.CellData.Y == cell.CellData.Y);

            Cell LeftCell = m_cells.FirstOrDefault(node => 
            node.CellData.X == cell.CellData.X - 1 && node.CellData.Y == cell.CellData.Y);

            Cell BottomCell = m_cells.FirstOrDefault(node => 
            node.CellData.X == cell.CellData.X && node.CellData.Y == cell.CellData.Y + 1);

            Cell TopCell = m_cells.FirstOrDefault(node => 
            node.CellData.X == cell.CellData.X && node.CellData.Y == cell.CellData.Y - 1);

            if (RightCell != null)
                neighbors.Add(RightCell);
            if (LeftCell != null)
                neighbors.Add(LeftCell);
            if (BottomCell != null)
                neighbors.Add(BottomCell);
            if (TopCell != null)
                neighbors.Add(TopCell);

            Cell superiorLeft = m_cells.FirstOrDefault(node => node.CellData.X == cell.CellData.X - 1 && node.CellData.Y == cell.CellData.Y - 1);
            Cell inferiorRight = m_cells.FirstOrDefault(node => node.CellData.X == cell.CellData.X + 1 && node.CellData.Y == cell.CellData.Y + 1);
            Cell inferiorLeft = m_cells.FirstOrDefault(node => node.CellData.X == cell.CellData.X - 1 && node.CellData.Y == cell.CellData.Y + 1);
            Cell superiorRight = m_cells.FirstOrDefault(node => node.CellData.X == cell.CellData.X + 1 && node.CellData.Y == cell.CellData.Y - 1);

            if (superiorLeft != null)
                neighbors.Add(superiorLeft);
            if (inferiorRight != null)
                neighbors.Add(inferiorRight);
            if (inferiorLeft != null)
                neighbors.Add(inferiorLeft);
            if (superiorRight != null)
                neighbors.Add(superiorRight);

            return neighbors;
        }

        public static int GetDistance(Cell a, Cell b) => ((a.CellData.X - b.CellData.X) * (a.CellData.X - b.CellData.X)) + ((a.CellData.Y - b.CellData.Y) * (a.CellData.Y - b.CellData.Y));

        public static string GetPathfindingString(List<Cell> path)
        {
            Cell cellDest = path.Last();

            if (path.Count <= 2)
                return cellDest.GetdirectionChar(path.First()) + Hash.GetCellChar(cellDest.CellData.Id);

            StringBuilder pathfinder = new StringBuilder();
            char directionPrevious = path[1].GetdirectionChar(path.First()), actualDirection;

            for (int i = 2; i < path.Count; i++)
            {
                Cell cellActual = path[i];
                Cell cellPrevious = path[i - 1];
                actualDirection = cellActual.GetdirectionChar(cellPrevious);

                if (directionPrevious != actualDirection)
                {
                    pathfinder.Append(directionPrevious);
                    pathfinder.Append(Hash.GetCellChar(cellPrevious.CellData.Id));

                    directionPrevious = actualDirection;
                }
            }

            pathfinder.Append(directionPrevious);
            pathfinder.Append(Hash.GetCellChar(cellDest.CellData.Id));
            return pathfinder.ToString();
        }

        //private static readonly Dictionary<AnimationType, DurationAnimation> TypeAnimations = new Dictionary<AnimationType, DurationAnimation>()
        //{
        //    { AnimationType.MOUNT, new DurationAnimation(135, 200, 120) },
        //    { AnimationType.RUNNING, new DurationAnimation(300, 180, 180) },
        //    { AnimationType.WALKING, new DurationAnimation(480, 330, 330) },
        //    { AnimationType.GHOST, new DurationAnimation(57, 85, 50) }
        //};

        //public static int GetMovingTime(Cell cell, List<Cell> path, bool useMount = false)
        //{
        //    int time = 20;
        //    DurationAnimation animation;

        //    //if (useMount)
        //    //    animation = TypeAnimations[AnimationType.MOUNT];
        //    //else
        //    animation = path.Count > 3 ? TypeAnimations[AnimationType.RUNNING] : TypeAnimations[AnimationType.WALKING];

        //    Cell neighbor;

        //    for (int i = 1; i < path.Count; i++)
        //    {
        //        neighbor = path[i];


        //        if (cell.CellData.Y == neighbor.CellData.Y || cell.CellData.X == neighbor.CellData.X)
        //        {
        //            time += animation.Vertical;
        //        }
        //        else if ((neighbor.CellData.X == cell.CellData.X - 1 && neighbor.CellData.Y == cell.CellData.Y - 1)
        //                ||neighbor.CellData.X == cell.CellData.X + 1 && neighbor.CellData.Y == cell.CellData.Y + 1)
        //        {
        //            time += animation.Horizontal;
        //        }
        //        else
        //        {
        //            time += animation.Linear;
        //        }

        //        if (cell.CellData.LayerGroundLevel < neighbor.CellData.LayerGroundLevel)
        //            time += 100;
        //        else if (neighbor.CellData.LayerGroundLevel > cell.CellData.LayerGroundLevel)
        //            time -= 100;
        //        else if (cell.CellData.LayerGroundSlope != neighbor.CellData.LayerGroundSlope)
        //        {
        //            if (cell.CellData.LayerGroundSlope == 1)
        //                time += 100;
        //            else if (neighbor.CellData.LayerGroundSlope == 1)
        //                time -= 100;
        //        }
        //        cell = neighbor;
        //    }

        //    return time;
        //}

        public void Dispose() => Dispose(true);
        ~Pathfinder() => Dispose(false);

        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                m_cells = null;
                m_mapManager = null;
                m_disposed = true;
            }
        }
    }
}
