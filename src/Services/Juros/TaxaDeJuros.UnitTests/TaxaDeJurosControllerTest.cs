using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TaxaDeJuros.Api;
using TaxaDeJuros.Api.Controllers;
using Xunit;

namespace TaxaDeJuros.UnitTests
{
    public class TaxaDeJurosControllerTest
    {
        private readonly Mock<ILogger<TaxaDeJurosController>> _loggerMock;

        public TaxaDeJurosControllerTest()
        {
            _loggerMock = new Mock<ILogger<TaxaDeJurosController>>();
        }

        [Fact]
        public void Obter_taxa_de_juros_com_sucesso()
        {
            var taxaDeJurosExperada = "0,01";
            var taxaDeJurosSettingsMock = ConfigurarTaxaDeJurosSettingsMock(taxaDeJurosExperada);

            var taxaDeJurosController = new TaxaDeJurosController(taxaDeJurosSettingsMock.Object, _loggerMock.Object);

            var actionResult = taxaDeJurosController.Get();


            Assert.IsType<ActionResult<string>>(actionResult);
            var taxaBaseDeJuros = Assert.IsAssignableFrom<string>(actionResult.Value);
            Assert.Equal(taxaDeJurosExperada, taxaBaseDeJuros);
        }

        [Fact]
        public void Obter_taxa_de_juros_com_erro()
        {
            var taxaDeJurosSettingsMock = ConfigurarTaxaDeJurosSettingsMock(null);

            var taxaDeJurosController = new TaxaDeJurosController(taxaDeJurosSettingsMock.Object, _loggerMock.Object);

            var actionResult = taxaDeJurosController.Get();

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequestObjectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult.Result);
            Assert.NotNull(badRequestObjectResult.Value);
        }

        private static Mock<IOptionsSnapshot<TaxaDeJurosSettings>> ConfigurarTaxaDeJurosSettingsMock(string value)
        {
            var taxaDeJurosSettingsMock = new Mock<IOptionsSnapshot<TaxaDeJurosSettings>>();
            taxaDeJurosSettingsMock.Setup(t => t.Value).Returns(new TaxaDeJurosSettings { TaxaBaseDeJuros = value });

            return taxaDeJurosSettingsMock;
        }
    }
}
