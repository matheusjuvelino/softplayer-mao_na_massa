
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;

namespace CalcularJuros.FunctionalTests
{
    public class CalcularJurosScenariosBase
    {
        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(CalcularJurosScenariosBase))
              .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables();
                })
                .UseStartup<CalcularJurosTestsStartup>();


            var testServer = new TestServer(hostBuilder);

            return testServer;
        }

        public static class Get
        {
            public static string CalculaJuros(string valorInicial, string meses)
            {
                return $"api/v1/calculajuros?valorinicial={valorInicial}&meses={meses}";
            }
        }
    }
}
