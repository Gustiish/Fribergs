using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClientGeneric<UserDTO> _client;
        public UserDTO User { get; set; }
        public DetailsModel(HttpClientGeneric<UserDTO> client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var response = await _client.GetAsync(id);
            if (!response.Success)
                return NotFound();
            User = response.Data;
            return Page();
        }
    }
}
