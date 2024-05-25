using TechChallengeFIAP.Core.Entities;

namespace TechChallengeFIAP.Core.Interfaces;

public interface IDDDRegionService
{
    public Task<DDDInfo> GetInfo(string DDD);
}
