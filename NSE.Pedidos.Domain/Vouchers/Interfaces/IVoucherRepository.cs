using Fiap.Core.Data;

namespace Fiap.Pedidos.Domain.Vouchers.Interfaces
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<Voucher?> ObterVoucherPorCodigo(string codigo);
        void Atualizar(Voucher voucher);
    }
}
