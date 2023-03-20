using System.Data.Common;

namespace Dtwo.API.Retro.Data
{
    public abstract class DatabaseItem
    {
        public int Id { get; set; }
        public virtual void Init<T>(DbDataReader resultQuery) where T : DatabaseItem, new()
        {
            if (resultQuery != null)
            {
                for (int i = 0; i < resultQuery.FieldCount; i++)
                {
                    switch (resultQuery.GetName(i).ToLower())
                    {
                        case "id":
                            Id = resultQuery.GetInt32(i);
                            break;
                    }

                }
            }
        }
    }
}
