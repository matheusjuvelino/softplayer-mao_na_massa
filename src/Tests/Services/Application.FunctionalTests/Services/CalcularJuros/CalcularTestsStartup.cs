
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CalcularJuros.Api;
using FunctionalTests.Services.TaxaDeJuros;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace FunctionalTests.Services.CalcularJuros
{
    class CalcularJurosTestsStartup : Startup
    {
        public CalcularJurosTestsStartup(IConfiguration env) : base(env)
        {
        }

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);


            services.AddSingleton(typeof(IHttpClientFactory), new TaxaDeJurosHttpClientFactory());

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }
    }

    class TaxaDeJurosHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            var taxaDeJurosServer = TaxaDeJurosScenariosBase.CreateServer();
            return taxaDeJurosServer.CreateClient();
        }
    }
}
