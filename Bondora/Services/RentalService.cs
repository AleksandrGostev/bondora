using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bondora.Dtos;
using Bondora.Entities;
using Bondora.Interfaces;
using Rental = Bondora.Entities.Rental;

namespace Bondora.Services
{
    public class RentalService : IRentalService
    {
	    public IEnumerable<Rental> CreateRentals(OrderForCreation newOrder)
	    {
			var rentedEquipments = newOrder.Equipments.Where(x => x.Days > 0);
		    var rentals = Mapper.Map<IEnumerable<Dtos.Equipment>, IEnumerable<Rental>>(rentedEquipments);

		    return rentals;
	    }
    }
}
