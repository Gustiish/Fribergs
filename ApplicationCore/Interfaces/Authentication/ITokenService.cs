using ApplicationCore.Entities.Identity;
using ApplicationCore.Records;

namespace ApplicationCore.Interfaces.Authentication
{
    public interface ITokenService
    {
        Task<TokenPair> GenerateInitalTokenAsync(ApplicationUser user);

        Task<TokenPair> RotateTokenAsync(string refreshToken);
    }
}
