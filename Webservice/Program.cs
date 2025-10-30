

using ApplicationCore.Entities.Identity;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Webservice
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>().AddEntityFrameworkStores<ApplicationDbContext>();

            var app = builder.Build();


        }
    }
}