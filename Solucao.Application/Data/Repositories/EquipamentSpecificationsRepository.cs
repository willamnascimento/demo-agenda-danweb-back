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
    public class EquipamentSpecificationsRepository
    {
        public IUnitOfWork UnitOfWork => Db;
        protected readonly SolucaoContext Db;
        protected readonly DbSet<EquipamentSpecifications> DbSet;

        public EquipamentSpecificationsRepository(SolucaoContext _context)
        {
            Db = _context;
            DbSet = Db.Set<EquipamentSpecifications>();
        }

        public async Task<ValidationResult> Add(List<EquipamentSpecifications> list)
        {
            try
            {

                await Db.EquipamentSpecifications.AddRangeAsync(list);
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
