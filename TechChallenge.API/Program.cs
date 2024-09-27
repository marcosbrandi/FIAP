
using Fiap.Clientes.API.Configuration;
using MediatR;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TechChallenge.API.Endpoints;
using Prometheus;
using Prometheus.Client.AspNetCore;
using Prometheus.Client.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TechChallengeFIAP.API.Middleware;

namespace TechChallenge.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services
                .AddHealthChecks()
                .AddUrlGroup(new Uri("https://google.com"), "google", HealthStatus.Unhealthy)
                .AddUrlGroup(new Uri("https://invalidurl"), "invalidurl", HealthStatus.Degraded);
            builder.Services.AddPrometheusHealthCheckPublisher();

            builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contatos API", Description = "Cadastro de Contatos", Version = "v1" }); });

            builder.Services.AddApiConfiguration(builder.Configuration);

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            builder.Services.RegisterServices();

            builder.Services.AddMessageBusConfiguration(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
            app.UseSwagger();

            //These metrics are called to use prometheus
            app.UseMetricServer();
            app.UseHttpMetrics();
            app.UsePrometheusServer();

            //ContatoEndpoints.Map(app);
            ContatoEndpoints.Map1(app);
            BuscarContatos.AddRoutes(app);
            PrometheusEndpoints.Configure(app);

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contatos API V1"); });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
