using FunctionalTests.Services.CalcularJuros;
using System.Threading.Tasks;
using Xunit;

namespace Application.FunctionalTests
{
    public class IntegrationServicesScenarios
    {
        [Fact]
        public async Task Obter_calculo_de_juros()
        {
            var jurosCalculadoExperado = "105,10";

            using var calcularJurosServer = CalcularJurosScenariosBase.CreateServer();

            var calcularJurosClient = calcularJurosServer.CreateClient();

            var response = await calcularJurosClient.GetAsync(CalcularJurosScenariosBase.Get.CalculaJuros("100", "5"));

            var result = await response.Content.ReadAsStringAsync();

            Assert.NotNull(result);

            Assert.Equal(jurosCalculadoExperado, result);
        }
    }
}
