﻿using Fiap.Core.Utils;
using Fiap.Infra.MessageBus;
using Fiap.Pedidos.API.Services;

namespace Fiap.Pedidos.API.Configuration
{
    public static class MessageBusConfig
    {
        public static WebApplicationBuilder AddMessageBusConfiguration(this WebApplicationBuilder builder)
        {
            // OBS.: Todo hosted service tem injeção de dependência singleton. Logo não pode injetar nada nele que não seja singleton

            builder.Services.AddMessageBus(builder.Configuration.GetMessageQueueConnection("MessageBus")!)
                .AddHostedService<PedidosOrquestradorIntegrationHandler>()
                .AddHostedService<PedidoIntegrationHandler>();

            return builder;
        }
    }
}
