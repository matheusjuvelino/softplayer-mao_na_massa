using Autofac;
using Autofac.Extensions.DependencyInjection;
using CalcularJuros.Api;
using CalcularJuros.Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CalcularJuros.FunctionalTests
{
    public class CalcularJurosTestsStartup : Startup
    {
        public CalcularJurosTestsStartup(IConfiguration env) : base(env)
        {
        }

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddSingleton<ITaxaDeJurosService>(sp => new TaxaDeJurosServiceFake());

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }
    }

    public class TaxaDeJurosServiceFake : ITaxaDeJurosService
    {
        public Task<decimal> ObterTaxaDeJuros() => Task.FromResult(0.01m);
    }
}
