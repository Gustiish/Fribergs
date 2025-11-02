using Contracts.DTO;

namespace WebRazor.Services.API
{
    public class HttpClientAPI
    {
        private readonly HttpClient _client;
        public HttpClientAPI(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<CarDTO>> GetAllCars()
        {
            var response = await _client.GetAsync("/cars/getall");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<List<CarDTO>>();
        }

        public async Task<CarDTO> GetCar(Guid id)
        {
            var response = await _client.GetAsync($"/cars/{id}");
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<CarDTO>();

        }

    }
}
