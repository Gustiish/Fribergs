using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly HttpClientGeneric<UserDTO> _client;
        public List<UserDTO>? Users { get; set; }

        public IndexModel(HttpClientGeneric<UserDTO> client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnGet()
        {
            var apiResponse = await _client.GetAllAsync();
            if (!apiResponse.Success)
                Users = null;

            Users = apiResponse.Data;
        }

    }
}
