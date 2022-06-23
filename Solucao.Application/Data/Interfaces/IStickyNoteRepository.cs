using Solucao.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Solucao.Application.Data.Interfaces
{
    public interface IStickyNoteRepository
    {
        Task<IEnumerable<StickyNote>> GetAll(DateTime date);
        Task<ValidationResult> Add(StickyNote stickyNote);
        Task<ValidationResult> Update(StickyNote stickyNote);
        Task<StickyNote> GetById(Guid id);
    }
}
