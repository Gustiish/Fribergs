using WebRazor.Services.API;
using WebRazor.Services.Authentication;
using WebRazor.Services.Authentication.Interfaces;

namespace WebRazor.Services.ServiceExtensions
{
    public static class IServiceExtensions
    {
        public static IServiceCollection AddTypedClientGeneric<T>(this IServiceCollection services, string prefix, string baseAddress) where T : class
        {
            services.AddHttpClient<HttpClientGeneric<T>>("BackendAPI", options =>
            {
                options.BaseAddress = new Uri(baseAddress);
            }).AddHttpMessageHandler<AuthStateHandler>().AddTypedClient(client => new HttpClientGeneric<T>(client, prefix));

            return services;
        }

        public static IServiceCollection AddTypedClientUser(this IServiceCollection services, string prefix, string baseAddress)
        {
            services.AddHttpClient<HttpClientUser>("BackendAPI", options =>
            {
                options.BaseAddress = new Uri(baseAddress);
            }).AddHttpMessageHandler<AuthStateHandler>().AddTypedClient((client, serviceProvider) =>
            {
                ITokenService service = serviceProvider.GetRequiredService<ITokenService>();
                return new HttpClientUser(client, prefix, service);
            });

            return services;
        }
    }
}
