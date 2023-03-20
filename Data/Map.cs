using System.Data.Common;

namespace Dtwo.API.Retro.Data
{
    public class Map : DatabaseItem
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Data { get; set; }
        public string Teleports { get; set; }
        public int SubAreaId{ get; set; }

        public SubArea GetSubArea => Database.GetSubAreaAsync(SubAreaId).Result;

        public override void Init<T>(DbDataReader resultQuery)
        {
            base.Init<T>(resultQuery);

            if (resultQuery != null)
            {
                for (int i = 0; i < resultQuery.FieldCount; i++)
                {
                    switch (resultQuery.GetName(i)?.ToLower())
                    {
                        case "width":
                            Width = resultQuery[i].GetType() == typeof(DBNull) ? 0 : resultQuery.GetInt32(i);
                            break;
                        case "height":
                            Height = resultQuery[i].GetType() == typeof(DBNull) ? 0 : resultQuery.GetInt32(i);
                            break;
                        case "x":
                            X = resultQuery[i].GetType() == typeof(DBNull) ? 0 : resultQuery.GetInt32(i);
                            break;
                        case "y":
                            Y = resultQuery[i].GetType() == typeof(DBNull) ? 0 : resultQuery.GetInt32(i);
                            break;
                        case "data":
                            Data = resultQuery[i].GetType() == typeof(DBNull) ? null : resultQuery.GetString(i);
                            break;
                        case "teleports":
                            Teleports = resultQuery[i].GetType() == typeof(DBNull) ? null : resultQuery.GetString(i);
                            break;
                        case "subarea":
                            SubAreaId = resultQuery[i].GetType() == typeof(DBNull) ? 0 : resultQuery.GetInt32(i);
                            break;
                    }
                }
            }
        }
    }
}
