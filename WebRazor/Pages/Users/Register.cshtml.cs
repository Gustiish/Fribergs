using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;
using WebRazor.Services.Authentication;

namespace WebRazor.Pages.Users
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClientGeneric<CreateUserDTO> _client;
        private readonly AuthState _state;
        [BindProperty]
        public CreateUserDTO CreateUserDTO { get; set; }

        public async Task<IActionResult> OnPost()
        {
            bool success = await _client.CreateAsync(CreateUserDTO);
            if (!success)
                return Page();

            //_state.Token =


            return RedirectToPage("/users/index");
        }


    }
}
