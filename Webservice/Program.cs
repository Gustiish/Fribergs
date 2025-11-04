

using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repository;
using Contracts.DTO;
using FluentValidation;
using Infrastructure.Database;
using Infrastructure.Services.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Webservice.Modules.CarModule;
using Webservice.Services.MappingProfiles;
using Webservice.Services.Validators;

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
            builder.Services.AddScoped<ICarReference, CarReference>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddScoped<IValidator<CarDTO>, CarValidator>();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.CarEndpoints();

            app.Run();

        }
    }
}