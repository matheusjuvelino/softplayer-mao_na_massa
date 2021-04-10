using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace FunctionalTests.Services.TaxaDeJuros
{
    public class TaxaDeJurosScenariosBase
    {
        private const string ApiUrlBase = "api/v1";


        public static TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(TaxaDeJurosScenariosBase))
               .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("Services/TaxaDeJuros/appsettings.json", optional: false)
                    .AddEnvironmentVariables();
                })
                .UseStartup<TaxaDeJurosTestsStartup>()
                .UseUrls("http://localhost:5000");

            return new TestServer(hostBuilder);
        }

        public static class Get
        {
            public static string TaxaDeJuros()
            {
                return $"{ApiUrlBase}/taxaJuros";
            }
        }
    }
}
