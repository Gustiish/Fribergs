using ApplicationCore.Interfaces.Entity;

namespace ApplicationCore.Interfaces.Repository
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<bool> CreateAsync(T Entity);
        Task<bool> UpdateAsync(T Entity);
        Task<T?> FindAsync(Guid Id);
        Task<bool> DeleteAsync(Guid Id);
        Task<IEnumerable<T>?> GetAllAsync();

    }
}
