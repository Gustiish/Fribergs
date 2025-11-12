using ApplicationCore.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Webservice.Services.ExtensionMethods
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(this WebApplication app)
        {
            using var scoped = app.Services.CreateScope();
            var _roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var _userManager = scoped.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = new[] { "Admin", "Customer" };
            foreach (string role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole<Guid>() { Name = role });
            }

            if (await _userManager.FindByEmailAsync("admin@fribergs.com") is null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "admin@fribergs.com",
                    Email = "admin@fribergs.com",

                };
                var result = await _userManager.CreateAsync(user, "Admin123!");
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(user, "Admin");
            }



        }


    }
}
