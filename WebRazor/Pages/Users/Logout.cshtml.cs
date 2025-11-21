using Contracts.DTO;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebRazor.Services.API;
using WebRazor.Services.Authentication.Interfaces;

namespace WebRazor.Pages.Users
{
    public class LogoutModel : PageModel
    {
        private readonly HttpClientUser _client;
        private readonly ITokenHandler _handler;
        private readonly IHttpContextAccessor _context;
        public string Email { get; set; }
        public LogoutModel(HttpClientUser client, ITokenHandler handler, IHttpContextAccessor context)
        {
            _client = client;
            _handler = handler;
            _context = context;
        }
        public void OnGet()
        {
            string JWT = _context.HttpContext.Session.GetString("JWT");
            var JwtHandler = new JwtSecurityTokenHandler();
            var token = JwtHandler.ReadJwtToken(JWT);


            Email = token.Claims.FirstOrDefault(c => c.Type == "Email").ToString();

        }

        public async Task<IActionResult> OnPost()
        {
            await _handler.ClearToken();
            return RedirectToPage("/users/login");
        }
    }
}
