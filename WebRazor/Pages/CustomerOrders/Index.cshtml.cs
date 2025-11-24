using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;

namespace WebRazor.Pages.CustomerOrders
{
    public class IndexModel : PageModel
    {
        private readonly HttpClientGeneric<OrderDTO> _client;
        public IndexModel(HttpClientGeneric<OrderDTO> client)
        {
            _client = client;
        }

        public List<OrderDTO> Orders { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var response = await _client.GetAllAsync();
            Orders = response.Data;
            return Page();
        }
    }
}
