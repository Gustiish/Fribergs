using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;
using Contracts.Services;

namespace WebRazor.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly HttpClientGeneric<CarDTO> _client;
        [BindProperty]
        public CreateCarDTO CreateCarDTO { get; set; } = new();

        public CreateModel(HttpClientGeneric<CarDTO> client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnPost()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(_client._client.BaseAddress);
            Console.WriteLine($"{_client._client.BaseAddress}{_client._prefix}");
            

            ApiResponse<CreateCarDTO> response = await _client.CreateAsync<CreateCarDTO>(CreateCarDTO);
            if (!response.Success)
            {
                return BadRequest($"Failed to create, statuscode: {response.StatusCode}");
            }

            return RedirectToPage("./Index");
        }
    }
}
