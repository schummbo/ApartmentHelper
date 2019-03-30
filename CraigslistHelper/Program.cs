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

            new CraigslistHelperRunner(config).Run();
        }
    }
}
