using ApplicationCore.Entities.Models;

namespace ApplicationCore.Interfaces
{
    public interface ICarReference
    {
        Task<List<Brand>?> GetCarReferencesAsync();

        //Implementera reference för att primärt lägga till märken och dess respektive modeller.
    }
}
