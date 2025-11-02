namespace ApplicationCore.Entities.Models
{
    public sealed class Brand
    {
        public Guid Id { get; set; }
        public required string BrandName { get; set; }
        public required List<Model> Models { get; set; }

    }


}
