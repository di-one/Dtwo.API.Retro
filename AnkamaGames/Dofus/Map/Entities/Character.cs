using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.DofusBehavior.Map.Entities
{
    public class Character : IEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public byte Sex { get; set; } = 0;
        public Cell Cell { get; set; }

        private bool m_disposed;

        public Character(int id, string name, byte sex, Cell cell)
        {
            Id = id;
            Name = name;
            Sex = sex;
            Cell = cell;
        }

        #region Dispose
        ~Character() => Dispose(false);
        public void Dispose() => Dispose(true);

        public virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                Cell = null;
                m_disposed = true;
            }
        }
        #endregion
    }
}
