using CalcularJuros.Api.Infrastructure.Format;
using System;
using System.Globalization;

namespace CalcularJuros.Api
{
    public class JurosCompostos
    {
        public static double Calcular(decimal valorInicial, int meses, decimal juros) =>
            (double)valorInicial * Math.Pow((double)(1 + juros), meses);
    }
    public static class DoubleExtensions
    {
        public static string FormatarJurosCompostos(this double valor) =>
            string.Format(new FormatProvider(CultureInfo.GetCultureInfo("pt-BR")), "{0:T.00}", valor);
    }
}
