using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using Solucao.Application.Data.Entities;
using Solucao.Application.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Solucao.Application.Data.Repositories
{
    public class StickyNoteRepository : IStickyNoteRepository
    {
        public IUnitOfWork UnitOfWork => Db;
        protected readonly SolucaoContext Db;
        protected readonly DbSet<StickyNote> DbSet;

        public StickyNoteRepository(SolucaoContext _context)
        {
            Db = _context;
            DbSet = Db.Set<StickyNote>();
        }

        public async Task<ValidationResult> Add(StickyNote stickyNote)
        {
            try
            {

                Db.StickyNotes.Add(stickyNote);
                await Db.SaveChangesAsync();
                return ValidationResult.Success;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<IEnumerable<StickyNote>> GetAll(DateTime date)
        {

            return await Db.StickyNotes.Include(x => x.User).Where(x => x.Active && x.Date.Date == date).OrderBy(x => x.CreatedAt).ToListAsync();
        }

        public async Task<ValidationResult> Update(StickyNote stickyNote)
        {
            try
            {
                DbSet.Update(stickyNote);
                await Db.SaveChangesAsync();
                return ValidationResult.Success;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<StickyNote> GetById(Guid id)
        {
            return await Db.StickyNotes.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
