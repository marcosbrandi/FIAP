using Swashbuckle.AspNetCore.Annotations;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using Prometheus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace TechChallengeFIAP.API.Middleware;

public static class PrometheusEndpoints
{
    public static void Configure(WebApplication app)
    {
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

        //app.UseHealthChecks("/hc", new HealthCheckOptions
        //{
        //    Predicate = _ => true
        //});

        /*
        app.MapPost("/IncrementCounter", (int inc) =>
        {
            _counter.Inc(inc);
            _counter.Publish();
        })
        .WithMetadata(new SwaggerOperationAttribute("IncrementCounter"));

        app.MapPost("/IncrementHistogram", (int inc) =>
        {
            _histogram.Observe(inc);
            _histogram.Publish();
        })
        .WithMetadata(new SwaggerOperationAttribute("IncrementHistogram"));

        app.MapPost("/IncrementGauge", (int inc) =>
        {
            _gauge.Inc(inc);
            _gauge.Publish();
        })
        .WithMetadata(new SwaggerOperationAttribute("IncrementGauge"));

        app.MapPost("/DecrementGauge", (int inc) =>
        {
            _gauge.Dec(inc);
            _gauge.Publish();
        })
        .WithMetadata(new SwaggerOperationAttribute("DecrementGauge"));

        app.MapPost("/IncrementSummary", (int inc) =>
        {
            _summary.Observe(inc);
            _summary.Publish();
        })
        .WithMetadata(new SwaggerOperationAttribute("IncrementSummary"));
        */
    }

}

