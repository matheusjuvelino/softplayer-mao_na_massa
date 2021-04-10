using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using CalcularJuros.Api.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using CalcularJuros.Api.Infrastructure.Filters;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using GrpcCalcularJuros;
using CalcularJuros.Api.Services;
using Polly;
using Polly.Extensions.Http;
using System.Net.Http;
using CalcularJuros.Api.Infrastructure.Grpc;
using GrpcTaxaDeJuros;
using Microsoft.Extensions.Options;

namespace CalcularJuros.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddGrpc(options =>
                {
                    options.EnableDetailedErrors = true;
                }).Services
                .AddCustomMVC(Configuration)
                .AddCustomOptions(Configuration)
                .AddSwagger(Configuration)
                .AddCustomHealthCheck(Configuration);

            if (Configuration.GetValue<bool>("USE_GRPC"))
                services.AddGrpcServices();
            else
                services.AddApplicationServices();


            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }

            app.UseSwagger()
               .UseSwaggerUI(setup =>
               {
                   setup.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "CalcularJuros.Api V1");
                   setup.OAuthAppName("Calcular Juros Swagger UI");
               });

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapGet("/_proto/", async ctx =>
                {
                    ctx.Response.ContentType = "text/plain";
                    using var fs = new FileStream(Path.Combine(env.ContentRootPath, "Proto", "calcularjuros.proto"), FileMode.Open, FileAccess.Read);
                    using var sr = new StreamReader(fs);
                    while (!sr.EndOfStream)
                    {
                        var line = await sr.ReadLineAsync();
                        if (line != "/* >>" || line != "<< */")
                        {
                            await ctx.Response.WriteAsync(line);
                        }
                    }
                });
                endpoints.MapGrpcService<CalcularJurosService>();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });
        }
    }

    public static class CustomExtensionMethods
    {

        public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.Filters.Add(typeof(ValidateModelStateFilter));

            })
            .AddApplicationPart(typeof(CalcularJurosController).Assembly)
            .AddNewtonsoftJson();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.Configure<CalcularJurosSettings>(configuration);

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services
                .AddHttpClient<ITaxaDeJurosService, TaxaDeJurosService>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            return services;
        }

        public static IServiceCollection AddGrpcServices(this IServiceCollection services)
        {
            services.AddTransient<GrpcExceptionInterceptor>();

            services.AddScoped<ITaxaDeJurosService, TaxaDeJurosServiceGrpc>();

            services.AddGrpcClient<TaxaDeJuros.TaxaDeJurosClient>((services, options) =>
            {
                var taxaDeJurosApi = services.GetRequiredService<IOptionsSnapshot<CalcularJurosSettings>>().Value.GrpcTaxaDeJuros;
                options.Address = new Uri(taxaDeJurosApi);
            }).AddInterceptor<GrpcExceptionInterceptor>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "softplayer m�o na massa - Calcular Juros HTTP API",
                    Version = "v1",
                    Description = "Servi�o para calcular Juros em HTTP API"
                });
            });

            return services;

        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddUrlGroup(new Uri(configuration["TaxaDeJurosUrlHC"]), name: "taxadejurosapi-check", tags: new string[] { "taxadejurosapi" });

            return services;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
           HttpPolicyExtensions
             .HandleTransientHttpError()
             .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
             .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }
}
