using Fiap.Pedidos.API.Application.DTO;

namespace Fiap.Pedidos.API.Application.Queries.Interfaces
{
    public interface IVoucherQueries
    {
        Task<VoucherDTO?> ObterVoucherPorCodigo(string codigo);
    }
}
