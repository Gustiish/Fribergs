using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.Cars
{
    public class EditModel : PageModel
    {
        private readonly HttpClientAPI _client;
        public CarDTO Car;

        public EditModel(HttpClientAPI client)
        {
            _client = client;
        }
        public async Task<IActionResult> OnGet(Guid id)
        {

        }

        public async Task<IActionResult> OnPost(CarDTO car)
        {

        }


    }
}
