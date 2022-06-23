using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Data.Entities
{
    public class EquipamentSpecifications
    {
        public int Id { get; set; }
        public Guid EquipamentId { get; set; }
        public Guid SpecificationId { get; set; }
        public bool Active { get; set; }

        [NotMapped]
        public string Name { get; set; }
        public Equipament Equipament { get; set; }
        public Specification Specification { get; set; }
    }
}
