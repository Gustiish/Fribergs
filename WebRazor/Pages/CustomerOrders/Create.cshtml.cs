using Contracts.DTO;
using Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.CustomerOrders
{
    public class CreateModel : PageModel
    {
        private readonly HttpClientGeneric<OrderDTO> _client;
        private readonly HttpClientGeneric<CarDTO> _carClient;
        [BindProperty]
        public CreateOrderDTO Order { get; set; } = new CreateOrderDTO();
        public CreateModel(HttpClientGeneric<OrderDTO> client, HttpClientGeneric<CarDTO> carClient)
        {
            _client = client;
            _carClient = carClient;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var carResponse = await _carClient.GetAsync(id);

            if (carResponse.Data == null)
                return NotFound();

            Order.CarId = id;

            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            ApiResponse<CreateOrderDTO> response = await _client.CreateAsync<CreateOrderDTO>(Order);
            if (!response.Success)
            {
                return BadRequest($"Failed to create, statuscode: {response.StatusCode}");
            }

            return RedirectToPage("./Index");
        }
    }
}
