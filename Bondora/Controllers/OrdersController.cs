using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bondora.Dtos;
using Bondora.Entities;
using Bondora.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using Equipment = Bondora.Dtos.Equipment;

namespace Bondora.Controllers
{
	[Route("api/orders")]
    public class OrdersController : Controller
	{
		private readonly IRentalService _rentalService;
		private readonly IOrdersRepository _ordersRepository;
		private readonly IMemoryCache _memoryCache;

	    public OrdersController(IRentalService rentalService, IOrdersRepository ordersRepository, IMemoryCache memoryCache)
	    {
		    _rentalService = rentalService;
		    _ordersRepository = ordersRepository;
		    _memoryCache = memoryCache;
	    }

		[HttpGet("{orderId}", Name = "GetOrder")]
		public async Task<IActionResult> GetOrder(Guid orderId)
		{
			var orderFromRepo = await _ordersRepository.GetOrder(orderId);

			if (orderFromRepo == null)
			{
				return BadRequest();
			}

			var orderToReturn = Mapper.Map<Dtos.Order>(orderFromRepo);

			return Ok(orderToReturn);
		}

		[HttpGet]
		public async Task<IActionResult> GetOrders()
		{
			var ordersFromRepo = await _ordersRepository.GetOrders();

			var orders = Mapper.Map<IEnumerable<Dtos.Order>>(ordersFromRepo);

			return Ok(orders);
		}

		[HttpPost]
	    public async Task<IActionResult> SubmitRequest([FromBody] OrderForCreation order)
		{
			if (order?.Equipments == null || !order.Equipments.Any(e => e.Days > 0))
			{
				return BadRequest();
			}

			var rentals = _rentalService.CreateRentals(order);

			var newOrder = new Entities.Order()
			{
				Rentals = rentals.ToList()
			};

			await _ordersRepository.AddOrder(newOrder);

			if (!await _ordersRepository.SaveAsync())
			{
				var err = new Exception("Adding new order failed on save.");
				Log.Error(err, err.Message);
				throw err;
			}

			return CreatedAtRoute("GetOrder", new { orderId = newOrder.OrderId }, newOrder.OrderId);
		}

		[HttpGet("download/{orderId}", Name = "Download")]
		public async Task<IActionResult> Download(Guid orderId)
		{
			var key = "Invoice." + orderId;
			var invoice = _memoryCache.GetOrCreate(key, cacheEntry =>
			{
				var order = _ordersRepository.GetOrder(orderId);
				if (order == null)
				{
					var err = new Exception("Order Not Found");
					Log.Error(err, err.Message);
					throw err;
				}

				var sb = new StringBuilder();
				sb.AppendLine($"Order #{orderId}\r\n");

				foreach (var rental in order.Result.Rentals)
				{
					sb.AppendLine($"Title: {rental.Equipment.Title}\t\t Price: {rental.TotalPrice}\t\tCollected bonus: {rental.Bonus}");
				}

				sb.AppendLine($"\r\nTotal price: {order.Result.TotalPrice} \t\t Total bonus: {order.Result.TotalBonus}");

				return sb.ToString();
			});
			
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(invoice);
			writer.Flush();
			stream.Position = 0;

			var response = File(stream, "application/octet-stream");
			return response;
		}
	}
}
