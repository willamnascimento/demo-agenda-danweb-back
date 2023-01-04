using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Contracts.Requests
{
    public class SpecificationRequest
    {
        public bool Ativo { get; set; }
        public string EquipamentList { get; set; }
    }
}
