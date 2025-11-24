using Contracts.DTO;
using WebRazor.Services.Authentication;
using WebRazor.Services.Authentication.Interfaces;
using WebRazor.Services.PagesServices;
using WebRazor.Services.ServiceExtensions;

namespace WebRazor
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddRazorPages();
            builder.Services.AddTransient<AuthStateHandler>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ITokenHandler, TokenHandler>();
            builder.Services.AddScoped<JwtDecoder>();

            builder.Services.AddTypedClientGeneric<CarDTO>("cars", builder.Configuration["WebserviceAddress"]!);
            builder.Services.AddTypedClientGeneric<UserDTO>("users", builder.Configuration["WebserviceAddress"]!);
            builder.Services.AddTypedClientGeneric<OrderDTO>("orders", builder.Configuration["WebserviceAddress"]!);
            builder.Services.AddTypedClientUser("users", builder.Configuration["WebserviceAddress"]!);

            builder.Services.AddHttpClient("TokenRefreshClient", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["WebserviceAddress"]);
            });






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


