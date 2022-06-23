using AutoMapper;
using Solucao.Application.Contracts;
using Solucao.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.AutoMapper
{
    public class EntityToViewModelMappingProfile : Profile
    {
        public EntityToViewModelMappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<Person, PersonViewModel>();
            CreateMap<Client, ClientViewModel>();
            CreateMap<Specification, SpecificationViewModel>();
            CreateMap<Equipament, EquipamentViewModel>();
            CreateMap<Calendar, CalendarViewModel>();
            CreateMap<StickyNote, StickyNoteViewModel>();
        }
    }
}
