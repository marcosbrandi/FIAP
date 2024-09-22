using Microsoft.EntityFrameworkCore;
using Fiap.Core.Data;
using Fiap.Pedidos.Domain.Vouchers;
using Fiap.Pedidos.Domain.Vouchers.Interfaces;

namespace Fiap.Pedidos.Infra.Data.Repositorios
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly PedidoContext _context;

        public VoucherRepository(PedidoContext context)
        {
            _context = context;
        }

        public async Task<Voucher?> ObterVoucherPorCodigo(string codigo)
        {
            return await _context.Vouchers
                .FirstOrDefaultAsync(v => v.Codigo == codigo);
        }

        public void Atualizar(Voucher voucher) => _context.Vouchers.Update(voucher);

        public IUnitOfWork UnitOfWork => _context;

        public void Dispose() => _context.Dispose();
    }
}