using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fiap.Core.Utils;
using Fiap.MessageBus;

namespace Fiap.Identidade.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
        }
    }
}