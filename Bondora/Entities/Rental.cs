using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bondora.Entities
{
    public class Rental
    {
	    [Key]
	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid RentalId { get; set; }
		
		public Equipment Equipment { get; set; }
		public Guid EquipmentId { get; set; }

		public Order Order { get; set; }
		public Guid OrderId { get; set; }

	    public int Days { get; set; }

	    public decimal TotalPrice => Equipment.CalculatePrice(Days);

	    public int Bonus => Equipment.BonusRate;
    }
}
