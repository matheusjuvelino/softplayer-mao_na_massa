
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace FunctionalTests.Services.CalcularJuros
{
    public class CalcularJurosScenariosBase
    {
        private const string ApiUrlBase = "api/v1";


        public static TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(CalcularJurosScenariosBase))
               .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("Services/CalcularJuros/appsettings.json", optional: false)
                    .AddEnvironmentVariables();
                })
                .UseStartup<CalcularJurosTestsStartup>()
                .UseUrls("http://localhost:5005");

            return new TestServer(hostBuilder);
        }

        public static class Get
        {
            public static string CalculaJuros(string valorInicial, string meses)
            {
                return $"{ApiUrlBase}/calculajuros?valorinicial={valorInicial}&meses={meses}";
            }
        }
    }
}
