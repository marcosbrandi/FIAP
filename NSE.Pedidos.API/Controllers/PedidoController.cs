﻿using Microsoft.AspNetCore.Mvc;
using Fiap.Core.Mediator;
using Fiap.Pedidos.API.Application.Commands;
using Fiap.Pedidos.API.Application.Queries.Interfaces;
using Fiap.WebAPI.Core.Controllers;
using Fiap.WebAPI.Core.Usuario;

namespace Fiap.Pedidos.API.Controllers
{
    public class PedidoController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly IMediatorHandler _mediator;
        private readonly IPedidoQueries _pedidoQueries;

        public PedidoController(
            IAspNetUser user,
            IMediatorHandler mediator,
            IPedidoQueries pedidoQueries)
        {
            _user = user;
            _mediator = mediator;
            _pedidoQueries = pedidoQueries;
        }

        [HttpPost("pedido")]
        public async Task<IActionResult> AdicionarPedido(AdicionarPedidoCommand pedido)
        {
            pedido.ClienteId = _user.ObterUserId();
            return CustomResponse(await _mediator.EnviarComando(pedido));
        }

        [HttpGet("pedido/ultimo")]
        public async Task<IActionResult> UltimoPedido()
        {
            var pedido = await _pedidoQueries.ObterUltimoPedido(_user.ObterUserId());

            return pedido is null ? NotFound() : CustomResponse(pedido);
        }

        [HttpGet("pedido/lista-cliente")]
        public async Task<IActionResult> ListaPorCliente()
        {
            var pedidos = await _pedidoQueries.ObterListaPorClienteId(_user.ObterUserId());

            return pedidos is null ? NotFound() : CustomResponse(pedidos);
        }
    }
}
