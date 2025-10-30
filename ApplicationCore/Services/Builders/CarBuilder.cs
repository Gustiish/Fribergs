using ApplicationCore.Entities.Models;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services.Builders
{
    public class CarBuilder : ICarBuilder
    {
        private readonly ICarReference _reference;
        private Car Car = new Car();
        public CarBuilder(ICarReference reference)
        {
            this.Reset();
            _reference = reference;
        }
        public Car Build()
        {
            return this.Car;
        }

        public void SetBrand(string brand)
        {
            this.Car.Brand = brand;
        }

        public void SetModel(string model)
        {
            this.Car.Model = model;
        }

        public void SetName(string name)
        {
            this.Car.Name = name;
        }

        public void SetPrice(double price)
        {
            this.Car.Price = price;
        }

        private void Reset()
        {
            this.Car = new Car();
        }
    }
}
