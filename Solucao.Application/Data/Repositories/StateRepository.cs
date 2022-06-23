using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using Solucao.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Data.Repositories
{
    public class StateRepository 
    {
        public IUnitOfWork UnitOfWork => Db;
        protected readonly SolucaoContext Db;
        protected readonly DbSet<State> DbSet;

        public StateRepository(SolucaoContext _context)
        {
            Db = _context;
            DbSet = Db.Set<State>();
        }

        public IEnumerable<State> GetAll()
        {
            return Db.States.ToList();
        }

        public async Task<ValidationResult> Add(State state)
        {
            try
            {

                Db.States.Add(state);
                await Db.SaveChangesAsync();
                return ValidationResult.Success;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




    }
}
