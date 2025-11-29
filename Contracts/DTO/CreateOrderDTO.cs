namespace Contracts.DTO
{
    public class CreateOrderDTO
    {
        public double Price { get; set; }
        public DateTime Start { get; set; } = DateTime.UtcNow;
        public DateTime End { get; set; } = DateTime.UtcNow;
        public Guid CarId { get; set; }
        public CarDTO Car { get; set; }
        public Guid UserId { get; set; }

    }
}
