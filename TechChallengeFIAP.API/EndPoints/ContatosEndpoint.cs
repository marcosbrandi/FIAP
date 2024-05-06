using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;

namespace TechChallengeFIAP.API.EndPoints;

public static class ContatosEndpoint
{
    public static void Map(WebApplication app)
    {
        const string baseUrl = @"/v1/contatos";

        var group = app.MapGroup(baseUrl);
         
        group.MapGet("/", async (IRepository<Contato> repository) => 
        {
            if (repository.Count() == 0)
                Teste.AdicionarDadosTeste(repository);

            var ret = await repository.GetAllWithTrackingAsync(d=> d==d, r => r.Telefones);
            return Results.Ok(ret);
        });
            
        group.MapGet("/{id:int}", async (int id, IRepository<Contato> repository)
            => await repository.GetByIdAsync(id) is Contato item ? Results.Ok(item) : Results.NotFound());

        group.MapPost("", async (Contato contato, IRepository<Contato> repository) =>
        {
            try
            {
                await repository.AddAsync(contato);
                return Results.Created($"{baseUrl}/{contato.Id}", contato);
            }
            catch (Exception ex)
            {
                //throw;
                return Results.BadRequest();
            }

        });

        group.MapPut("/{id:int}", async (int id, Contato contato, IRepository<Contato> repository) =>
        {
            try
            {
                Contato currentContato = await repository.GetByIdWithTrackingAsync(id, r => r.Telefones);
                if (currentContato is null) return Results.NotFound();

                currentContato.Nome = contato.Nome;
                currentContato.Email = contato.Email;
                currentContato.Telefones = contato.Telefones?.ToList();

                var ret = await repository.UpdateAsync(currentContato);
                return Results.Ok($"{ret} Registro(s) atualizado(s) com sucesso!");
            }
            catch (Exception ex)
            {
                //throw;
                return Results.BadRequest("Erro na atualização do registro!");
            }
        });

        group.MapDelete("/{id:int}", async (int id, IRepository<Contato> repository) =>
        {
            try
            {
                if (await repository.GetByIdAsync(id) is Contato contato)
                {
                    int ret = await repository.DeleteAsync(contato);
                    return Results.Ok($"{ret} Registro(s) excluído(s) com sucesso!");
                }
                return Results.NotFound();
            }
            catch (Exception ex)
            {
                //throw;
                return Results.BadRequest();
            }
        });

    }

}


public static class Teste
{
    public static void AdicionarDadosTeste(IRepository<Contato> repository)
    {
        var ret = repository.GetAllAsync().Result;
        if (ret.Count() > 0) return;

        var testeContato1 = new Contato
        {
            Id = 123456,
            Nome = "Teste",
            Email = "teste@gmail.com",
            Telefones = new List<Telefone>() { new Telefone { DDD = "11", Numero = "982598878" } }
        };

        repository.Add(testeContato1);
    }
}

