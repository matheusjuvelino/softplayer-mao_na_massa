using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace CalcularJuros.Api.Services
{
    public class TaxaDeJurosService : ITaxaDeJurosService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;

        public TaxaDeJurosService(HttpClient httpClient, IOptionsSnapshot<CalcularJurosSettings> settings)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            _remoteServiceBaseUrl = $"{settings.Value.TaxaDeJurosUrl}/taxaJuros";
        }

        public async Task<decimal> ObterTaxaDeJuros()
        {
            var response = await _httpClient.GetAsync($"{_remoteServiceBaseUrl}");

            if (!response.IsSuccessStatusCode)
                return 0;

            var result = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(result))
                return 0;

            return decimal.Parse(result, CultureInfo.GetCultureInfo("pt-BR"));
        }
    }
}
