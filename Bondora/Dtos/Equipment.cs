using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bondora.Enums;

namespace Bondora.Dtos
{
    public class Equipment
    {
		public Guid EquipmentId { get; set; }
		public string Title { get; set; }
		public string Type { get; set; }
		public int Days { get; set; }
    }
}