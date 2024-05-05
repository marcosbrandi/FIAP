using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastracture.Data;

namespace TechChallengeFIAP.API.EndPoints
{
    public static class ContatosEndpoint
    {
        public static void Map(WebApplication app)
        {
            const string baseUrl = @"/v1/contatos";

            var group = app.MapGroup(baseUrl);

            group.MapGet("/", async (FiapDbContext db) =>
            {
                return await db.Contatos.ToListAsync();
            });


            group.MapGet("/{id:int}", async (int id, FiapDbContext db) => await db.Contatos.FindAsync(id) is Contato item ? Results.Ok(item) : Results.NotFound());

            group.MapPost("", async (Contato contato, IContatoRepository contatoRepository) =>
            {
                try
                {
                    await contatoRepository.AddAsync(contato);
                    return Results.Created($"{baseUrl}/{contato.Id}", contato);
                }
                catch
                {
                    return Results.BadRequest();
                }

            });

            group.MapPut("/{id:int}", async (int id, Contato contato, FiapDbContext db) =>
            {
                try
                {
                    var existItem = await db.Contatos.FindAsync(id);
                    if (existItem is null) return Results.NotFound();

                    existItem.Nome = contato.Nome;
                    existItem.Telefone.DDD = contato.Telefone.DDD;
                    existItem.Telefone.Numero = contato.Telefone.Numero;
                    existItem.Email = contato.Email;
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
                catch
                {
                    return Results.BadRequest();
                }
            });

            group.MapDelete("/{id:int}", async (int id, FiapDbContext db) =>
            {
                try
                {
                    if (await db.Contatos.FindAsync(id) is Contato contato)
                    {
                        db.Contatos.Remove(contato);
                        await db.SaveChangesAsync();
                        return Results.NoContent();
                    }
                    return Results.NotFound();
                }
                catch
                {
                    return Results.BadRequest();
                }
            });


            /*
            app.MapGet("/", async context =>
            {
                // Get all todo items
                await context.Response.WriteAsJsonAsync(new { Message = "All todo items" });
            });

            app.MapGet("/{id}", async context =>
            {
                // Get one todo item
                await context.Response.WriteAsJsonAsync(new { Message = "One todo item" });
            });
            */
        }

    }


    public static class Teste
    {
        public static void AdicionarDadosTeste(FiapDbContext context)
        {
            if (context.Contatos.Count() > 0) return;

            var testeContato1 = new Contato
            {
                Id = 123456,
                Nome = "Teste",
                Email = "teste@gmail.com",
                Telefone = new Telefone { DDD = "11", Numero = "981498979" }
            };
            context.Contatos.Add(testeContato1);

            context.SaveChanges();
        }
    }


}

