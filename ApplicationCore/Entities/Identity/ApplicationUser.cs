using ApplicationCore.Interfaces.Entity;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity
    {

    }
}
