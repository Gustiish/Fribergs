using ApplicationCore.Entities.Models;

namespace ApplicationCore.Interfaces
{
    public interface ICarBuilder
    {
        void SetName(string name);
        void SetModel(string model);
        void SetBrand(string brand);
        void SetPrice(double price);
        void SetImage(string url);
        Car Build();

    }
}
