using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using GrpcTaxaDeJuros;
using System.Threading.Tasks;

namespace CalcularJuros.Api.Services
{
    public class TaxaDeJurosServiceGrpc : ITaxaDeJurosService
    {
        private readonly TaxaDeJuros.TaxaDeJurosClient _taxaDeJurosClient;
        private readonly string _remoteServiceBaseUrl;

        public TaxaDeJurosServiceGrpc(TaxaDeJuros.TaxaDeJurosClient taxaDeJurosClient, IOptionsSnapshot<CalcularJurosSettings> settings)
        {
            _taxaDeJurosClient = taxaDeJurosClient ?? throw new ArgumentNullException(nameof(taxaDeJurosClient));

            _remoteServiceBaseUrl = $"{settings.Value.TaxaDeJurosUrl}/taxaJuros";
        }

        public async Task<decimal> ObterTaxaDeJuros()
        {
            var responseTaxaDeJuros = await _taxaDeJurosClient.ObterAsync(new ObterTaxaDeJuros());

            return decimal.Parse(responseTaxaDeJuros.Valor, CultureInfo.GetCultureInfo("pt-BR"));
        }
    }
}
