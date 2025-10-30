namespace ApplicationCore.Entities.Models
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string BrandName { get; set; }
        public required List<Model> Models { get; set; }

    }


}
