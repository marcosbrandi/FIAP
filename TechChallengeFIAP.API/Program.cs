using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TechChallengeFIAP.API.Middleware;
using TechChallengeFIAP.Infrastracture.Data;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { 
    Title = "Contatos API", Description = "Cadastro de Contatos", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
});


builder.Services.AddDbContext<FiapDbContext>( opt => opt.UseInMemoryDatabase(databaseName: "fiap") );

// declara interfaces
ServiceInterfaces.Add(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

app.UseSwagger();

// declara endpoints
AppEndpoints.Set(app);

app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contatos API V1"); });

app.Run();



