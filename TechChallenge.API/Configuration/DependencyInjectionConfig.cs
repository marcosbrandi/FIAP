using Fiap.Clientes.API.Application.Commands;
using Fiap.Clientes.API.Application.Events;
using Fiap.Core.Mediator;
using FluentValidation.Results;
using MediatR;
using TechChallenge.Core.Interfaces;
using TechChallenge.Infrastracture.Data;
using TechChallenge.Infrastructure.Repositories;

namespace Fiap.Clientes.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<NovoContatoCommand, ValidationResult>, NovoContatoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarContatoCommand, ValidationResult>, AtualizarContatoCommandHandler>();
            //services.AddScoped<IRequestHandler<NovoContatoCommand, ValidationResult>, NovoContatoCommandHandler>();

            services.AddScoped<INotificationHandler<ContatoRegistradoEvent>, ContatoEventHandler>();

            services.AddScoped<IContatoRepository, ContatoRepository>();
            services.AddScoped<FiapDbContext>();
        }
    }
}