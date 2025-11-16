using System.Net.Http.Headers;

namespace WebRazor.Services.Authentication
{
    public class AuthStateHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _context;

        public AuthStateHandler(IHttpContextAccessor context)
        {
            _context = context;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwt = _context.HttpContext.Session.GetString("JWT");
            if (!string.IsNullOrEmpty(jwt))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            return base.SendAsync(request, cancellationToken);
        }




    }
}

