namespace Contracts.DTO
{
    public sealed class ModelDTO
    {
        public Guid Id { get; set; }
        public required string ModelName { get; set; }
        public required BrandDTO Brand { get; set; }
        public Guid BrandId { get; set; }

    }
}
