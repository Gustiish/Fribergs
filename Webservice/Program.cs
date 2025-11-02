

using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Repository;
using Infrastructure.Database;
using Infrastructure.Services.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Webservice.Modules.CarModule;
using Webservice.Services.MappingProfiles;

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
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.CarEndpoints();

            app.Run();

        }
    }
}