using Contracts.DTO;
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
            Car = await _client.GetAsync(id);
            if (Car == null)
                return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            bool success = await _client.DeleteAsync(id);
            if (!success)
                return Page();
            return RedirectToPage("./Index");
        }
    }
}
