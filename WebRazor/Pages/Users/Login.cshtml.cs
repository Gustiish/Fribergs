using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly HttpClientUser _client;
        [BindProperty]
        public LoginUserDTO LoginUserDTO { get; set; }

        public LoginModel(HttpClientUser client)
        {
            _client = client;
        }
        public async Task<IActionResult> OnPost()
        {
            bool response = await _client.LoginAsync(LoginUserDTO);
            if (!response)
            {
                ViewData["Error"] = "Incorrect password or user";
                return Page();
            }



            return RedirectToPage("/Cars/Index");
        }
    }
}
