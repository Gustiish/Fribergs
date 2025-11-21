namespace WebRazor.Services.Authentication.Interfaces
{
    public interface ITokenHandler
    {
        Task SetTokenAsync(string token);
        Task<string> GetTokenAsync();
        Task ClearToken();


    }

}

