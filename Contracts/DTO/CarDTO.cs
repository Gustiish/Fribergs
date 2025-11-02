namespace Contracts.DTO
{
    public sealed class CarDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public double Price { get; set; }
        public string URL { get; set; }
    }
}
