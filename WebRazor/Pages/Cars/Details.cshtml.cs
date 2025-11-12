using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.Cars
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClientGeneric<CarDTO> _client;
        public CarDTO Car { get; set; }
        public DetailsModel(HttpClientGeneric<CarDTO> client)
        {
            _client = client;
        }
        public async Task<IActionResult> OnGet(Guid id)
        {
            Car = await _client.GetAsync(id);
            if (Car == null)
                return NotFound();
            return Page();
        }
    }
}
