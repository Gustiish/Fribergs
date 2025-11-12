using Contracts.DTO;
using WebRazor.Services.ServiceExtensions;

namespace WebRazor
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddRazorPages();

            builder.Services.AddTypedClientAPI<CarDTO>("cars", builder.Configuration["WebserviceAddress"]);
            builder.Services.AddTypedClientAPI<CreateCarDTO>("cars", builder.Configuration["WebserviceAddress"]);
            builder.Services.AddTypedClientAPI<UserDTO>("users", builder.Configuration["WebserviceAddress"]);
            builder.Services.AddTypedClientAPI<CreateUserDTO>("users", builder.Configuration["WebserviceAddress"]);








            var app = builder.Build();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}


