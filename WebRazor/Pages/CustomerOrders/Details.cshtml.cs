using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.CustomerOrders
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClientGeneric<OrderDTO> _client;
        public OrderDTO Order { get; set; }

        public DetailsModel(HttpClientGeneric<OrderDTO> client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var response = await _client.GetAsync(id);

            if (response.Data == null)
                return NotFound();

            Order = response.Data;
            return Page();
        }
    }
}
