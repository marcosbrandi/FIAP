using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastructure.Repositories;

namespace TechChallengeFIAP.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class FiapMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly FiapMiddlewareOptions _options;

        public FiapMiddleware(RequestDelegate next /*, FiapMiddlewareOptions options*/)
        {
            _next = next;
            //_options = options;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            //builder.
            return builder.UseMiddleware<FiapMiddleware>();
        }

        public static IServiceCollection AddMyMiddleware(this IServiceCollection services) //, Action<FiapMiddlewareOptions> options = default)
        {
            //options = options ?? (opts => { });

            services.AddTransient<IContatoRepository, ContatoRepository>();

            //services.Configure(options);
            return services;
        }
    }

    public class FiapMiddlewareOptions
    {
        public string BeforeMessage { get; set; } = "Chamou nosso middleware (antes)";
        public string AfterMessage { get; set; } = "Chamou nosso middleware (depois)";
    }

}
