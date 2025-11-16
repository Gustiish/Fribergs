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

        public async Task<IActionResult> OnPost()
        {
            bool success = await _client.RegisterAsync(CreateUserDTO);
            if (!success)
                return Page();



            return RedirectToPage("/users/index");
        }


    }
}
