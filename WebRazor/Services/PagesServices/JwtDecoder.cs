using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebRazor.Services.PagesServices
{
    public class JwtDecoder
    {
        private readonly IHttpContextAccessor _context;

        public JwtDecoder(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string? Email
        {
            get
            {
                var jwt = _context.HttpContext?.Session.GetString("JWT");
                if (string.IsNullOrEmpty(jwt))
                    return null;

                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);

                // Adjust the claim type based on your token
                return token.Claims.FirstOrDefault(c =>
                           c.Type == "email" ||
                           c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
                       )?.Value;
            }
        }

    }
}
