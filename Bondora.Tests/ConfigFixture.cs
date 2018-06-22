using Bondora.Helpers;
using Microsoft.Extensions.Configuration;

namespace Bondora.Tests
{
    public class ConfigFixture
    {
	    public ConfigFixture()
	    {
		    Config.AppSettings = new ConfigurationBuilder()
			    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			    .Build();
		}
    }
}
