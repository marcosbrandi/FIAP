using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastructure.Repositories;

namespace TechChallengeFIAP.API.Middleware;

public static class ServiceInterfaces
{
    public static void Add(IServiceCollection services)
    {
        //services.AddTransient<IRepository<Contato>, Repository<Contato>>();
        services.AddTransient<IContatoRepository, ContatoRepository>();


    }

}

