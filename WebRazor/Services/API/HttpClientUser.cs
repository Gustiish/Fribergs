namespace WebRazor.Services.API
{
    public class HttpClientUser<T> : HttpClientGeneric<T> where T : class
    {
        public HttpClientUser(HttpClient client, string prefix) : base(client, prefix)
        {
        }

        //public async Task<TokenResponse> Login(T entity)
        //{

        //}

        //public async Task<> Register(T entity)
        //{

        //}


    }
}
