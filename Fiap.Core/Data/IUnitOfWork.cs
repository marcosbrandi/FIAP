using System.Threading.Tasks;

namespace Fiap.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}