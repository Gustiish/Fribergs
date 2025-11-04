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

        public async Task<CarDTO> GetCarAsync(Guid id)
        {
            var response = await _client.GetAsync($"/cars/{id}");
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<CarDTO>();

        }

        public async Task<bool> UpdateCarAsync(CarDTO car)
        {
            var response = await _client.PatchAsJsonAsync($"/cars/{car.Id}", car);
            if (!response.IsSuccessStatusCode)
                return false;
            return true;
        }

        public async Task<bool> DeleteCarAsync(Guid id)
        {
            var response = await _client.DeleteAsync($"/cars/{id}");
            if (!response.IsSuccessStatusCode)
                return false;
            return true;
        }

        public async Task<List<Brand>> GetCarReferencesAsync()
        {

        }

    }
}
