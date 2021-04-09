using Microsoft.Extensions.Logging;

namespace GrpcJuros
{
    public class JurosService : Juros.JurosBase
    {
        private readonly ILogger<JurosService> _logger;

        public JurosService(ILogger<JurosService> logger)
        {
            _logger = logger;
        }
    }
}
