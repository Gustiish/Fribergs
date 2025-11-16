using WebRazor.Services.Authentication.Interfaces;

namespace WebRazor.Services.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly IHttpContextAccessor _context;
        public TokenService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public Task ClearToken()
        {
            _context.HttpContext.Session.Clear();
            return Task.CompletedTask;
        }

        public async Task<string> GetTokenAsync()
        {
            return _context.HttpContext.Session.GetString("JWT");
        }

        public Task SetTokenAsync(string token)
        {
            _context.HttpContext!.Session.SetString("JWT", token);
            return Task.CompletedTask;
        }
    }
}
