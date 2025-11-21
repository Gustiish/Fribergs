using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;

namespace WebRazor.Pages.Shared
{
    public class BasePageModel : PageModel
    {
        protected readonly IHttpContextAccessor _context;
        public string? Email { get; set; }
        public BasePageModel(IHttpContextAccessor httpContext)
        {
            _context = httpContext;
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
           
            string jwt = _context.HttpContext.Session.GetString("JWT");
            if (!string.IsNullOrEmpty(jwt))
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);

                Email = token.Claims.FirstOrDefault(c => c.Type == "email").ToString();
            }


            base.OnPageHandlerExecuting(context);
        }
    }
}
