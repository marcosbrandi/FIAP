using Fiap.Clientes.API.Application.Commands;
using Fiap.Core.Mediator;
using Fiap.Core.Messages.Integration;
using Fiap.MessageBus;
using FluentValidation.Results;

namespace Fiap.Clientes.API.Services
{
    public class AtualizaContatoIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public AtualizaContatoIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<ContatoAtualizadoIntegrationEvent, ResponseMessage>(AtualizarCliente);

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

        private async Task<ResponseMessage> AtualizarCliente(ContatoAtualizadoIntegrationEvent message)
        {
            var clienteCommand = new AtualizarContatoCommand(message.Id, message.Nome, message.Email);
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