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
    public class OrdersRepository : IOrdersRepository
    {
	    private readonly BondoraContext _context;

	    public OrdersRepository(BondoraContext context)
	    {
		    _context = context;
	    }

	    public async Task AddOrder(Order order)
	    {
		    await _context.Orders.AddAsync(order);
	    }

	    public async Task<IEnumerable<Order>> GetOrders()
		{
			return await _context.Orders.ToListAsync();
		}

	    public async Task<Order> GetOrder(Guid orderId)
	    {
		    return await _context.Orders
				.Include(o => o.Rentals)
				.ThenInclude(r => r.Equipment)
				.Where(o => o.OrderId == orderId).FirstOrDefaultAsync();
	    }

	    public async Task<bool> SaveAsync()
	    {
			return (await _context.SaveChangesAsync() >= 0);
		}
    }
}
