namespace WebRazor.Services.Authentication
{
    public class AuthStateHandler : DelegatingHandler
    {
        private readonly AuthState _state;
        private readonly IHttpContextAccessor _context;

        public AuthStateHandler(IHttpContextAccessor context)
        {
            _context = context;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpContext = _context.HttpContext;







            return base.SendAsync(request, cancellationToken);
        }




    }
}

