using WebRazor.Services.API;

namespace WebRazor
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddRazorPages();
            builder.Services.AddHttpClient<HttpClientAPI>("BackendAPI", options =>
            {
                options.BaseAddress = new Uri(builder.Configuration["WebserviceAddress"]);
            });




            var app = builder.Build();

            // Configure the HTTP request pipeline.


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


