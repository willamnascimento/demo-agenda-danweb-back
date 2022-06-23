using Solucao.Application.Contracts;
using Solucao.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Service.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonViewModel>> GetAll(bool ativo, string tipo_pessoa);

        Task<IEnumerable<PersonViewModel>> GetByName(string tipo_pessoa, string nome);

        Task<ValidationResult> Add(PersonViewModel driver);

        Task<ValidationResult> Update(PersonViewModel driver);

    }
}
