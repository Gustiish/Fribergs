namespace ApplicationCore.Entities.Models
{
    public sealed class Model
    {
        public Guid Id { get; set; }
        public required string ModelName { get; set; }
        public required Brand Brand { get; set; }
        public Guid BrandId { get; set; }

    }
}
