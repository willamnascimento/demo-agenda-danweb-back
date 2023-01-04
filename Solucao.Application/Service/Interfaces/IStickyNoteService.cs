using Solucao.Application.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Service.Interfaces
{
    public interface IStickyNoteService
    {
        Task<IEnumerable<StickyNoteViewModel>> GetAll(DateTime date);
        Task<ValidationResult> Add(StickyNoteViewModel stickyNote);
        Task<ValidationResult> Update(StickyNoteViewModel stickyNote);
        Task<ValidationResult> UpdateResolved(Guid stickyNotesId);
        Task<ValidationResult> Remove(Guid stickyNotesId);
    }
}
