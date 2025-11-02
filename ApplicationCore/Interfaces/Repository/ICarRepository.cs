using ApplicationCore.Entities.Models;

namespace ApplicationCore.Interfaces.Repository
{
    public interface ICarRepository
    {
        Task<bool> CreateAsync(Car car);
        Task<bool> UpdateAsync(Car car);
        Car? Find(Guid Id);
        Task<bool> DeleteAsync(Guid Id);
        Task<IEnumerable<Car>?> GetAllAsync();

    }
}
