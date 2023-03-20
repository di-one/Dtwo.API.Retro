using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.DofusBehavior.Map.Entities
{
    public class Monster : IEntity
    {
        public int Id { get; set; } = 0;
        public int DataId { get; set; } = 0;
        public Cell Cell { get; set; }
        public int Level { get; set; }

        public List<Monster> MonstersInGroup { get; set; }
        public Monster LeaderGroup { get; set; }

        public int GroupMonsterCount => MonstersInGroup.Count + 1;
        public int GroupLevel => LeaderGroup.Level + MonstersInGroup.Sum(f => f.Level);

        private bool m_disposed;

        public string GetGroupString()
        {
            //string str = "";
            //Dictionary<Monster, int> monsters = new Dictionary<Monster, int>(); //nb, monster
            //List<Monster> allMonsters = new List<Monster>(MonstersInGroup);
            //allMonsters.Add(LeaderGroup);
            //for (int i = 0; i < allMonsters.Count; i++)
            //{
            //    if (monsters != null && monsters.Count > 0
            //        && monsters.FirstOrDefault(x => x.Key.DataId == allMonsters.ElementAt(i).DataId && x.Key.Level == allMonsters.ElementAt(i).Level).Key != null)
            //    {
            //        var kvp = monsters.FirstOrDefault(x => x.Key.DataId == allMonsters.ElementAt(i).DataId && x.Key.Level == allMonsters.ElementAt(i).Level);
            //        int nb = kvp.Value;
            //        Monster m = kvp.Key;
            //        monsters.Remove(m);
            //        monsters.Add(m, nb + 1);
            //    }
            //    else
            //    {
            //        monsters.Add(allMonsters.ElementAt(i), 1);
            //    }
            //}


            //for (int i = 0; i < monsters.Count; i++)
            //{
            //    var kvp = monsters.ElementAt(i);
            //    var data = SqlDatabase.Database.GetMonsterAsync(kvp.Key.DataId).Result;
            //    if (data != null)
            //    {
            //        str += $"x{kvp.Value} {data.Name} lvl.{kvp.Key.Level}";
            //        if (i != monsters.Count - 1) str += ", ";
            //    }
            //}

            //str += " Lvl. Total : " + GroupLevel;

            //return str;

            return null;
        }

        public Monster(int id, int dataId, Cell cell, int level)
        {
            Console.WriteLine("ADD MONSTER_______________________________");
            Console.WriteLine("Id : " + id);
            Console.WriteLine("data id : " + dataId);
            Id = id;
            DataId = dataId;
            Cell = cell;
            MonstersInGroup = new List<Monster>();
            Level = level;
        }

        /// <summary>
        /// Get if group contains a monster
        /// </summary>
        public bool GroupContains(int id)
        {
            Console.WriteLine("CHECK MONSTER ID MAP");

            if (LeaderGroup.DataId == id)
                return true;

            Console.WriteLine("CHECK MONSTER ID MAP : " + LeaderGroup.DataId);

            for (int i = 0; i < MonstersInGroup.Count; i++)
            {
                Console.WriteLine("CHECK MONSTER ID MAP : " + MonstersInGroup[i].DataId);
                if (MonstersInGroup[i].DataId == id)
                    return true;
            }
            return false;
        }

        public void Dispose() => Dispose(true);
        ~Monster() => Dispose(false);

        public virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                MonstersInGroup?.Clear();
                MonstersInGroup = null;
                m_disposed = true;
            }
        }
    }
}
