using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Juros.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JurosController : ControllerBase
    {
        private readonly ILogger<JurosController> _logger;

        public JurosController(ILogger<JurosController> logger)
        {
            _logger = logger;
        }
    }
}
