using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace TaxaDeJuros.FunctionalTests
{
    public class TaxaDeJurosScenarios : TaxaDeJurosScenariosBase
    {

        [Fact]
        public async Task Obter_taxa_de_juros_response_ok_status_code()
        {
            using (var server = CreateServer("appsettings.json"))
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.TaxaDeJuros());

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task Obter_taxa_de_juros_response_bad_request_status_code()
        {
            using (var server = CreateServer("appsettings.invalido.json"))
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.TaxaDeJuros());

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
    }
}
