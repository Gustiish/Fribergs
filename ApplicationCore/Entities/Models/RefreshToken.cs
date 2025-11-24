using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Entity;

namespace ApplicationCore.Entities.Models
{
    public class RefreshToken : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime Expires { get; set; }
        public bool IsInUse { get; set; }
        public bool IsRevoked { get; set; }

    }
}
