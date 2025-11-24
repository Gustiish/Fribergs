using Contracts.Services.Authentication;

namespace WebRazor.Services.Authentication.Interfaces
{
    public interface ITokenHandler
    {
        void SetTokens(TokenResponse tokens);
        string GetAccessToken();
        string GetRefreshToken();
        void ClearToken();
        Task<TokenResponse> RefreshTokensAsync(string refreshToken);

    }

}

