using Contracts.DTO;
using WebRazor.Services.Authentication.Interfaces;

namespace WebRazor.Services.API
{
    public class HttpClientUser
    {
        private readonly ITokenService _tokenService;
        private readonly HttpClientGeneric<UserDTO> _genericClient;
        private readonly HttpClient _client;
        private readonly string _prefix;
        public HttpClientUser(HttpClient client, string prefix, ITokenService tokenService)
        {
            _client = client;
            _prefix = prefix;
            _genericClient = new HttpClientGeneric<UserDTO>(client, prefix);
            _tokenService = tokenService;
        }

        public async Task<bool> LoginAsync(LoginUserDTO login)
        {
            var response = await _client.PostAsJsonAsync($"/{_prefix}/login", login);
            if (!response.IsSuccessStatusCode)
                return false;

            await _tokenService.SetTokenAsync(await response.Content.ReadAsStringAsync());

            return true;
        }

        public async Task<bool> RegisterAsync(CreateUserDTO register)
        {
            var response = await _client.PostAsJsonAsync($"/{_prefix}/register", register);
            if (!response.IsSuccessStatusCode)
                return false;

            await _tokenService.SetTokenAsync(await response.Content.ReadAsStringAsync());

            return true;
        }


    }
}
