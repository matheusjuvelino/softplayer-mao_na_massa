using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Threading.Tasks;

namespace TaxaDeJuros.Api.Controllers
{
    [Route("api/v1/taxaJuros")]
    [ApiController]
    public class TaxaDeJurosController : ControllerBase
    {
        private readonly TaxaDeJurosSettings _settings;
        private readonly ILogger<TaxaDeJurosController> _logger;

        public TaxaDeJurosController(IOptionsSnapshot<TaxaDeJurosSettings> settings, ILogger<TaxaDeJurosController> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        // GET api/v1/taxaJuros
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<string> Get()
        {
            if (string.IsNullOrWhiteSpace(_settings.TaxaBaseDeJuros))
            {
                return BadRequest("Não foi informado o valor base para taxa de juros.");
            }

            return _settings.TaxaBaseDeJuros;
        }
    }
}
