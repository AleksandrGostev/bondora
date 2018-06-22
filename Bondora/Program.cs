using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bondora.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Bondora
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var host = BuildWebHost(args);
			Log.Logger = new LoggerConfiguration()
				.WriteTo.File("logger.log")
				.CreateLogger();

	        // migrate & seed the database.  Best practice = in Main, using service scope
	        using (var scope = host.Services.CreateScope())
	        {
		        try
		        {
			        var context = scope.ServiceProvider.GetService<BondoraContext>();
			        context.Database.Migrate();
			        context.EnsureSeedDataForContext();
		        }
		        catch (System.Exception ex)
		        {
			        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
			        logger.LogError(ex, "An error occurred with migrating or seeding the DB.");
		        }
	        }

	        // run the web app
	        host.Run();
		}

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
