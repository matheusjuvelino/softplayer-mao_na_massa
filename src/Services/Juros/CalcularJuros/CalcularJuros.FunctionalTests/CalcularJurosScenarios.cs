using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CalcularJuros.FunctionalTests
{
    public class CalcularJurosScenarios : CalcularJurosScenariosBase
    {

        [Fact]
        public async Task Obter_taxa_de_juros_response_ok_status_code()
        {
            using var server = CreateServer();
            var response = await server.CreateClient()
                .GetAsync(Get.CalculaJuros("100", "5"));

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Obter_taxa_de_juros_response_bad_request_status_code()
        {
            using var server = CreateServer();
            var response = await server.CreateClient()
                .GetAsync(Get.CalculaJuros(null, null));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
