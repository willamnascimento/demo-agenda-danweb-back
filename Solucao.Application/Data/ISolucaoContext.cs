using Microsoft.EntityFrameworkCore;
using Solucao.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Data
{
    public interface ISolucaoContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Person> People { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<State> States { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Specification> Specifications { get; set; }
        DbSet<Equipament> Equipaments { get; set; }
        DbSet<EquipamentSpecifications> EquipamentSpecifications { get; set; }
        DbSet<Calendar> Calendars { get; set; }
        DbSet<CalendarSpecifications> CalendarSpecifications { get; set; }
    }
}
