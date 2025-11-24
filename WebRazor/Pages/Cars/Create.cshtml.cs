using Contracts.DTO;
using Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly HttpClientGeneric<CarDTO> _client;
        [BindProperty]
        public CreateCarDTO CreateCarDTO { get; set; } = new();

        public CreateModel(HttpClientGeneric<CarDTO> client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            ApiResponse<CreateCarDTO> response = await _client.CreateAsync<CreateCarDTO>(CreateCarDTO);
            if (!response.Success)
            {
                return BadRequest($"Failed to create, statuscode: {response.StatusCode}");
            }

            return RedirectToPage("./Index");
        }
    }
}
