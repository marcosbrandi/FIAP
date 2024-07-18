using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TechChallengeFIAP.API.Middleware;
using TechChallengeFIAP.Infrastracture.Data;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Prometheus;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus.Client.HealthChecks;
using System;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { 
    Title = "Contatos API", Description = "Cadastro de Contatos", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.EnableAnnotations();
});

builder.Services.AddDbContext<FiapDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("FiapDbContextConnection");
    options.UseNpgsql(connectionString);
});

//builder.Services.AddDbContext<FiapDbContext>( opt => opt.UseInMemoryDatabase(databaseName: "fiap") );

builder.Services.AddHttpClient();


builder.Services
    .AddHealthChecks()
    .AddUrlGroup(new Uri("https://google.com"), "google", HealthStatus.Unhealthy)
    .AddUrlGroup(new Uri("https://invalidurl"), "invalidurl", HealthStatus.Degraded);
builder.Services.AddPrometheusHealthCheckPublisher();

// declare interfaces
ServiceInterfaces.Add(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

// setup Swagger
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contatos API V1"); });

//These metrics are called to use prometheus
app.UseMetricServer();
app.UseHttpMetrics();

// declare endpoints
APIEndpoints.Configure(app);

// declare Prometheus endpoints
PrometheusEndpoints.Configure(app);

app.Run();



