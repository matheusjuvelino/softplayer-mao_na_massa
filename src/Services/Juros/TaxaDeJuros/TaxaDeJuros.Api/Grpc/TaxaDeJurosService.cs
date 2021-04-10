using Microsoft.Extensions.Logging;

namespace GrpcTaxaDeJuros
{
    public class TaxaDeJurosService : TaxaDeJuros.TaxaDeJurosBase
    {
        private readonly ILogger<TaxaDeJurosService> _logger;

        public TaxaDeJurosService(ILogger<TaxaDeJurosService> logger)
        {
            _logger = logger;
        }
    }
}
