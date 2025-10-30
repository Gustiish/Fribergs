namespace ApplicationCore.Entities.Models
{
    public sealed class Model
    {
        public Guid Id { get; set; }
        public string ModelName { get; set; }
        public Brand Brand { get; set; }
        public Guid BrandId { get; set; }

    }
}
