using System.Data.Common;

namespace Dtwo.API.Retro.Data
{
    public class Area : DatabaseItem
    {
        public string Name { get; private set; }

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
                            Name = resultQuery.GetString(i);
                            break;
                    }
                }
            }
        }
    }
}
