using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;
using Contracts.Services;

namespace WebRazor.Pages.Cars
{
    public class EditModel : PageModel
    {
        private readonly HttpClientGeneric<CarDTO> _client;
        [BindProperty]
        public CarDTO Car { get; set; }

        public EditModel(HttpClientGeneric<CarDTO> client)
        {
            _client = client;
        }
        public async Task<IActionResult> OnGet(Guid id)
        {
            ApiResponse<CarDTO> response = await _client.GetAsync(id);
            if (response.Data is null)
                return NotFound();
            Car = response.Data;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            ApiResponse<CarDTO> response = await _client.UpdateAsync(Car, Car.Id);
            if (!response.Success)
            {
                return Page();
            }
            return RedirectToPage("./Index");

        }


    }
}
