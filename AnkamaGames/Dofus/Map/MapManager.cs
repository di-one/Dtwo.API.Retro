using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_One.Core.DofusBehavior.Cryptography;
using D_One.Core.DofusBehavior.Map.Entities;
using Dtwo.API.Retro.Data;

namespace D_One.Core.DofusBehavior.Map
{
    public class MapManager : IDisposable
    {
        public MapInformations MapData { get; internal set; }
        public ConcurrentDictionary<int, Entities.IEntity> MapEntities { get; internal set; }

        public List<Cell> GetTeleports => MapData.Cells.Where(cell => cell.CellData.Type == D_One.Core.Models.Cell.CellType.TELEPORT).Select(cell => cell).ToList();
        public Cell GetCellByID(short id) =>  MapData?.Cells?.Length <= id ? null : MapData.Cells[id];/*.FirstOrDefault(x => x.CellData.Id == id);*/
        public Cell GetCellByCoordinates(int x, int y) => MapData.Cells.FirstOrDefault(cell => cell.CellData.X == x && cell.CellData.Y == y);
        public List<Cell> GetOccupedCells => MapEntities.Values.Select(c => c.Cell).ToList();
        public List<Entities.Npc> GetNpcs => MapEntities.Values.Where(n => n is Entities.Npc).Select(n => n as Entities.Npc).ToList();
        public List<Entities.Monster> GetMonsters() => MapEntities.Values.Where(n => n is Entities.Monster).Select(n => n as Entities.Monster).ToList();
        public List<Entities.Character> GetCharacters() => MapEntities.Values.Where(n => n is Entities.Character).Select(n => n as Entities.Character).ToList();


        public Entities.Character GetPlayer(string name) => GetCharacters().Find(x => x.Name.ToLower() == name.ToLower());

        /// <summary>
        /// Called when a new map is loaded (on connexion or map changed)
        /// </summary>
        public event Action OnMapLoaded;

        /// <summary>
        /// Called when a entities data on the map are refreshed
        /// </summary>
        public event Action OnEntitiesRefresh;

        /// <summary>
        /// Called whend a fight is launched on the actual map
        /// </summary>
        public event Action<int, int> OnFightLaunchedInMap;

        public Action<IEntity> OnEntityMoved;

        public Action OnJoinFight;
        public Action OnMonsterSpawn;

        internal void EventMapChanged() => OnMapLoaded?.Invoke();
        internal void EventEntitiesRefresh() => OnEntitiesRefresh?.Invoke();
        internal void EventFightLaunchedInMap(int playerId, int fightId) => OnFightLaunchedInMap?.Invoke(playerId, fightId);

        private bool m_disposed = false;


        public MapManager()
        {
            //MapData = new Models.MapData();
            MapEntities = new ConcurrentDictionary<int, Entities.IEntity>();
            //MapData.InteractiveMapObjects = new ConcurrentDictionary<int, Map.InteractiveMapObject>();
        }

        internal void LoadMap(string packet) /*=> Task.Factory.StartNew(async () =>*/
        {
            //MapEntities.Clear();
            //MapData.InteractiveMapObjects.Clear();
            MapEntities.Clear();
            MapData = MapParsing.ParseAsync(packet).Result;
            //MapData.InteractiveMapObjects = new ConcurrentDictionary<int, Map.InteractiveMapObject>();
        }/*);*/

        public static Dtwo.API.Retro.Data.Map GetMap(string map)
        {
            if (map.Contains("[") && map.Contains("]") && map.Contains(","))
            {
                map = map.Replace(" ", "").Replace("[", "").Replace("]", "");
                string[] split = map.Split(',');
                if (split == null || split.Length != 2) return null;
                int x = -1;
                int y = -1;
                if (!int.TryParse(split[0], out x) || !int.TryParse(split[1], out y)) return null;

                return Database.GetDataAsyncCustom<Dtwo.API.Retro.Data.Map>("*", "maps", new Dictionary<string, string>() { { "X", x.ToString() }, { "Y", y.ToString() } }).Result;
            }
            else
            {
                int parsed = -1;
                if (int.TryParse(map, out parsed))
                {
                    return Database.GetMapAsync(parsed).Result;
                }
                else
                {
                    return null;
                }
            }
        }

        //public void JoinFight(int id)
        //{
        //    Core.Console.WriteLine("JoinFight " + m_gameManager.CharacterPlayerManager.Name);
        //    OnJoinFight?.Invoke();
        //    Core.Console.WriteLine("JoinFight :  GA903" + id + ";" + id);
        //    m_gameManager.TcpClient.SendPacket("GA903" + id + ";" + id);
        //}

        public void Dispose() => Dispose(true);
        ~MapManager() => Dispose(false);

        public void Clear()
        {
            MapEntities?.Clear();
            MapData?.Clear();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed)
                return;

            Clear();
            MapEntities = null;
            MapData = null;

            m_disposed = true;
        }
    }
}
