using ApplicationCore.Interfaces.Entity;

namespace ApplicationCore.Entities.Models
{
    public sealed class Car : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public double Price { get; set; }
        public string URL { get; set; }

    }
}
