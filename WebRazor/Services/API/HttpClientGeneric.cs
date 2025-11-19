using Contracts.Services;

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

        public async Task<ApiResponse<List<T>>?> GetAllAsync()
        {
            var response = await _client.GetAsync($"/{_prefix}/getall");
            return await response.Content.ReadFromJsonAsync<ApiResponse<List<T>>>();
        }

        public async Task<ApiResponse<T>?> GetAsync(Guid id)
        {
            var response = await _client.GetAsync($"/{_prefix}/{id}");
            return await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
        }

        public async Task<ApiResponse<T>?> UpdateAsync(T entity, Guid id)
        {
            var response = await _client.PatchAsJsonAsync($"/{_prefix}/{id}", entity);
            return await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
        }

        public async Task<ApiResponse<T>?> DeleteAsync(Guid id)
        {
            var response = await _client.DeleteAsync($"/{_prefix}/{id}");
            return await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
        }
        public async Task<ApiResponse<T>?> CreateAsync<TCreate>(TCreate entity) where TCreate : class
        {
            var response = await _client.PostAsJsonAsync<TCreate>($"/{_prefix}", entity);
            return await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
        }


    }
}
