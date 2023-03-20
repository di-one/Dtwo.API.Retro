using System.Data.SQLite;
using System.Xml.Linq;
using D_One.Core.Models;

namespace Dtwo.API.Retro.Data
{
    public static class Database
    {
        private static readonly string DATA_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Retro");
        private static readonly string DB_PATH = Path.Combine(DATA_PATH, "db.db3");

        public static async Task<Map> GetMapAsync(int id, string select = null, string where = null) => await GetDataAsync<Map>("maps", id, select, where);
        public static async Task<Item> GetItemAsync(int id) => await GetDataAsync<Item>("items", id);
        public static async Task<Monster> GetMonsterAsync(int id) => await GetDataAsync<Monster>("monsters", id);
        public static async Task<Area> GetAreaAsync(int id, string select = null, string where = null) => await GetDataAsync<Area>("area", id, select, where);
        public static async Task<SubArea> GetSubAreaAsync(int id, string select = null, string where = null) => await GetDataAsync<SubArea>("subArea", id, select, where);

        public static async Task<List<SubArea>> GetSubAreaAsyncByAreaId(int areaId) => await GetDataListAsync<SubArea>("subArea", $"WHERE Area = {areaId}");

        public static void UpdateMapAsync(string column, string value, int id) => UpdateData("maps", column, value, id);

        public static bool IsLoaded;

        public static List<Job> Jobs { get; private set; } // Load at runtime
        //public static List<Spell> Spells { get; private set; } // Load at runtime
        public static List<InteractiveMapObject> InteractiveMapObjects { get; private set; } // Load at runtime
        public static List<RaceStatsCalcul> RaceStatsCalculs { get; private set; }
        public static List<Monster> Monsters { get; private set; }
        public static Dictionary<int, Craft> Crafts { get; private set; } //id, craft
        //private static List<Item> m_items = new List<Item>();

        public static InteractiveMapObject FindByGfx(this List<InteractiveMapObject> interactiveMapObjects, short gfxId)
            => interactiveMapObjects.Find(x => x.Gfxs.Contains(gfxId));

        public static InteractiveMapObject FindBySkill(this List<InteractiveMapObject> interactiveMapObjects, short skillId)
            => interactiveMapObjects.Find(x => x.Skills.Contains(skillId));

        public static async Task<bool> LoadAsync()
        {
            return await Task.FromResult<bool>(Load());
        }

        public static bool Load()
        {
            Jobs = null;
            //Spells = null;
            InteractiveMapObjects = null;
            IsLoaded = false;

            //Console.WriteLine("__DATABASE 1 : " + Directory.Exists(Manager.MAPS_PATH).ToString() + " " + Directory.Exists(Manager.ITEMS_PATH).ToString() + " " + File.Exists(Manager.JOBS_PATH).ToString() + " " + File.Exists(Manager.SPELLS_PATH).ToString() + " " + File.Exists(Manager.INTERACTIVES_PATH).ToString());

            if (/*Directory.Exists(Manager.MAPS_PATH) &&*/ /*Directory.Exists(Manager.ITEMS_PATH) &&*/ File.Exists(Path.Combine(DATA_PATH, "jobs.json"))
                && File.Exists(Path.Combine(DATA_PATH, "interactives.json")) && File.Exists(Path.Combine(DATA_PATH, "crafts.json")) /*&& File.Exists(Manager.MONSTERS_PATH)*/)
            {
                if ((Jobs = Json.JSonSerializer<List<Job>>.DeSerialize(File.ReadAllText(Path.Combine(DATA_PATH, "jobs.json")))) == null) // Load server runtime
                    return false;

                List<Craft> crafts = null;
                if ((crafts = Json.JSonSerializer<List<Craft>>.DeSerialize(File.ReadAllText(Path.Combine(DATA_PATH, "jobs.json")))) == null) // Load server runtime
                    return false;

                //if ((Monsters = JsonConvert.DeserializeObject<List<Monster>>(File.ReadAllText(Manager.MONSTERS_PATH))) == null) // Load server runtime
                //    return false;

                Crafts = new Dictionary<int, Craft>();
                for (int i = 0; i < crafts.Count; i++)
                {
                    Crafts.Add(crafts[i].Id, crafts[i]);
                }

                //Console.WriteLine("DATABASE 2");

                List<InteractiveMapObjectData> interactiveMapObjectsData; // Load server runtime
                if ((interactiveMapObjectsData = Json.JSonSerializer<List<InteractiveMapObjectData>>.DeSerialize(File.ReadAllText(Path.Combine(DATA_PATH, "interactives.json")))) == null)
                    return false;

                //Console.WriteLine("DATABASE 3");

                InteractiveMapObjects = new List<InteractiveMapObject>();
                for (int i = 0; i < interactiveMapObjectsData.Count; i++)
                    InteractiveMapObjects.Add(new InteractiveMapObject(interactiveMapObjectsData[i]));

                //if ((RaceStatsCalculs = Json.JSonSerializer<List<RaceStatsCalcul>>.DeSerialize(File.ReadAllText(Manager.RACE_STATS_CALCUL_PATH))) == null)
                //{
                //    return false;
                //}

                //Console.WriteLine("____ RACESTATSCOUNT " + RaceStatsCalculs.Count);

                //Spells = new List<Spell>(); // Load server runtime
                //XElement.Parse(File.ReadAllText(Manager.SPELLS_PATH)).Descendants("SPELL").ToList().ForEach(spellItem =>
                //{
                //    Spell spell = new Spell(short.Parse(spellItem.Attribute("ID").Value), spellItem.Element("NAME").Value);

                //    spellItem.Descendants("LEVEL").ToList().ForEach(stats =>
                //    {
                //        SpellStats spellStats = new SpellStats();

                //        spellStats.ActionPointCost = byte.Parse(stats.Attribute("COST_PA").Value);
                //        spellStats.MinDistance = byte.Parse(stats.Attribute("RANGE_MIN").Value);
                //        spellStats.MaxDistance = byte.Parse(stats.Attribute("RANGE_MAX").Value);

                //        spellStats.IsLineLaunched = bool.Parse(stats.Attribute("LAUNCH_INLINE").Value);
                //        spellStats.IsVisionLaunched = bool.Parse(stats.Attribute("VISION_LINE").Value);
                //        spellStats.IsEmptyCell = bool.Parse(stats.Attribute("EMPTY_CELL").Value);
                //        spellStats.IsModifiableDistance = bool.Parse(stats.Attribute("MODIFIABLE_DISTANCE").Value);

                //        spellStats.LaunchPerTurn = byte.Parse(stats.Attribute("LAUNCH_PER_TURN").Value);
                //        spellStats.LaunchPerTarget = byte.Parse(stats.Attribute("LAUNCH_PER_TARGET").Value);
                //        spellStats.Interval = byte.Parse(stats.Attribute("COOLDOWN").Value);

                //        stats.Descendants("EFFECT").ToList().ForEach(effect => spellStats.AddEffect(new SpellEffect(int.Parse(effect.Attribute("TYPE").Value), Range.Parse(effect.Attribute("RANGE").Value)), bool.Parse(effect.Attribute("IS_CRITIC").Value)));
                //        spell.AddStats(byte.Parse(stats.Attribute("LEVEL").Value), spellStats);
                //    });

                //    Spells.Add(spell);
                //});

                IsLoaded = true;
                return true;
            }

            //Console.WriteLine("DATABASE 4");
            return false;
        }


        public static async Task<int> GetCount(string table)
        {
            string cs = $"URI=file:{DB_PATH}";

            using (SQLiteConnection connection = new SQLiteConnection())
            {
                connection.ConnectionString = cs;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand($"SELECT COUNT(*) FROM {table};", connection))
                {
                    var rd = await cmd.ExecuteReaderAsync();
                    int count = 0;
                    while (await rd.ReadAsync())
                    {
                        count = rd.GetInt32(0);
                        break;
                    }

                    return count;
                }
            }
        }

        public static async Task<T> GetDataAsyncCustom<T>(string select, string table, Dictionary<string, string> whereNameValues) where T : DatabaseItem, new()
        {
            string cs = $"URI=file:{DB_PATH}";
            string where = "WHERE ";

            int count = 1;
            foreach (var v in whereNameValues)
            {
                where += $"{v.Key} = '{v.Value}'";
                if (count != whereNameValues.Count)
                    where += " AND ";
                count++;
            }

            using (SQLiteConnection connection = new SQLiteConnection())
            {
                connection.ConnectionString = cs;
                connection.Open();

                T item = null;
                try
                {
                    using (SQLiteCommand cmd = CreateSelectWhereCommand(table, where, connection, select))
                    {
                        using (SQLiteDataReader rd = cmd.ExecuteReader())
                        {
                            if (await rd.ReadAsync())
                            {
                                if (rd.FieldCount > 0)
                                {
                                    item = new T();
                                    item.Init<T>(rd);
                                }
                            }

                            return item;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("____ERROR SQLITE " + ex.Message);
                }
            }


            return null;
        }

        public static async Task<T> GetDataAsync<T>(string table, int id, string select = null, string where = null) where T : DatabaseItem, new()
        {
            string cs = $"URI=file:{DB_PATH}";
            where = where ?? $"WHERE Id = '{id}'";

            using (SQLiteConnection connection = new SQLiteConnection())
            {
                connection.ConnectionString = cs;
                connection.Open();

                T item = null;
                try
                {
                    using (SQLiteCommand cmd = CreateSelectWhereCommand(table, where, connection, select))
                    {
                        using (SQLiteDataReader rd = cmd.ExecuteReader())
                        {
                            if (await rd.ReadAsync())
                            {
                                if (rd.FieldCount > 0)
                                {
                                    item = new T();
                                    item.Init<T>(rd);
                                }
                            }

                            return item;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("____ERROR SQLITE " + ex.Message);
                }
            }


            return null;
        }

        public static async Task<List<T>> GetDataListAsync<T>(string table, string otherQuery = null) where T : DatabaseItem, new()
        {
            //if (m_connexion == null)
            //    await Connection();
            string cs = $"URI=file:{DB_PATH}";

            using (SQLiteConnection connection = new SQLiteConnection())
            {
                connection.ConnectionString = cs;
                connection.Open();
                using (SQLiteCommand cmd = CreateAllSelectCommand(table, connection, otherQuery))
                {
                    List<T> dataList = new List<T>();

                    var rd = await cmd.ExecuteReaderAsync();
                    while (await rd.ReadAsync())
                    {
                        T t = new T();
                        t.Init<T>(rd);
                        dataList.Add(t);
                    }

                    return dataList;
                }
            }
        }

        public static void UpdateData(string table, string column, string value, int id)
        {
            //Core.Console.WriteLine("UPDATE DATA ASYNC 1");
            string cs = $"URI=file:{DB_PATH}";

            using (SQLiteConnection connection = new SQLiteConnection())
            {
                connection.ConnectionString = cs;
                connection.Open();

                string query = $"UPDATE maps SET Teleports = '{value}' WHERE Id = '{id.ToString()}';";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    try
                    {
                        //cmd.CommandText = $"UPDATE maps SET Teleports = :value WHERE Id = :id;";
                        //cmd.Parameters.Add("value", DbType.String).Value = value;
                        //cmd.Parameters.Add("id", DbType.String).Value = id.ToString();

                        //Core.Console.WriteLine("UPDATE DATA ASYNC 2 - " + cmd.CommandText);

                        cmd.ExecuteNonQuery();

                        //Core.Console.WriteLine("UPDATE DATA ASYNC 3");
                    }
                    catch (Exception ex)
                    {
                        //Core.Console.WriteLine(ex.Message + " " + ex.StackTrace);
                    }
                }

                //Core.Console.WriteLine("UPDATE DATA ASYNC 4");
            }
            //Core.Console.WriteLine("UPDATE DATA ASYNC 5");
        }

        private static SQLiteCommand CreateSelectWhereCommand(string table, string where, SQLiteConnection connection, string select = null, string otherQuery = null)
        {
            string query = "";
            if (select != null)
            {
                query = $"SELECT {select} ";
            }
            else
            {
                query = "SELECT * ";
            }

            query += $"FROM {table} {where}";

            if (otherQuery != null) query += $" {otherQuery}";

            query += ";";

            return new SQLiteCommand(query, connection);
        }

        private static SQLiteCommand CreateAllSelectCommand(string table, SQLiteConnection connection, string otherQuery = null)
        {
            string query = $"SELECT * FROM {table} ";
            if (otherQuery == null) query += "WHERE 1";
            else query += otherQuery;

            query += ";";

            return new SQLiteCommand(query, connection);
        }

        //private static SQLiteCommand CreateSingleUpdateWhereCommand(string table, string set, string where)
        //{
        //    string query = $"UPDATE {table} SET {set} WHERE {where};";
        //    return new SQLiteCommand(query, m_connexion);
        //}
    }
}
