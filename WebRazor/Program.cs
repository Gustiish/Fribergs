using Contracts.DTO;
using WebRazor.Services.Authentication;
using WebRazor.Services.Authentication.Interfaces;
using WebRazor.Services.ServiceExtensions;

namespace WebRazor
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Console.WriteLine($"WS Address = {builder.Configuration["WebserviceAddress"]}");

            builder.Services.AddRazorPages();
            builder.Services.AddTransient<AuthStateHandler>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddTypedClientGeneric<CarDTO>("cars", builder.Configuration["WebserviceAddress"]!);
            builder.Services.AddTypedClientGeneric<UserDTO>("users", builder.Configuration["WebserviceAddress"]!);
            builder.Services.AddTypedClientUser("users", builder.Configuration["WebserviceAddress"]!);






            builder.Services.AddSession();



            var app = builder.Build();

            app.UseStaticFiles();

            app.UseSession();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}


