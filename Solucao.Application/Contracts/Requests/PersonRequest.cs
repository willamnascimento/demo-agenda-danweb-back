using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Contracts.Requests
{
    public class PersonRequest
    {
        public bool Ativo { get; set; }
        public string TipoPessoa { get; set; }
        public string Nome { get; set; }
    }
}
