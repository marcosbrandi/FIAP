using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TechChallengeFIAP.API.Middleware;
using TechChallengeFIAP.Infrastracture.Data;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System;
using Prometheus;
using Prometheus.Client.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Contatos API",
        Description = "Cadastro de Contatos",
        Version = "v1"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.EnableAnnotations();
});

builder.Services.AddDbContext<FiapDbContext>(options => 
options.UseNpgsql(builder.Configuration.GetConnectionString("FiapDbContextConnection")));

//builder.Services.AddDbContext<FiapDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "fiap"));


builder.Services.AddHttpClient();

// declara interfaces
ServiceInterfaces.Add(builder.Services);

var app = builder.Build();

app.UsePrometheusServer();

app.UseHttpMetrics(options =>
{
    options.AddCustomLabel("host", context => context.Request.Host.Host);
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contatos API V1");
});

// Apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FiapDbContext>();
}

// declare endpoints
APIEndpoints.Configure(app);

// declare Prometheus endpoints
PrometheusEndpoints.Configure(app);

Counter _counter = Metrics.CreateCounter("TestMetricCounter", "will be incremented and published as metrics");
//Histogram _histogram = Metrics.CreateHistogram("TestMetricHistogram", "Will observe a value and publish it as Histogram");
//Gauge _gauge = Metrics.CreateGauge("TestMetricGauge", "Will observe a value and publish it as Gauge");
//Summary _summary = Metrics.CreateSummary("TestMetricSummary", "Will observe a value and publish it as Summary");
app.UseMetricServer();

var counter = Metrics.CreateCounter("TestMetricCounter", "Counts requests to endpoints",
    new CounterConfiguration
    {
        LabelNames = new[] { "method", "endpoint" }
    });

app.Use((context, next) =>
{
    counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
    return next();
});

app.Run();
