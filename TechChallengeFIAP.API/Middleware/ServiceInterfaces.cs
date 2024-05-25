using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastructure.Repositories;
using TechChallengeFIAP.Infrastructure.Services;

namespace TechChallengeFIAP.API.Middleware;

public static class ServiceInterfaces
{
    public static void Add(IServiceCollection services)
    {
        services.AddTransient<IDDDRegionService, DDDRegionService>();
        services.AddTransient<IContatoRepository, ContatoRepository>();


    }

}

