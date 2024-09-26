using TechChallenge.Core.Interfaces;

namespace TechChallenge.API.Endpoints;

public record BuscarContatoPorIdRequest(Guid Id);

public class BuscarContatoPorId
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/contatos", async ([AsParameters] BuscarContatoPorIdRequest request, IContatoRepository repository) =>
        {
            //var query = request.Adapt<GetProductsQuery>();

            var result = await repository.FindAsync(request.Id);

            return Results.Ok(result);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
