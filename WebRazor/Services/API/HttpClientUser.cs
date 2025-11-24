using Contracts.DTO;
using Contracts.Services.Authentication;
using WebRazor.Services.Authentication.Interfaces;

namespace WebRazor.Services.API
{
    public class HttpClientUser
    {
        private readonly ITokenHandler _tokenService;
        private readonly HttpClient _client;
        private readonly string _prefix;
        public HttpClientUser(HttpClient client, string prefix, ITokenHandler tokenService)
        {
            _client = client;
            _prefix = prefix;
            _tokenService = tokenService;
        }

        public async Task<bool> LoginAsync(LoginUserDTO login)
        {
            var response = await _client.PostAsJsonAsync($"/{_prefix}/login", login);
            if (!response.IsSuccessStatusCode)
                return false;


            TokenResponse tokens = await response.Content.ReadFromJsonAsync<TokenResponse>();
            _tokenService.SetTokens(tokens);

            return true;
        }

        public async Task<bool> RegisterAsync(CreateUserDTO register)
        {
            var response = await _client.PostAsJsonAsync($"/{_prefix}/register", register);
            if (!response.IsSuccessStatusCode)
                return false;


            TokenResponse tokens = await response.Content.ReadFromJsonAsync<TokenResponse>();
            _tokenService.SetTokens(tokens);

            return true;
        }




    }
}
