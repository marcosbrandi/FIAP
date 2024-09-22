namespace Fiap.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
