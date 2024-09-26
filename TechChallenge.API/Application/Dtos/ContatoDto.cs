namespace Ordering.Application.Dtos;

public record ContatoDto(
    Guid Id,
    Guid CustomerId,
    string Nome,
    string Emmail,
    List<TelefoneDto> OrderItems);
