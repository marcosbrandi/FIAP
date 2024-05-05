using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TechChallengeFIAP.API.EndPoints;
using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastracture.Data;
using TechChallengeFIAP.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contatos API", Description = "Cadastro de Contatos", Version = "v1" }); });

builder.Services.AddDbContext<FiapDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "fiap"));

builder.Services.AddTransient<IContatoRepository, ContatoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

app.UseSwagger();

ContatosEndpoint.Map(app);

app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contatos API V1"); });

app.Run();



