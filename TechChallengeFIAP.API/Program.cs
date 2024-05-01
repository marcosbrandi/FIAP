using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using TechChallengeFIAP.API.EndPoints;
using TechChallengeFIAP.Infrastracture.Data;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contatos API", Description = "Cadastro de Contatos", Version = "v1" });
});

builder.Services.AddDbContext<FiapDbContext>( opt => opt.UseInMemoryDatabase(databaseName: "fiap") );


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();

/*
app.MapGet("/", (context) =>
{
    return Results.Redirect("swagger/index.html");
});
*/

ContatosEndpoint.Map(app);


app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contatos API V1");
});


app.Run();






/*
app.MapGet("/", (context) =>
{
    return (Task)Results.Redirect("swagger/index.html");
});


const string baseUrl = @"/v1/contatos";
var group = app.MapGroup(baseUrl);

group.MapGet("/", async (FiapDbContext db) =>
{
    return await db.Contatos.ToListAsync();
});

group.MapGet("/{id}", async (int id, FiapDbContext db) =>
{
    return await db.Contatos.FindAsync(id) is Contato item ? Results.Ok(item) : Results.NotFound();
});

group.MapPost("", async (Contato contato, FiapDbContext db) =>
{
    db.Contatos.Add(contato);
    await db.SaveChangesAsync();
    return Results.Created($"{baseUrl}/{contato.Id}", contato);
});

group.MapPut("/{id}", async (int id, Contato contato, FiapDbContext db) =>
{
    var existItem = await db.Contatos.FindAsync(id);

    if (existItem is null) return Results.NotFound();

    existItem.Nome = contato.Nome;
    existItem.Telefone = contato.Telefone;
    existItem.Email = contato.Email;
    await db.SaveChangesAsync();

    return Results.NoContent();
});

group.MapDelete("/{id}", async (int id, FiapDbContext db) =>
{
    if (await db.Contatos.FindAsync(id) is Contato contato)
    {
        db.Contatos.Remove(contato);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});
*/

/*

app.MapGet(baseUrl, async (FiapDbContext db) =>
    await db.Contatos.ToListAsync());

app.MapGet(baseUrl+"/{id}", async (int id, FiapDbContext db) =>
    await db.Contatos.FindAsync(id) is Contato item ? Results.Ok(item) : Results.NotFound());

app.MapPost("/v1/contatos", async (Contato contato, FiapDbContext db) =>
{
    db.Contatos.Add(contato);
    await db.SaveChangesAsync();
    return Results.Created($"{baseUrl}/{contato.Id}", contato);
});

app.MapPut("/v1/contatos/{id}", async (int id, Contato contato, FiapDbContext db) =>
{
    var existItem = await db.Contatos.FindAsync(id);

    if (existItem is null) return Results.NotFound();

    existItem.Nome = contato.Nome;
    existItem.Telefone = contato.Telefone;
    existItem.Email = contato.Email;
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/v1/contatos/{id}", async (int id, FiapDbContext db) =>
{
    if (await db.Contatos.FindAsync(id) is Contato contato)
    {
        db.Contatos.Remove(contato);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});
*/











/*
app.MapGet(baseUrl, ([FromServices] ContatoRepository items) =>
{
    var result = items.GetAllAsync();
    return result != null ? Results.Ok(result) : Results.NotFound();
}).Produces<List<Contato>>();

app.MapGet("{baseUrl}/{id}", ([FromServices] ContatoRepository items, int id) =>
{
    var result = items.GetByIdAsync(id);
    return result != null ? Results.Ok(result) : Results.NotFound();
}).Produces<Contato>();

app.MapPost("/v1/contatos", ([FromServices] ContatoRepository items, Contato item) =>
{
    items.Add(item);
    //return Results.Created($"/v1/contatos/{item.Id}", item);
    return Results.Created($"{baseUrl}/{item.Id}", item);
});

//app.MapPut("/v1/contatos/{id}", ([FromServices] ContatoRepository items, int id, Contato item) =>
app.MapPut($"{{baseUrl}}/{{id}}", ([FromServices] ContatoRepository items, int id, Contato item) =>
{
    if (items.GetByIdAsync(id) == null)
    {
        return Results.NotFound();
    }
    items.Update(item);
    return Results.Ok(item);
});

//app.MapDelete("/v1/contatos/{id}", ([FromServices] ContatoRepository items, int id) =>
app.MapDelete("{baseUrl}/{id}", ([FromServices] ContatoRepository items, int id) =>
{
    var item = items.GetByIdAsync(id);
    //if (items.GetByIdAsync(id) == null)
    if (item == null)
    {
        return Results.NotFound();
    }
    items.Delete(item);
    return Results.NoContent();
});
*/
