using ApplicationCore.Entities.Models;

namespace ApplicationCore.Interfaces
{
    public interface ICarBuilder
    {
        void SetName(string name);
        void SetModel(string model);
        void SetBrand(string brand);
        void SetPrice(double price);
        Car Build();

    }
}
