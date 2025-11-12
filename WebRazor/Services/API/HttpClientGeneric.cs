namespace WebRazor.Services.API
{
    public class HttpClientGeneric<T> where T : class
    {
        private readonly HttpClient _client;
        private readonly string _prefix;

        public HttpClientGeneric(HttpClient client, string prefix)
        {
            _prefix = prefix;
            _client = client;
        }

        public async Task<List<T>?> GetAllAsync()
        {
            var response = await _client.GetAsync($"/{_prefix}/getall");
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<List<T>>();

        }

        public async Task<T?> GetAsync(Guid id)
        {
            var response = await _client.GetAsync($"/{_prefix}/{id}");
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<bool> UpdateAsync(T entity, Guid id)
        {
            var response = await _client.PatchAsJsonAsync($"/{_prefix}/{id}", entity);
            if (!response.IsSuccessStatusCode)
                return false;
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _client.DeleteAsync($"/{_prefix}/{id}");
            if (!response.IsSuccessStatusCode)
                return false;
            return true;
        }
        public async Task<bool> CreateAsync(T entity)
        {
            var response = await _client.PostAsJsonAsync<T>($"/{_prefix}", entity);
            if (!response.IsSuccessStatusCode)
                return false;
            return true;
        }


    }
}
