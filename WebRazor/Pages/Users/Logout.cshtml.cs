using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.Authentication.Interfaces;

namespace WebRazor.Pages.Users
{
    public class LogoutModel : PageModel
    {
        private readonly ITokenHandler _handler;
        public LogoutModel(ITokenHandler handler)
        {
            _handler = handler;
        }

        public async Task<IActionResult> OnPost()
        {
            _handler.ClearToken();
            return RedirectToPage("/users/login");
        }
    }
}
