using System.Data.Common;

namespace Dtwo.API.Retro.Data
{
    public class Monster : DatabaseItem
    {
        public string? Name { get; set; }
        public int GfxId { get; set; }
        public string? Grades { get; set; }
        public string? Stats { get; set; }
        public string? Spells { get; set; }
        public string? Pdvs { get; set; }
        public string? Points { get; set; }
        public string? Inits { get; set; }
        public string? MinKamas { get; set; }
        public int MaxKamas { get; set; }
        public string? Xps { get; set; }
        public int Capturable { get; set; }

        public override void Init<T>(DbDataReader resultQuery)
        {
            base.Init<Monster>(resultQuery);

            if (resultQuery != null)
            {
                for (int i = 0; i < resultQuery.FieldCount; i++)
                {
                    switch (resultQuery.GetName(i)?.ToLower())
                    {
                        case "name":
                            Name = resultQuery[i].GetType() == typeof(DBNull) ? "UNKNOW" : (string)resultQuery.GetValue(i);
                           break;

                        case "gfxid":
                            GfxId = resultQuery[i].GetType() == typeof(DBNull) ? 0 : (int)resultQuery.GetValue(i);
                            break;
                        case "grades":
                            Grades = resultQuery[i].GetType() == typeof(DBNull) ? null : (string)resultQuery.GetValue(i);
                            break;
                        case "stats":
                            Stats = resultQuery[i].GetType() == typeof(DBNull) ? null : (string)resultQuery.GetValue(i);
                            break;
                        case "spells":
                            Spells = resultQuery[i].GetType() == typeof(DBNull) ? null : (string)resultQuery.GetValue(i);
                            break;
                        case "pdvs":
                            Pdvs = resultQuery[i].GetType() == typeof(DBNull) ? null : (string)resultQuery.GetValue(i);
                            break;
                        case "points":
                            Points = resultQuery[i].GetType() == typeof(DBNull) ? null : (string)resultQuery.GetValue(i);
                            break;
                        case "inits":
                            Inits = resultQuery[i].GetType() == typeof(DBNull) ? null : (string)resultQuery.GetValue(i);
                            break;
                        case "minkamas":
                            MinKamas = resultQuery[i].GetType() == typeof(DBNull) ? null : (string)resultQuery.GetValue(i);
                            break;
                        case "maxkamas":
                            MaxKamas = resultQuery[i].GetType() == typeof(DBNull) ? 0 : (int)resultQuery.GetValue(i);
                            break;
                        case "xps":
                            Xps = resultQuery[i].GetType() == typeof(DBNull) ? null : (string)resultQuery.GetValue(i);
                            break;
                        case "capturable":
                            Capturable = resultQuery[i].GetType() == typeof(DBNull) ? 0 : (int)resultQuery.GetValue(i);
                            break;
                    }
                }
            }
        }
    }
}
