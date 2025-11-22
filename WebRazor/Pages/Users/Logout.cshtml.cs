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
        private readonly ITokenHandler _handler;
        private readonly IHttpContextAccessor _context;
        public LogoutModel(ITokenHandler handler, IHttpContextAccessor context)
        {
         
            _handler = handler;
            _context = context;
        }

        public async Task<IActionResult> OnPost()
        {
            await _handler.ClearToken();
            return RedirectToPage("/users/login");
        }
    }
}
