namespace Contracts.DTO
{
    public sealed class CreateCarDTO
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public string URL { get; set; }
    }
}
