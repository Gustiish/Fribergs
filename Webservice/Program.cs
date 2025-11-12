

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
using Webservice.Modules;
using Webservice.Modules.CarModule;
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

            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

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
            builder.Services.AddScoped<ITokenService, TokenService>();

            Console.WriteLine("[JWT VALIDATION KEY]" + builder.Configuration["Jwt:Key"]);


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
                    ValidAudience = builder.Configuration["Jwt:Audience"],

                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var header = context.Request.Headers["Authorization"].FirstOrDefault();
                        Console.WriteLine($"[AUTH HEADER RAW] {header}");
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"[JWT FAIL] {context.Exception.Message}");
                        Console.WriteLine($"{context.Exception.InnerException.Message}");
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Console.WriteLine($"[JWT CHALLENGE] {context.ErrorDescription}");
                        return Task.CompletedTask;
                    }
                };

            });

            builder.Services.AddAuthorizationBuilder().AddPolicy("AdminAccess", policy =>
            {
                policy.RequireRole("Admin");
            });
            var app = builder.Build();

            await app.SeedRolesAsync();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                // Optional: only log for authenticated endpoints
                var path = context.Request.Path.Value;
                if (!path.Contains("login", StringComparison.OrdinalIgnoreCase))
                {
                    var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                    Console.WriteLine($"[AUTH HEADER] {authHeader}");
                }
                await next.Invoke();
            });

            app.CarEndpoints();
            app.UserEndpoints();

            app.Run();

        }
    }
}