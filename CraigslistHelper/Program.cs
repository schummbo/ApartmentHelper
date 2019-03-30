using CraigslistHelper.Core.Settings;
using Microsoft.Extensions.Configuration;

namespace CraigslistHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var settings = new Settings();
            config.Bind(settings);

            new CraigslistHelperRunner(settings).Run();
        }
    }
}
