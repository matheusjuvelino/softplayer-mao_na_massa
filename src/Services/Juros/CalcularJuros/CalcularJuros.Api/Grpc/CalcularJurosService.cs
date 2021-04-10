using Microsoft.Extensions.Logging;

namespace GrpcCalcularJuros
{
    public class CalcularJurosService : CalcularJuros.CalcularJurosBase
    {
        private readonly ILogger<CalcularJurosService> _logger;

        public CalcularJurosService(ILogger<CalcularJurosService> logger)
        {
            _logger = logger;
        }
    }
}
