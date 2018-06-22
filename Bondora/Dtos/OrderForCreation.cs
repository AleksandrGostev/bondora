using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bondora.Dtos
{
    public class OrderForCreation
    {
		public IEnumerable<Equipment> Equipments { get; set; }
    }
}
