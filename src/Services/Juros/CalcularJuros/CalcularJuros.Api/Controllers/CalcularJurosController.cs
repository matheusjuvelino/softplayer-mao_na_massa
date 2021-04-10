using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Net;
using CalcularJuros.Api.Services;
using System;

namespace CalcularJuros.Api.Controllers
{
    [Route("api/v1/calculajuros")]
    [ApiController]
    public class CalcularJurosController : ControllerBase
    {
        private readonly ITaxaDeJurosService _taxaDeJurosService;
        private readonly ILogger<CalcularJurosController> _logger;

        public CalcularJurosController(ITaxaDeJurosService taxaDeJurosService, ILogger<CalcularJurosController> logger)
        {
            _taxaDeJurosService = taxaDeJurosService ?? throw new ArgumentNullException(nameof(taxaDeJurosService));
            _logger = logger;
        }

        // GET api/v1/taxaJuros
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<string>> GetAsync([FromQuery] string valorinicial, [FromQuery] string meses)
        {
            if (string.IsNullOrWhiteSpace(valorinicial))
            {
                return BadRequest("Não foi informado o valor inicial.");
            }

            if (!decimal.TryParse(valorinicial, out decimal valorInicial))
            {
                return BadRequest("O valor inicial informado não é está num formato válido. Ex.: 5.00, 5 ou 100.22 ...");
            }

            if (string.IsNullOrWhiteSpace(meses))
            {
                return BadRequest("Não foi informado a quantidade de meses.");
            }

            if (!int.TryParse(meses, out int totalMeses))
            {
                return BadRequest("A quantidade de meses informada não é está num formato válido. Ex.: 5, 1 ou 10 ...");
            }

            var taxaDeJuros = await _taxaDeJurosService.ObterTaxaDeJuros();

            return JurosCompostos.Calcular(valorInicial, totalMeses, taxaDeJuros).FormatarJurosCompostos();
        }
    }
}
