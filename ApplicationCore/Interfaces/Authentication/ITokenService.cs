using ApplicationCore.Entities.Identity;

namespace ApplicationCore.Interfaces.Authentication
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}
