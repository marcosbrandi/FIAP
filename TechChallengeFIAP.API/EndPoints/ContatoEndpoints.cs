using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;

namespace TechChallengeFIAP.API.EndPoints;

public static class ContatoEndpoints
{
    /// <summary>
    /// Método responsável por mapear todas as operações realizadas pela entidade Contato.
    /// Necessário receber uma instância de WebApplication para execução das operações CRUD da entidade
    /// </summary>
    /// <param name="app"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void Map(WebApplication app)
    {
        const string baseUrl = @"/v1/contatos";

        var group = app.MapGroup(baseUrl);

        group.MapGet("/", async (IContatoRepository repository) =>
        {
            if (await repository.CountAsync() == 0)
                SeedTest.Add(repository);

            var ret = await repository.GetAllAsync(null);
            return Results.Ok(ret);
        });

        // Retorna um contato pelo ID
        group.MapGet("/{id:int}", async (int id, IContatoRepository repository)
            => await repository.FindAsync(id) is Contato item ? Results.Ok(item) : Results.NotFound($"Contato ID {id} não localizado."));

        /// <summary>
        /// Retorna um contato pelo nome
        /// </summary>
        /// <param nome="nome"></param>
        /// <returns></returns>
        /// <exception cref="WarningException"></exception>
        group.MapGet("/Search/Name", async (string nome, IContatoRepository repository)
            => await repository.GetByNameAsync(nome) is Contato item ? Results.Ok(item) : Results.NotFound($"Contato {nome} não localizado."));

        // Retorna um contato pelo DDD
        group.MapGet("/Search", async (string? DDD, IContatoRepository repository) =>
        {
            var contatos = await repository.GetAllAsync(DDD);
            if (contatos == null || contatos.Count() == 0)
               return Results.NotFound($"Contatos com o DDD: {(DDD is null ? "nulo" : DDD)} não encontrado.");
           
            return Results.Ok(contatos);
        });

        // Adiciona um novo contato
        group.MapPost("", async (Contato contato, IContatoRepository repository) =>
        {
            await repository.AddAsync(contato);
            return Results.Created($"{baseUrl}/{contato.Id}", contato);
        });

        // Atualiza um contato
        group.MapPut("", async (Contato contato, IContatoRepository repository) =>
        {
            if (await repository.FindAsync(contato.Id) is Contato currentContato)
            {
                currentContato.Nome = contato.Nome;
                currentContato.Email = contato.Email;
                currentContato.Telefone.DDD = contato.Telefone.DDD;
                currentContato.Telefone.Numero = contato.Telefone.Numero;

                await repository.UpdateAsync(currentContato);
                return Results.Ok($"Registro(s) atualizado(s) com sucesso!");
            }
            return Results.NotFound();
        });

        // Deleta um contato
        group.MapDelete("/{id:int}", async (int id, IContatoRepository repository) =>
        {
            if (await repository.FindAsync(id) is Contato contato)
            {
                await repository.DeleteAsync(contato);
                return Results.Ok($"Registro excluído com sucesso!");
            }
            return Results.NotFound();
        });

        // Retorna informações sobre o DDD
        group.MapGet("/DDD/{DDD}", async (string DDD, IContatoRepository repository) =>
        {
            var contatos = await repository.GetAllAsync(DDD);
            if (contatos == null || contatos.Count() == 0)
                return Results.NotFound($"Contatos com o DDD: {DDD} não encontrado.");
            return Results.Ok(contatos);
        });




    }

}




