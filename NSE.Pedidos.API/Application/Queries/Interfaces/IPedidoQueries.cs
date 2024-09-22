using Fiap.Pedidos.API.Application.DTO;

namespace Fiap.Pedidos.API.Application.Queries.Interfaces
{
    public interface IPedidoQueries
    {
        Task<PedidoDTO> ObterUltimoPedido(Guid clientId);
        Task<PedidoDTO?> ObterPedidosAutorizados();
        Task<IEnumerable<PedidoDTO>> ObterListaPorClienteId(Guid clientId);
    }
}
