using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TechChallengeFIAP.API.Middleware;
using TechChallengeFIAP.Infrastracture.Data;
using System.Reflection;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { 
    Title = "Contatos API", Description = "Cadastro de Contatos", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.EnableAnnotations();
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

const string baseUrl = @"/v1/contatos";


app.MapGet($"/Buscar/Nome", async (string nome, IContatoRepository repository)
    => await repository.GetByNameAsync(nome) is Contato item ? Results.Ok(item) : Results.NotFound($"Contato {nome} não localizado."));

// Retorna um contato pelo ID
app.MapGet("/Buscar/Id", async (int id, IContatoRepository repository)
    => await repository.FindAsync(id) is Contato item ? Results.Ok(item) : Results.NotFound($"Contato ID {id} não localizado.")).
       WithMetadata(new SwaggerOperationAttribute($@"Retorna um contato passando Id de registro como parâmetro", "description001"));

// Retorna um contato pelo DDD
app.MapGet("/Buscar/DDD", async (string? DDD, IContatoRepository repository) =>
{
    var contatos = await repository.GetAllAsync(DDD);
    if (contatos == null || contatos.Count() == 0)
        return Results.NotFound($"Contatos com o DDD: {(DDD is null ? "nulo" : DDD)} não encontrado.");

    return Results.Ok(contatos);
}).WithMetadata(new SwaggerOperationAttribute("Retorna todos os contatos correspondentes ao DDD recebido como parâmetro"));

// Retorna informações sobre o DDD
app.MapGet("/Buscar/UfPorDDD", async (string DDD, IContatoRepository repository) =>
{
    var contatos = await repository.GetAllAsync(DDD);
    if (contatos == null || contatos.Count() == 0)
        return Results.NotFound($"Contatos com o DDD: {DDD} não encontrado.");
    return Results.Ok(contatos);
}).WithMetadata(new SwaggerOperationAttribute("Retorna o estado correspondente ao DDD recebido como parâmetro"));

// Adiciona um novo contato
app.MapPost("Inserir/Contato", async (Contato contato, IContatoRepository repository) =>
{
    await repository.AddAsync(contato);
    return Results.Created($"{baseUrl}/{contato.Id}", contato);
}).WithMetadata(new SwaggerOperationAttribute($"Cria um novo contato, os parâmetros devem corresponder ao body do json, há validações para Id e E-mail repetido"));

// Atualiza um contato
app.MapPut("Atualizar/Contato", async (int id,Contato contato, IContatoRepository repository) =>
{
    Contato currentContato = await repository.FindAsync(id);
    if (currentContato != null)
    {
    await repository.UpdateAsync(currentContato,contato);
    return Results.Ok($"Registro(s) atualizado(s) com sucesso!");
    }
    
    return Results.NotFound($"Contato ID {id} não localizado.");

}).WithMetadata(new SwaggerOperationAttribute("Atualiza um contato"));

// Deleta um contato
app.MapDelete("Deletar/Contato", async (int id, IContatoRepository repository) =>
{
    if (await repository.FindAsync(id) is Contato contato)
    {
        await repository.DeleteAsync(contato);
        return Results.Ok($"Registro excluído com sucesso!");
    }
    return Results.NotFound();
}).WithMetadata(new SwaggerOperationAttribute("Deleta um contato correspondente ao Id recebido como parâmetro"));

app.Run();



