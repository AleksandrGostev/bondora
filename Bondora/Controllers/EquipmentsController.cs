using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bondora.Dtos;
using Bondora.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Bondora.Controllers
{
    [Route("api/equipments")]
    public class EquipmentsController : Controller
    {
	    private readonly IEquipmentsRepository _equipmentsRepository;
	    private readonly IMemoryCache _memoryCache;
	    private const string EQUIPMENTS_KEY = "AllEquipments";

	    public EquipmentsController(IEquipmentsRepository equipmentsRepository, IMemoryCache memoryCache)
	    {
		    _equipmentsRepository = equipmentsRepository;
		    _memoryCache = memoryCache;
	    }

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var equipmentsFromRepo = await _memoryCache.GetOrCreate(EQUIPMENTS_KEY, cacheEntry => _equipmentsRepository.GetEquipments());

			var equipments = Mapper.Map<IEnumerable<Equipment>>(equipmentsFromRepo).ToList();

			return Ok(equipments);
		}
	}
}