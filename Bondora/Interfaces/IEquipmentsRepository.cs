using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bondora.Entities;

namespace Bondora.Interfaces
{
    public interface IEquipmentsRepository
    {
	    Task<IEnumerable<Equipment>> GetEquipments();
		Task<bool> SaveAsync();
	}
}
