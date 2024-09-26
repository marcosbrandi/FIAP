using TechChallenge.Core.Entities;
using TechChallenge.Core.Interfaces;

namespace TechChallenge.API.Endpoints;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetProductsResponse(IEnumerable<Contato> Products);

public static class BuscarContatos
{
    public static void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (IContatoRepository repository) =>
        {
            var result = await repository.GetAllAsync(null);

            return Results.Ok(result);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
