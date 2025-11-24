using WebRazor.Services.API;
using WebRazor.Services.Authentication;
using WebRazor.Services.Authentication.Interfaces;

namespace WebRazor.Services.ServiceExtensions
{
    public static class IServiceExtensions
    {
        public static IServiceCollection AddTypedClientGeneric<T>(this IServiceCollection services, string prefix, string baseAddress) where T : class
        {
            services.AddHttpClient<HttpClientGeneric<T>>(options =>
            {
                options.BaseAddress = new Uri(baseAddress);
            }).AddHttpMessageHandler<AuthStateHandler>()
            .AddTypedClient((client, provider) => new HttpClientGeneric<T>(client, prefix));

            return services;
        }

        public static IServiceCollection AddTypedClientUser(this IServiceCollection services, string prefix, string baseAddress)
        {
            services.AddHttpClient<HttpClientUser>(options =>
            {
                options.BaseAddress = new Uri(baseAddress);
            }).AddHttpMessageHandler<AuthStateHandler>().AddTypedClient((client, serviceProvider) =>
            {
                ITokenHandler service = serviceProvider.GetRequiredService<ITokenHandler>();
                return new HttpClientUser(client, prefix, service);
            });

            return services;
        }
    }
}
