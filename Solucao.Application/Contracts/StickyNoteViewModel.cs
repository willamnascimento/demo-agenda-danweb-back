using Solucao.Application.Data.Entities;
using System;

namespace Solucao.Application.Contracts
{
    public class StickyNoteViewModel
    {
        public Guid? Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; }
        public Guid UserId { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public bool Resolved { get; set; }
        public virtual User User { get; set; }
    }
}
