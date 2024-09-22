using Microsoft.AspNetCore.Mvc;
using Fiap.Pedidos.API.Application.DTO;
using Fiap.Pedidos.API.Application.Queries.Interfaces;
using Fiap.WebAPI.Core.Controllers;

namespace Fiap.Pedidos.API.Controllers
{
    public class VoucherController : MainController
    {
        private readonly IVoucherQueries _voucherQueries;

        public VoucherController(IVoucherQueries voucherQueries)
        {
            _voucherQueries = voucherQueries;
        }

        [HttpGet("voucher/{codigo}")]
        [ProducesResponseType(typeof(VoucherDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorCodigo(string codigo)
        {
            if (string.IsNullOrEmpty(codigo)) return NotFound();

            var voucher = await _voucherQueries.ObterVoucherPorCodigo(codigo);

            return voucher is null ? NotFound() : CustomResponse(voucher);
        }
    }
}
