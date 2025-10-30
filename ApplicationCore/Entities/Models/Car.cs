namespace ApplicationCore.Entities.Models
{
    public sealed class Car
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
    }
}
