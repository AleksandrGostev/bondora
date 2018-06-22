using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bondora.Dtos;
using Bondora.Entities;

namespace Bondora.Interfaces
{
    public interface IRentalService
    {
	    IEnumerable<Entities.Rental> CreateRentals(OrderForCreation newOrder);
    }
}
