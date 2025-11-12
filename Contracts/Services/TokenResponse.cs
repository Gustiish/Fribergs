namespace Contracts.Services.Authentication
{
    public record TokenResponse
    {
        public string Token { get; }
        public TokenResponse(string token)
        {
            Token = token;
        }
    }
}
