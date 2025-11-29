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
        public string[]? Roles
        {
            get
            {
                var token = _context.HttpContext?.Session.GetString("JWT");
                if (string.IsNullOrEmpty(token))
                    return null;

                var jwt = Decode(token);

                return jwt.Claims
                    .Where(c => c.Type == ClaimTypes.Role || c.Type == "role" || c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                    .Select(c => c.Value)
                    .ToArray();
            }
        }
        public string? Email
        {
            get
            {
                var token = _context.HttpContext?.Session.GetString("JWT");
                if (string.IsNullOrEmpty(token))
                    return null;

                var jwt = Decode(token);


                return jwt.Claims.FirstOrDefault(c => c.Type == "email" ||
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            }
        }

        public string? UserId
        {
            get
            {
                var token = _context.HttpContext?.Session.GetString("JWT");
                if (string.IsNullOrEmpty(token))
                    return null;

                var jwt = Decode(token);

                return jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            }
        }

        public JwtSecurityToken Decode(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(token);
        }

        public bool IsExpired(string token)
        {
            var jwt = Decode(token);

            var exp = jwt.Payload.Expiration;

            if (exp == null)
                return true;

            var expiration = DateTimeOffset.FromUnixTimeSeconds(exp.Value);
            return expiration <= DateTimeOffset.UtcNow;
        }
    }
}
