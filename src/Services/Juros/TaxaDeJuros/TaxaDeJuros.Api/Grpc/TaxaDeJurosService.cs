using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using TaxaDeJuros.Api;

namespace GrpcTaxaDeJuros
{
    public class TaxaDeJurosService : TaxaDeJuros.TaxaDeJurosBase
    {
        private readonly TaxaDeJurosSettings _settings;
        private readonly ILogger<TaxaDeJurosService> _logger;

        public TaxaDeJurosService(IOptionsSnapshot<TaxaDeJurosSettings> settings, ILogger<TaxaDeJurosService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        [AllowAnonymous]
        public override Task<TaxaDeJurosResponse> Obter(ObterTaxaDeJuros request, ServerCallContext context)
        {
            if (string.IsNullOrWhiteSpace(_settings.TaxaBaseDeJuros))
            {
                context.Status = new Status(StatusCode.NotFound, "Não foi informado o valor base para taxa de juros.");
                return null;
            }

            context.Status = new Status(StatusCode.OK, "Taxa base de juros obtida com sucesso");
            return Task.FromResult(new TaxaDeJurosResponse { Valor = _settings.TaxaBaseDeJuros });
        }
    }
}
