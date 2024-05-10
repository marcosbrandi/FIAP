using TechChallengeFIAP.API.EndPoints;

namespace TechChallengeFIAP.API.Middleware;

public static class Endpoints
{
    public static void Set(WebApplication app)
    {
        ContatosEndpoint.Map(app);

    }

}

