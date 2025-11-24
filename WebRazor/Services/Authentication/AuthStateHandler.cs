using System.Net.Http.Headers;
using WebRazor.Services.Authentication.Interfaces;
using WebRazor.Services.PagesServices;

namespace WebRazor.Services.Authentication
{
    public class AuthStateHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _context;
        private readonly ITokenHandler _handler;
        private readonly JwtDecoder _decoder;


        public AuthStateHandler(IHttpContextAccessor context, ITokenHandler handler, JwtDecoder decoder)
        {
            _context = context;
            _handler = handler;
            _decoder = decoder;
        }
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {


            string accessToken = _context.HttpContext.Session.GetString("JWT");
            string refreshToken = _context.HttpContext.Session.GetString("RefreshToken");

            bool needsRefresh = false;
            if (string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(refreshToken))
            {
                needsRefresh = true;
            }
            else if (!string.IsNullOrEmpty(accessToken))
            {
                needsRefresh = _decoder.IsExpired(accessToken);
            }

            if (needsRefresh && !string.IsNullOrEmpty(refreshToken))
            {
                var newTokens = await _handler.RefreshTokensAsync(refreshToken);

                if (newTokens != null)
                {
                    _handler.SetTokens(newTokens);
                    accessToken = newTokens.AccessToken;
                    refreshToken = newTokens.RefreshToken;
                }
                else
                {

                    _handler.ClearToken();
                }
            }

            if (!string.IsNullOrEmpty(accessToken))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            if (!string.IsNullOrEmpty(refreshToken))
                request.Headers.Add("X-Refresh-Token", refreshToken);

            return await base.SendAsync(request, cancellationToken);
        }



    }
}

