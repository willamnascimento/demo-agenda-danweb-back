using System;

namespace Solucao.Application.Data.Entities
{
    public class StickyNote : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Notes { get; set; }
        public bool Resolved { get; set; }
        public DateTime Date { get; set; }
        public virtual User User { get; set; }
    }
}
