using Contracts.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly HttpClientAPI _client;
        public List<CarDTO> Cars = [];
        public IndexModel(HttpClientAPI client)
        {
            _client = client;

        }


        public async Task OnGet()
        {
            List<CarDTO> cars = await _client.GetAllCars();
            Cars = cars;
        }
    }
}
