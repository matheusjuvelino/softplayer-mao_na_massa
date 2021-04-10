
using Microsoft.Extensions.Configuration;
using TaxaDeJuros.Api;

namespace FunctionalTests.Services.TaxaDeJuros
{
    class TaxaDeJurosTestsStartup : Startup
    {
        public TaxaDeJurosTestsStartup(IConfiguration env) : base(env)
        {
        }
    }
}
