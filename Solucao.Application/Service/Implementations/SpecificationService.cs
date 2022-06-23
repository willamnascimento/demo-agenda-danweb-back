using AutoMapper;
using Solucao.Application.Contracts;
using Solucao.Application.Data.Entities;
using Solucao.Application.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Service.Interfaces
{
    public class SpecificationService : ISpecificationService
    {
        private SpecificationRepository specificationRepository;
        private readonly IMapper mapper;
        public SpecificationService(SpecificationRepository _specificationRepository, IMapper _mapper)
        {
            specificationRepository = _specificationRepository;
            mapper = _mapper;
        }

        public async Task<ValidationResult> Add(SpecificationViewModel specification)
        {
            bool retorno = false;

            if (specification.Single)
                retorno = await specificationRepository.SingleIsValid(null);

            if (!retorno)
            {
                specification.Id = Guid.NewGuid();
                specification.CreatedAt = DateTime.Now;
                var _specification = mapper.Map<Specification>(specification);
                return await specificationRepository.Add(_specification);
            }

            return new ValidationResult("Já existe uma especificação única");
            
        }

        public async Task<IEnumerable<SpecificationViewModel>> GetAll()
        {
            return mapper.Map<IEnumerable<SpecificationViewModel>>(await specificationRepository.GetAll());
        }

        public async Task<ValidationResult> Update(SpecificationViewModel specification)
        {
            var retorno = await specificationRepository.SingleIsValid(specification.Id);

            if (!retorno)
            {
                var _specification = mapper.Map<Specification>(specification);
                _specification.UpdatedAt = DateTime.Now;
                return await specificationRepository.Update(_specification);
            }

            return new ValidationResult("Já existe uma especificação única");
        }
    }
}
