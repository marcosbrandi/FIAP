using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;

namespace TechChallengeFIAP.API.EndPoints;

public static class ContatosEndpoint
{
    public static void Map(WebApplication app)
    {
        const string baseUrl = @"/v1/contatos";

        var group = app.MapGroup(baseUrl);

        group.MapGet("/", async (IContatoRepository repository) =>
        {
            if (await repository.CountAsync() == 0)
                Teste.AdicionarDadosTeste(repository);

            var ret = await repository.GetAllAsync(null);
            return Results.Ok(ret);
        });
            
        group.MapGet("/{id:int}", async (int id, IContatoRepository repository)
            => await repository.FindAsync(id) is Contato item ? Results.Ok(item) : Results.NotFound($"Contato ID {id} não localizado."));

        group.MapGet("/Search", async (string? DDD, IContatoRepository repository) =>
        {
            var contatos = await repository.GetAllAsync(DDD);

            if (contatos == null || contatos.Count() == 0)
                throw new InvalidOperationException($"Contatos com o DDD: {(DDD is null ? "nulo" : DDD)} não encontrado.");
            
            return contatos;
        });
        
        group.MapPost("", async (Contato contato, IContatoRepository repository) =>
        {
            await repository.AddAsync(contato);
            return Results.Created($"{baseUrl}/{contato.Id}", contato);
        });

        group.MapPut("/{id:int}", async (int id, Contato contato, IContatoRepository repository) =>
        {
            Contato? currentContato = await repository.FindAsync(id);
            if (currentContato is null) return Results.NotFound();

            currentContato.Nome = contato.Nome;
            currentContato.Email = contato.Email;
            currentContato.Telefone.DDD = contato.Telefone.DDD;
            currentContato.Telefone.Numero = contato.Telefone.Numero;

            await repository.UpdateAsync(currentContato);
            return Results.Ok($"Registro(s) atualizado(s) com sucesso!");
        });

        group.MapDelete("/{id:int}", async (int id, IContatoRepository repository) =>
        {
            if (await repository.FindAsync(id) is Contato contato)
            {
                await repository.DeleteAsync(contato);
                return Results.Ok($"Registro excluído com sucesso!");
            }
            return Results.NotFound();
        });

    }

}


public static class Teste
{
    public static void AdicionarDadosTeste(IContatoRepository repository)
    {
        var ret = repository.GetAllAsync(null).Result;
        if (ret.Count() > 0) return;

        var testeContato1 = new Contato
        {
            Id = 123456,
            Nome = "Teste",
            Email = "teste@gmail.com",
            Telefone = new Telefone { DDD = "11", Numero = "982598878" }
        };
        repository.AddAsync(testeContato1);
    }
}

