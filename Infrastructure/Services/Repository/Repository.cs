using ApplicationCore.Interfaces.Entity;
using ApplicationCore.Interfaces.Repository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DbSet<T> _set;
        private readonly ApplicationDbContext _context;


        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }

        public virtual async Task<bool> CreateAsync(T Entity)
        {
            try
            {
                _set.Add(Entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create entity {nameof(Entity)}: {ex.Message}");
                return false;
            }

        }

        public virtual async Task<T?> FindAsync(Guid id)
        {
            try
            {
                return await _set.FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to find {typeof(T).Name} with ID {id}: {ex.Message}");
                return null;
            }
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await FindAsync(id);
                if (entity == null)
                {
                    Console.WriteLine($"{typeof(T).Name} with ID {id} not found.");
                    return false;
                }

                _set.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete {typeof(T).Name} with ID {id}: {ex.Message}");
                return false;
            }
        }

        public virtual async Task<IEnumerable<T>?> GetAllAsync()
        {
            try
            {
                return await _set.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get all {typeof(T).Name} entities: {ex.Message}");
                return null;
            }


        }


        public virtual async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _set.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update {typeof(T).Name}: {ex.Message}");
                return false;
            }
        }
    }
}
