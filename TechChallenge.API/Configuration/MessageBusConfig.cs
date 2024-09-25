using Fiap.Clientes.API.Services;
using Fiap.Core.Utils;
using Fiap.MessageBus;

namespace Fiap.Clientes.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus")).AddHostedService<NovoContatoIntegrationHandler>();
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus")).AddHostedService<AtualizaContatoIntegrationHandler>();
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus")).AddHostedService<DeleteContatoIntegrationHandler>();
        }
    }
}