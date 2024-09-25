using Fiap.Clientes.API.Application.Commands;
using Fiap.Clientes.API.Application.Events;
using Fiap.Clientes.API.Data;
using Fiap.Clientes.API.Data.Repository;
using Fiap.Clientes.API.Models;
using Fiap.Core.Mediator;
using FluentValidation.Results;
using MediatR;

namespace Fiap.Clientes.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegistrarContatoCommand, ValidationResult>, ContatoCommandHandler>();

            services.AddScoped<INotificationHandler<ContatoRegistradoEvent>, ContatoEventHandler>();

            services.AddScoped<IContatoRepository, ContatoRepository>();
            services.AddScoped<ContatoContext>();
        }
    }
}