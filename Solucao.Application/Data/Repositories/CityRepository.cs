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
    public class CityRepository
    {
        public IUnitOfWork UnitOfWork => Db;
        protected readonly SolucaoContext Db;
        protected readonly DbSet<City> DbSet;

        public CityRepository(SolucaoContext _context)
        {
            Db = _context;
            DbSet = Db.Set<City>();
        }

        public async Task<ValidationResult> Add(List<City> list)
        {
            try
            {

                await Db.Cities.AddRangeAsync(list);
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
