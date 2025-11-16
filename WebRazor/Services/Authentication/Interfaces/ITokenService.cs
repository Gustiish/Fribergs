namespace WebRazor.Services.Authentication.Interfaces
{
    public interface ITokenService
    {
        Task SetTokenAsync(string token);
        Task<string> GetTokenAsync();
        Task ClearToken();


    }

}

