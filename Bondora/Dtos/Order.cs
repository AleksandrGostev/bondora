using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bondora.Entities;

namespace Bondora.Dtos
{
    public class Order
    {
	    public Guid OrderId { get; set; }

	    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();

		public decimal TotalPrice => Rentals.Sum(r => r.TotalPrice);

	    public int TotalBonus => Rentals.Sum(r => r.Bonus);
	}
}
