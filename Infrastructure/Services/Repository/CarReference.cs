using ApplicationCore.Entities.Models;
using ApplicationCore.Interfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Repository
{
    public sealed class CarReference : ICarReference
    {
        private readonly ApplicationDbContext _context;
        public CarReference(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Brand>?> GetCarReferencesAsync()
        {
            try
            {
                return await _context.Brands.Include(b => b.Models).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed top retrieve brands and models");
                return null;
            }
        }
    }
}

