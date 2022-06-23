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
    public class PersonRepository : IPersonRepository
    {
        public IUnitOfWork UnitOfWork => Db;
        protected readonly SolucaoContext Db;
        protected readonly DbSet<Person> DbSet;

        public PersonRepository(SolucaoContext _context)
        {
            Db = _context;
            DbSet = Db.Set<Person>();
        }

        public async Task<IEnumerable<Person>> GetAll(bool ativo, string tipo_pessoa)
        {
            if (String.IsNullOrEmpty(tipo_pessoa))
                return await Db.People.Where(x => x.Active == ativo).OrderBy(x => x.PersonType).OrderBy(x => x.Name).ToListAsync();
            return await Db.People.Where(x => x.Active == ativo && x.PersonType == tipo_pessoa).OrderBy(x => x.PersonType).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetByName(string tipo_pessoa, string nome)
        {   
            return await Db.People.Where(x => x.Active && x.PersonType == tipo_pessoa && x.Name.Contains(nome)).ToListAsync();
        }

        public async Task<ValidationResult> Add(Person person)
        {
            try
            {
                
                Db.People.Add(person);
                await Db.SaveChangesAsync();
                return ValidationResult.Success;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<ValidationResult> Update(Person person)
        {
            try
            {
                DbSet.Update(person);
                await Db.SaveChangesAsync();
                return ValidationResult.Success;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }

        }

    }   
}
