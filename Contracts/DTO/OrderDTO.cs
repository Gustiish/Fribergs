namespace Contracts.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid CarId { get; set; }
        public CarDTO Car { get; set; }
        public Guid UserId { get; set; }
    }
}
