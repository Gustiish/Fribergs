using Contracts.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly HttpClientAPI _client;
        public List<BrandDTO> Brands { get; set; } = new();
        public CreateModel(HttpClientAPI client)
        {
            _client = client;
        }

        public async Task OnGet()
        {
            Brands = await _client.GetCarReferencesAsync();

        }
    }
}
