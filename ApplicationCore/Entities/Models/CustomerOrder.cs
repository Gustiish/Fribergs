using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Entity;

namespace ApplicationCore.Entities.Models
{
    public class CustomerOrder : IEntity
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid CarId { get; set; }
        public virtual Car Car { get; set; }
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
