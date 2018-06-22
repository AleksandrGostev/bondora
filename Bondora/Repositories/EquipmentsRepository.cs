using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bondora.Context;
using Bondora.Entities;
using Bondora.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bondora.Repositories
{
    public class EquipmentsRepository : IEquipmentsRepository
    {
	    private BondoraContext _context;

	    public EquipmentsRepository(BondoraContext context)
	    {
		    _context = context;
	    }

	    public async Task<IEnumerable<Equipment>> GetEquipments()
	    {
		    return await _context.Equipments.ToListAsync();
	    }

	    public async Task<bool> SaveAsync()
	    {
			return (await _context.SaveChangesAsync() >= 0);
		}
	}
}
