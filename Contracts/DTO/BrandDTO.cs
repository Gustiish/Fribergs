namespace Contracts.DTO
{
    public sealed class BrandDTO
    {
        public Guid Id { get; set; }
        public required string BrandName { get; set; }
        public required List<ModelDTO> Models { get; set; }
    }
}
