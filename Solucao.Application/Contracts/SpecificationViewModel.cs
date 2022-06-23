using System;

namespace Solucao.Application.Contracts
{
    public class SpecificationViewModel
    {
        public Guid? Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public bool Single { get; set; }
    }
}
