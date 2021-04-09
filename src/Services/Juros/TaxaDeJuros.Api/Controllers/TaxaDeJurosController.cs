using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TaxaDeJuros.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TaxaDeJurosController : ControllerBase
    {
        private readonly ILogger<TaxaDeJurosController> _logger;

        public TaxaDeJurosController(ILogger<TaxaDeJurosController> logger)
        {
            _logger = logger;
        }
    }
}
