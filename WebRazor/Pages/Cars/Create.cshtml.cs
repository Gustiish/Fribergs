using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly HttpClientGeneric<CreateCarDTO> _client;
        [BindProperty]
        public CreateCarDTO CreateCarDTO { get; set; } = new();

        public CreateModel(HttpClientGeneric<CreateCarDTO> client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnPost()
        {
            bool success = await _client.CreateAsync(CreateCarDTO);
            if (!success)
                return BadRequest("Failed to create");

            return RedirectToPage("./Index");
        }
    }
}
