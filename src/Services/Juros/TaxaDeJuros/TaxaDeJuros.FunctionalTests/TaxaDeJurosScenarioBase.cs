
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;
using TaxaDeJuros.Api;

namespace TaxaDeJuros.FunctionalTests
{
    public class TaxaDeJurosScenariosBase
    {
        public TestServer CreateServer(string appsettings)
        {
            var path = Assembly.GetAssembly(typeof(TaxaDeJurosScenariosBase))
              .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile(appsettings, optional: false)
                    .AddEnvironmentVariables();
                })
                .UseStartup<Startup>();


            var testServer = new TestServer(hostBuilder);

            return testServer;
        }

        public static class Get
        {
            public static string TaxaDeJuros()
            {
                return "api/v1/taxaJuros";
            }
        }
    }
}
