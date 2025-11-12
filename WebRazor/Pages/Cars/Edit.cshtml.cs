using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

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
            Car = await _client.GetAsync(id);
            if (Car is null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            bool success = await _client.UpdateAsync(Car, Car.Id);
            if (!success)
            {
                return Page();
            }
            return RedirectToPage("./Index");

        }


    }
}
