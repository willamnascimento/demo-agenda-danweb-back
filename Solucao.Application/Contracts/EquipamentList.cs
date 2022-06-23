using System;
using System.Collections.Generic;
using Solucao.Application.Data.Entities;

namespace Solucao.Application.Contracts
{
	public class EquipamentList
	{
        public Equipament Equipament { get; set; }
        public IEnumerable<Calendar> Calendars { get; set; }
	}
}

