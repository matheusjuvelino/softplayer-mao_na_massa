using CalcularJuros.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace TaxaDeJuros.UnitTests
{
    public class CalcularJurosControllerTest
    {
        private readonly Mock<ILogger<CalcularJurosController>> _loggerMock;

        public CalcularJurosControllerTest()
        {
            _loggerMock = new Mock<ILogger<CalcularJurosController>>();
        }

        [Fact]
        public void Obter_taxa_de_juros_com_sucesso()
        {
            var jurosCalculadoExperado = "105,10";

            var calcularJurosController = new CalcularJurosController(_loggerMock.Object);

            var actionResult = calcularJurosController.Get("100", "5");


            Assert.IsType<ActionResult<string>>(actionResult);
            var jurosCalculado = Assert.IsAssignableFrom<string>(actionResult.Value);
            Assert.Equal(jurosCalculadoExperado, jurosCalculado);
        }

        [Fact]
        public void Obter_taxa_de_juros_quando_valor_inicial_nao_informado_erro()
        {
            var calcularJurosController = new CalcularJurosController(_loggerMock.Object);

            var actionResult = calcularJurosController.Get(null, "1");

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequestObjectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult.Result);
            Assert.NotNull(badRequestObjectResult.Value);
        }

        [Fact]
        public void Obter_taxa_de_juros_quando_meses_nao_informado_erro()
        {
            var calcularJurosController = new CalcularJurosController(_loggerMock.Object);

            var actionResult = calcularJurosController.Get("548", null);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequestObjectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult.Result);
            Assert.NotNull(badRequestObjectResult.Value);
        }

        [Fact]
        public void Obter_taxa_de_juros_quando_valor_inicial_nao_for_valido_erro()
        {
            var calcularJurosController = new CalcularJurosController(_loggerMock.Object);

            var actionResult = calcularJurosController.Get("asdf", "10");

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequestObjectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult.Result);
            Assert.NotNull(badRequestObjectResult.Value);
        }

        [Fact]
        public void Obter_taxa_de_juros_quando_meses_nao_for_valido_erro()
        {
            var calcularJurosController = new CalcularJurosController(_loggerMock.Object);

            var actionResult = calcularJurosController.Get("548.65", "asdf");

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequestObjectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult.Result);
            Assert.NotNull(badRequestObjectResult.Value);
        }
    }
}
