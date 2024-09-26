using TechChallenge.Core.Entities;
using TechChallenge.Core.Interfaces;

namespace TechChallenge.API.Endpoints;

public static class ContatoEndpoints
{
    public static void Map1(IEndpointRouteBuilder app)
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

        group.MapGet("/{id:int}", async (Guid id, IContatoRepository repository)
            => await repository.FindAsync(id) is Contato item ? Results.Ok(item) : Results.NotFound($"Contato ID {id} não localizado."));

        group.MapGet("/Search", async (string? DDD, IContatoRepository repository) =>
        {
            var contatos = await repository.GetAllAsync(DDD);

            if (contatos == null || contatos.Count() == 0)
                throw new InvalidOperationException($"Contatos com o DDD: {(DDD is null ? "nulo" : DDD)} não encontrado.");

            return contatos;
        });
    }

    //public static void Map(WebApplication app)
    //{
    //    const string baseUrl = @"/v1/contatos";

    //    var group = app.MapGroup(baseUrl);

    //    group.MapGet("/", async (IContatoRepository repository) =>
    //    {
    //        if (await repository.CountAsync() == 0)
    //            Teste.AdicionarDadosTeste(repository);

    //        var ret = await repository.GetAllAsync(null);
    //        return Results.Ok(ret);
    //    });

    //    group.MapGet("/{id:int}", async (Guid id, IContatoRepository repository)
    //        => await repository.FindAsync(id) is Contato item ? Results.Ok(item) : Results.NotFound($"Contato ID {id} não localizado."));

    //    group.MapGet("/Search", async (string? DDD, IContatoRepository repository) =>
    //    {
    //        var contatos = await repository.GetAllAsync(DDD);

    //        if (contatos == null || contatos.Count() == 0)
    //            throw new InvalidOperationException($"Contatos com o DDD: {(DDD is null ? "nulo" : DDD)} não encontrado.");

    //        return contatos;
    //    });
    //}
}

public static class Teste
{
    public static void AdicionarDadosTeste(IContatoRepository repository)
    {
        var ret = repository.GetAllAsync(null).Result;
        if (ret.Count() > 0) return;

        var testeContato1 = new Contato("Teste", "teste@gmail.com");
        //var testeContato1 = new Contato
        //{
        //    Id = 123456,
        //    Nome = "Teste",
        //    Email = "teste@gmail.com",
        //    Telefone = new Telefone { DDD = "11", Numero = "982598878" }
        //};
        repository.AddAsync(testeContato1);
    }
}

