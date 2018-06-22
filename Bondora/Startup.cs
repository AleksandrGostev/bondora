using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bondora.Context;
using Bondora.Enums;
using Bondora.Helpers;
using Bondora.Interfaces;
using Bondora.Repositories;
using Bondora.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bondora
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			Config.AppSettings = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddMemoryCache();

			// Configure CORS so the API allows requests from JavaScript.  
			// For demo purposes, all origins/headers/methods are allowed.  
			services.AddCors(options =>
			{
				options.AddPolicy("AllowAllOriginsHeadersAndMethods",
					builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
			});

			var connectionString = Configuration["ConnectionStrings:BondoraDb"];
			services.AddDbContext<BondoraContext>(o => o.UseSqlServer(connectionString));

			// register repositories
			services.AddScoped<IEquipmentsRepository, EquipmentsRepository>();
			services.AddScoped<IOrdersRepository, OrdersRepository>();

			// register services
			services.AddScoped<IRentalService, RentalService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			AutoMapper.Mapper.Initialize(config =>
			{
				config.CreateMap<EquipmentType, string>().ConvertUsing(src => src.ToString());
				config.CreateMap<Entities.Equipment, Dtos.Equipment>();
				config.CreateMap<Dtos.Equipment, Entities.Rental>();
				config.CreateMap<Entities.Order, Dtos.Order>()
				.ForMember(o => o.TotalBonus, options => options.Ignore())
				.ForMember(o => o.TotalPrice, options => options.Ignore());
				config.CreateMap<Entities.Rental, Dtos.Rental>();
			});

			// Enable CORS
			app.UseCors("AllowAllOriginsHeadersAndMethods");
			app.UseMvc();
		}
	}
}
