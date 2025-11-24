

using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Authentication;
using ApplicationCore.Interfaces.Repository;
using Contracts.DTO;
using FluentValidation;
using Infrastructure.Database;
using Infrastructure.Services.Authentication;
using Infrastructure.Services.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Webservice.Endpoints;
using Webservice.Services.ExtensionMethods;
using Webservice.Services.MappingProfiles;
using Webservice.Services.Validators;

namespace Webservice
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddScoped<IValidator<CarDTO>, CarValidator>();
            builder.Services.AddScoped<IValidator<CreateCarDTO>, CreateCarValidator>();
            builder.Services.AddScoped<IValidator<UserDTO>, UserValidator>();
            builder.Services.AddScoped<IValidator<CreateUserDTO>, CreateUserValidator>();
            builder.Services.AddScoped<IValidator<CreateOrderDTO>, CreateOrderValidator>();
            builder.Services.AddScoped<IValidator<OrderDTO>, OrderValidator>();
            builder.Services.AddScoped<ITokenService, TokenService>();



            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"]
                };

            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOrCustomer", policy =>
                {
                    policy.RequireRole("Admin", "Customer");
                });

                options.AddPolicy("AdminAccess", policy =>
                {
                    policy.RequireRole("Admin");
                });
            });
            var app = builder.Build();

            await app.SeedRolesAsync();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.CarEndpoints();
            app.UserEndpoints();
            app.OrderEndpoints();

            app.Run();

        }
    }
}