using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<int>? Tools { get; set; }
        public List<CraftJob>? Crafts { get; set; }
        public List<JobResource>? Resources { get; set; }
    }

    public class CraftJob
    {
        public int InteractiveId { get; set; }
        public List<int>? itemsId { get; set; }
    }

    public class JobResource
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public int Xp { get; set; }
    }

}
