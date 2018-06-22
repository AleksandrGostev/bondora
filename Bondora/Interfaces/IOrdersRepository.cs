using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bondora.Entities;

namespace Bondora.Interfaces
{
    public interface IOrdersRepository
    {
	    Task AddOrder(Order order);
	    Task<IEnumerable<Order>> GetOrders();
	    Task<Order> GetOrder(Guid orderId);
	    Task<bool> SaveAsync();
	}
}
