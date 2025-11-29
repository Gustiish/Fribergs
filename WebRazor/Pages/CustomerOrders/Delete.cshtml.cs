using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.CustomerOrders
{
    public class DeleteModel : PageModel
    {
        private readonly HttpClientGeneric<OrderDTO> _client;
        private readonly HttpClientGeneric<UserDTO> _userClient;
        private readonly HttpClientGeneric<CarDTO> _carClient;
        public OrderDTO Order { get; set; }
        public DeleteModel(HttpClientGeneric<OrderDTO> order, HttpClientGeneric<UserDTO> user, HttpClientGeneric<CarDTO> car)
        {
            _client = order;
            _userClient = user;
            _carClient = car;
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

        public async Task<IActionResult> OnPost(Guid id)
        {
            var response = await _client.DeleteAsync(id);
            if (!response)
                return Page();

            return RedirectToPage("./Index");
        }
    }
}
