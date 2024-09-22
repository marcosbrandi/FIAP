using MediatR;
using Microsoft.EntityFrameworkCore;
using Fiap.Core.Mediator;
using Fiap.Pedidos.API.Application.Queries;
using Fiap.Pedidos.API.Application.Queries.Interfaces;
using Fiap.Pedidos.Domain.Pedidos.Interfaces;
using Fiap.Pedidos.Domain.Vouchers.Interfaces;
using Fiap.WebAPI.Core.Usuario;
using System.Reflection;
using Fiap.Pedidos.Infra.Data.Repositorios;
using Fiap.Pedidos.Infra.Data;

namespace Fiap.Pedidos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder RegisterDependencies(this WebApplicationBuilder builder)
        {
            // API
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IAspNetUser, AspNetUser>();

            // Application
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
            builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

            builder.Services.AddScoped<IVoucherQueries, VoucherQueries>();
            builder.Services.AddScoped<IPedidoQueries, PedidoQueries>();

            //services.AddScoped<IRequestHandler<AdicionarPedidoCommand, ValidationResult>, PedidoCommandHandler>();

            //services.AddScoped<INotificationHandler<PedidoRealizadoEvent>, PedidoEventHandler>();

            // Data
            //builder.Services.AddScoped<PedidoContext>();
            builder.Services.AddDbContext<PedidoContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
            builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

            return builder;
        }
    }
}
