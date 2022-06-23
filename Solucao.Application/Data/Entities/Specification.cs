using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Data.Entities
{
    public class Specification : BaseEntity
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public bool Single { get; set; }
        public List<EquipamentSpecifications> EquipamentSpecifications { get; set; }
        public IList<CalendarSpecifications> CalendarSpecifications { get; set; }
    }
}
