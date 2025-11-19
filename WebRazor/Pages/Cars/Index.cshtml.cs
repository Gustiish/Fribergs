using Contracts.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;
using Contracts.Services;

namespace WebRazor.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly HttpClientGeneric<CarDTO> _client;
        public List<CarDTO> Cars = [];
        public IndexModel(HttpClientGeneric<CarDTO> client)
        {
            _client = client;
        }


        public async Task OnGet()
        {
            ApiResponse<List<CarDTO>> response = await _client.GetAllAsync();
            Cars = response.Data;
        }
    }
}
