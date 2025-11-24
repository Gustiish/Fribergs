using Contracts.Services;
using Contracts.Services.Authentication;
using WebRazor.Services.Authentication.Interfaces;

namespace WebRazor.Services.Authentication
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IHttpContextAccessor _context;
        private readonly HttpClient _refreshClient;
        public TokenHandler(IHttpContextAccessor context, IHttpClientFactory factory)
        {
            _context = context;
            _refreshClient = factory.CreateClient("TokenRefreshClient");
        }

        public void ClearToken()
        {
            _context.HttpContext.Session.Clear();
        }

        public string GetAccessToken()
        {
            return _context.HttpContext.Session.GetString("JWT");
        }

        public string GetRefreshToken()
        {
            return _context.HttpContext.Session.GetString("RefreshToken");
        }

        public async Task<TokenResponse> RefreshTokensAsync(string refreshToken)
        {
            RefreshTokenRequest request = new RefreshTokenRequest(refreshToken);
            var response = await _refreshClient.PostAsJsonAsync<RefreshTokenRequest>($"/users/refresh", request);
            TokenResponse tokens = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return tokens;
        }

        public void SetTokens(TokenResponse tokens)
        {
            _context.HttpContext.Session.SetString("JWT", tokens.AccessToken);
            _context.HttpContext.Session.SetString("RefreshToken", tokens.RefreshToken);
        }
    }
}
