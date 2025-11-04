using ApplicationCore.Entities.Models;
using ApplicationCore.Interfaces.Repository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<bool> CreateAsync(Car car)
        {
            try
            {
                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database update error while creating car: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while creating car: {ex.Message}");
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                Car? car = _context.Cars.Find(Id);
                if (car == null)
                {
                    Console.WriteLine($"Could now find car with id {Id}");
                    return false;
                }
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database update error while deleting car: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while deleting car: {ex.Message}");
            }
            return false;

        }

        public Car? Find(Guid Id)
        {
            try
            {
                return _context.Cars.Find(Id) ?? null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while finding car: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<Car>?> GetAllAsync()
        {
            try
            {
                return await _context.Cars.ToListAsync() ?? null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while getting all cars: {ex.Message}");
                return Enumerable.Empty<Car>();
            }
        }

        public async Task<bool> UpdateAsync(Car car)
        {
            try
            {
                _context.Update(car);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database update error while updating car: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while updating car: {ex.Message}");
            }
            return false;
        }
    }
}
