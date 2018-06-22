using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bondora.Entities
{
    public class Order
    {
	    [Key]
	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid OrderId { get; set; }

		public ICollection<Rental> Rentals { get; set; } = new List<Rental>();

	    public decimal TotalPrice => Rentals.Sum(r => r.TotalPrice);

	    public int TotalBonus => Rentals.Sum(r => r.Bonus);
    }
}
