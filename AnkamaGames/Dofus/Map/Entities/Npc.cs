using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_One.Core.DofusBehavior.Map.Entities
{
    public class Npc : IEntity
    {
        public int Id { get; set; }
        public int DataId { get; set; }
        public Cell Cell { get; set; }

        public short Question { get; internal set; }
        public List<short> Responses { get; internal set; }

        private bool m_disposed;

        public Npc(int id, int dataId, Cell cell)
        {
            Id = id;
            DataId = dataId;
            Cell = cell;
        }

        #region Dispose
        ~Npc() => Dispose(false);
        public void Dispose() => Dispose(true);

        public virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                Responses?.Clear();
                Responses = null;
                Cell = null;
                m_disposed = true;
            }
        }
        #endregion
    }
}
