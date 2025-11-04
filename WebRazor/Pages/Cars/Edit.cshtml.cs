using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.Cars
{
    public class EditModel : PageModel
    {
        private readonly HttpClientAPI _client;
        [BindProperty]
        public CarDTO Car { get; set; }

        public EditModel(HttpClientAPI client)
        {
            _client = client;
        }
        public async Task<IActionResult> OnGet(Guid id)
        {
            Car = await _client.GetCarAsync(id);
            if (Car is null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            bool success = await _client.UpdateCarAsync(Car);
            if (!success)
            {
                return Page();
            }
            return Page();

        }


    }
}
