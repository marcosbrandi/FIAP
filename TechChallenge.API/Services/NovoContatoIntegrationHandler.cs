﻿using Fiap.Clientes.API.Application.Commands;
using Fiap.Core.Mediator;
using Fiap.Core.Messages.Integration;
using Fiap.MessageBus;
using FluentValidation.Results;

namespace Fiap.Clientes.API.Services
{
    public class NovoContatoIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public NovoContatoIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<ContatoRegistradoIntegrationEvent, ResponseMessage>(RegistrarContato);

            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> RegistrarContato(ContatoRegistradoIntegrationEvent message)
        {
            var clienteCommand = new NovoContatoCommand(message.Nome, message.Email);
            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.EnviarComando(clienteCommand);
            }

            return new ResponseMessage(sucesso);
        }
    }
}