using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CalcularJuros.Api.Controllers
{
    [Route("api/v1/showmethecode")]
    public class ShowMeTheCodeController
    {
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public ActionResult<string> Get()
        {
            return "https://github.com/matheusjuvelino/softplayer-mao_na_massa";
        }
    }
}
