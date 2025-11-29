using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Services.API;
using WebRazor.Services.PagesServices;

namespace WebRazor.Pages.CustomerOrders
{
    public class IndexModel : PageModel
    {
        private readonly HttpClientGeneric<OrderDTO> _client;
        private readonly JwtDecoder _decoder;
        public IndexModel(HttpClientGeneric<OrderDTO> client, JwtDecoder decoder)
        {
            _client = client;
            _decoder = decoder;
        }

        public List<OrderDTO>? Orders { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var response = await _client.GetAllAsync();
            if (!_decoder.Roles.Contains("Admin"))
            {
                Orders = response.Data.Where(o => o.UserId == Guid.Parse(_decoder.UserId)).ToList();
            }
            else
            {
                Orders = response.Data;
            }
            return Page();
        }
    }
}
