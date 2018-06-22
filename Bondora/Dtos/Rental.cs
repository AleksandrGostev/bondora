using System;

namespace Bondora.Dtos
{
    public class Rental
    {
	    public Guid RentalId { get; set; }

	    public Equipment Equipment { get; set; }

		public int Days { get; set; }

	    public decimal TotalPrice { get; set; }

	    public int Bonus { get; set; }
	}
}
