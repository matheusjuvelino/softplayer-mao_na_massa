using CalcularJuros.Api;
using static CalcularJuros.Api.DoubleExtensions;
using Xunit;

namespace CalcularJuros.UnitTests
{
    public class JurosCompostosTest
    {

        [Theory]
        [InlineData(100, 5, 0.01, 105.10100501000001)]
        [InlineData(200, 10, 0.02, 243.79888399895145)]
        [InlineData(80, 2, 0.03, 84.872)]
        [InlineData(10000, 12, 0.02, 12682.417945625455)]
        public void Calcular_juros_compostos_com_sucesso(decimal valorInicial, int meses, decimal juros, double resultadoEsperado)
        {
            var resultado = JurosCompostos.Calcular(valorInicial, meses, juros);

            Assert.Equal(resultadoEsperado, resultado);
        }

        [Theory]
        [InlineData(105.10100501000001, "105,10")]
        [InlineData(243.79888399895145, "243,79")]
        [InlineData(84.872, "84,87")]
        [InlineData(12682.417945625455, "12682,41")]
        [InlineData(12682, "12682,00")]
        public void Formatar_juros_compostos_com_sucesso(double valor, string resultadoEsperado)
        {
            var resultado = valor.FormatarJurosCompostos();

            Assert.Equal(resultadoEsperado, resultado);
        }
    }
}
