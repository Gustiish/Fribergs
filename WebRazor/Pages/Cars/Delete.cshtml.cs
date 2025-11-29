using Contracts.DTO;
using Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.Cars
{
    public class DeleteModel : PageModel
    {
        private readonly HttpClientGeneric<CarDTO> _client;
        public CarDTO Car { get; set; }
        public DeleteModel(HttpClientGeneric<CarDTO> client)
        {
            _client = client;
        }
        public async Task<IActionResult> OnGet(Guid id)
        {
            ApiResponse<CarDTO> response = await _client.GetAsync(id);
            Car = response.Data;
            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            var response = await _client.DeleteAsync(id);
            if (!response)
                return Page();

            return RedirectToPage("./Index");
        }
    }
}
