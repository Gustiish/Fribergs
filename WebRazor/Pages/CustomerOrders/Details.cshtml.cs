using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.CustomerOrders
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClientGeneric<OrderDTO> _client;
        private readonly HttpClientGeneric<CarDTO> _carClient;
        private readonly HttpClientGeneric<UserDTO> _userClient;

        public OrderDTO Order { get; set; }

        public DetailsModel(HttpClientGeneric<OrderDTO> client, HttpClientGeneric<CarDTO> carClient, HttpClientGeneric<UserDTO> userClient)
        {
            _client = client;
            _carClient = carClient;
            _userClient = userClient;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var response = await _client.GetAsync(id);
            if (!response.Success)
                return NotFound();
            var carResponse = await _carClient.GetAsync(response.Data.CarId);
            var userResponse = await _userClient.GetAsync(response.Data.UserId);
            response.Data.Car = carResponse.Data;
            response.Data.User = userResponse.Data;
            Order = response.Data;
            return Page();
        }
    }
}
