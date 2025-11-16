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
        internal LoginUserDTO LoginUserDTO { get; set; }

        public LoginModel(HttpClientUser client)
        {
            _client = client;
        }
        public async Task<IActionResult> OnPost()
        {
            var response = await _client.LoginAsync(LoginUserDTO);
            if (!response)
                return Page();

            return RedirectToAction("/cars/index");
        }
    }
}
