using Solucao.Application.Contracts;
using Solucao.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Data.Interfaces
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAll(bool ativo, string tipo_pessoa);
        Task<ValidationResult> Add(Person person);
        Task<ValidationResult> Update(Person person);
        Task<IEnumerable<Person>> GetByName(string tipo_pessoa, string nome);


    }
}
