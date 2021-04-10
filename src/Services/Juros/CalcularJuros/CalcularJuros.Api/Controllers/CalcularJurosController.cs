using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CalcularJuros.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CalcularJurosController : ControllerBase
    {
        private readonly ILogger<CalcularJurosController> _logger;

        public CalcularJurosController(ILogger<CalcularJurosController> logger)
        {
            _logger = logger;
        }
    }
}
