using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.Users
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClientUser _client;
        [BindProperty]
        public CreateUserDTO CreateUserDTO { get; set; }

        public RegisterModel(HttpClientUser client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            bool success = await _client.RegisterAsync(CreateUserDTO);
            if (!success)
                return Page();
            return RedirectToPage("/Cars/Index");
        }


    }
}
