using Solucao.Application.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Service.Interfaces
{
    public interface ISpecificationService
    {
        Task<IEnumerable<SpecificationViewModel>> GetAll();

        Task<IEnumerable<SpecificationViewModel>> GetSpecificationByEquipament(List<Guid> equipamentIds);

        Task<ValidationResult> Add(SpecificationViewModel specification);

        Task<ValidationResult> Update(SpecificationViewModel specification);


    }
}
