using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastracture.Repositories;

namespace TechChallengeFIAP.API;

public static class Interfaces
{
    public static void Add(IServiceCollection services)
    {
        services.AddTransient<IRepository<Contato>, Repository<Contato>>();

    }

}

