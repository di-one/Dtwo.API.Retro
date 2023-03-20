using D_One.Core.DofusBehavior.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.DofusBehavior.Map.Entities
{
    public interface IEntity : IDisposable
    {
        int Id { get; set; }
        Cell Cell { get; set; }
    }
}
