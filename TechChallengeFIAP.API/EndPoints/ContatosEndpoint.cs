using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Infrastracture.Data;

namespace TechChallengeFIAP.API.EndPoints
{
    public static class ContatosEndpoint
    {
        public static void Map(WebApplication app)
        {
            const string baseUrl = @"/v1/contatos";

            var group = app.MapGroup(baseUrl);

            group.MapGet("/", async (FiapDbContext db) => await db.Contatos.ToListAsync());

            group.MapGet("/{id:int}", async (int id, FiapDbContext db) => await db.Contatos.FindAsync(id) is Contato item ? Results.Ok(item) : Results.NotFound());

            group.MapPost("", async (Contato contato, FiapDbContext db) =>
            {
                db.Contatos.Add(contato);
                await db.SaveChangesAsync();
                return Results.Created($"{baseUrl}/{contato.Id}", contato);
            });

            group.MapPut("/{id:int}", async (int id, Contato contato, FiapDbContext db) =>
            {
                var existItem = await db.Contatos.FindAsync(id);

                if (existItem is null) return Results.NotFound();

                existItem.Nome = contato.Nome;
                existItem.Telefone = contato.Telefone;
                existItem.Email = contato.Email;
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            group.MapDelete("/{id:int}", async (int id, FiapDbContext db) =>
            {
                if (await db.Contatos.FindAsync(id) is Contato contato)
                {
                    db.Contatos.Remove(contato);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }

                return Results.NotFound();
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
            var testeContato1 = new Contato
            {
                //Id = "usuario1",
                Nome = "Teste",
                Email = "teste@gmail.com",
                Telefone = new Telefone { DDD = "11", Numero = "981498979" }
            };
            context.Contatos.Add(testeContato1);

            /*
            var testePost1 = new Models.Post
            {
                Id = "post1",
                UsuarioId = testeUsuario1.Id,
                Conteudo = "Primeiro Post(post1) do Usuario : usuario1"
            };
            */

            context.Contatos.Add(testeContato1);

            context.SaveChanges();
        }
    }

}

