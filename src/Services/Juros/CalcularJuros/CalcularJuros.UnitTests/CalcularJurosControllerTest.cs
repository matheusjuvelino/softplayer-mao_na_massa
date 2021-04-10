using CalcularJuros.Api.Controllers;
using CalcularJuros.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace TaxaDeJuros.UnitTests
{
    public class CalcularJurosControllerTest
    {
        private readonly Mock<ILogger<CalcularJurosController>> _loggerMock;
        private readonly Mock<ITaxaDeJurosService> _taxaDeJurosServiceMock;

        public CalcularJurosControllerTest()
        {
            _loggerMock = new Mock<ILogger<CalcularJurosController>>();

            _taxaDeJurosServiceMock = new Mock<ITaxaDeJurosService>();

            _taxaDeJurosServiceMock.Setup(t => t.ObterTaxaDeJuros()).Returns(Task.FromResult(0.01m));
        }

        [Fact]
        public async Task Obter_taxa_de_juros_com_sucessoAsync()
        {
            var jurosCalculadoExperado = "105,10";

            var calcularJurosController = new CalcularJurosController(_taxaDeJurosServiceMock.Object, _loggerMock.Object);

            var actionResult = await calcularJurosController.GetAsync("100", "5");

            Assert.IsType<ActionResult<string>>(actionResult);
            var jurosCalculado = Assert.IsAssignableFrom<string>(actionResult.Value);
            Assert.Equal(jurosCalculadoExperado, jurosCalculado);
        }

        [Fact]
        public async Task Obter_taxa_de_juros_quando_valor_inicial_nao_informado_erroAsync()
        {
            var calcularJurosController = new CalcularJurosController(_taxaDeJurosServiceMock.Object, _loggerMock.Object);

            var actionResult = await calcularJurosController.GetAsync(null, "1");

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequestObjectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult.Result);
            Assert.NotNull(badRequestObjectResult.Value);
        }

        [Fact]
        public async Task Obter_taxa_de_juros_quando_meses_nao_informado_erroAsync()
        {
            var calcularJurosController = new CalcularJurosController(_taxaDeJurosServiceMock.Object, _loggerMock.Object);

            var actionResult = await calcularJurosController.GetAsync("548", null);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequestObjectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult.Result);
            Assert.NotNull(badRequestObjectResult.Value);
        }

        [Fact]
        public async Task Obter_taxa_de_juros_quando_valor_inicial_nao_for_valido_erroAsync()
        {
            var calcularJurosController = new CalcularJurosController(_taxaDeJurosServiceMock.Object, _loggerMock.Object);

            var actionResult = await calcularJurosController.GetAsync("asdf", "10");

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequestObjectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult.Result);
            Assert.NotNull(badRequestObjectResult.Value);
        }

        [Fact]
        public async Task Obter_taxa_de_juros_quando_meses_nao_for_valido_erroAsync()
        {
            var calcularJurosController = new CalcularJurosController(_taxaDeJurosServiceMock.Object, _loggerMock.Object);

            var actionResult = await calcularJurosController.GetAsync("548.65", "asdf");

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequestObjectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult.Result);
            Assert.NotNull(badRequestObjectResult.Value);
        }
    }
}
