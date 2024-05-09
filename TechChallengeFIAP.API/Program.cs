using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Infrastracture.Data;

var builder = WebApplication.CreateBuilder(args);

//var connectionString = "Data Source=fiap.db";

//builder.Services.AddSqlite<FiapDbContext>(connectionString);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contatos API", Description = "Cadastro de Contatos", Version = "v1" });
});

var app = builder.Build();

//app.MapSwagger();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contatos API V1");
});


app.MapGet("/", () => "Hello World!");

app.MapGet("/v1/contatos", async (FiapDbContext context) =>  await context.Contatos.ToListAsync() ); //.Produces<Contato>();

app.MapPost("/v1/contatos", (FiapDbContext context, Contato contato) =>
{
    context.Database.EnsureCreated();
    context.Contatos.Add(contato);
    context.SaveChanges();
    return Results.Created($"/v1/contatos/{contato.Id}", contato);
});

app.Run();


