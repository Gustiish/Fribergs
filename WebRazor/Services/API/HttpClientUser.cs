using Contracts.DTO;
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
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{_client.BaseAddress}{_prefix}/login");
            var response = await _client.PostAsJsonAsync($"/{_prefix}/login", login);
            if (!response.IsSuccessStatusCode)
                return false;


            string token = await response.Content.ReadFromJsonAsync<string>();
            await _tokenService.SetTokenAsync(token);

            return true;
        }

        public async Task<bool> RegisterAsync(CreateUserDTO register)
        {
            var response = await _client.PostAsJsonAsync($"/{_prefix}/register", register);
            if (!response.IsSuccessStatusCode)
                return false;


            string token = await response.Content.ReadFromJsonAsync<string>();
            await _tokenService.SetTokenAsync(token);

            return true;
        }


    }
}
