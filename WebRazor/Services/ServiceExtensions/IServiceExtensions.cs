using WebRazor.Services.API;
using WebRazor.Services.Authentication;

namespace WebRazor.Services.ServiceExtensions
{
    public static class IServiceExtensions
    {
        public static IServiceCollection AddTypedClientAPI<T>(this IServiceCollection services, string prefix, string baseAddress) where T : class
        {
            services.AddHttpClient<HttpClientGeneric<T>>("BackendAPI", options =>
            {
                options.BaseAddress = new Uri(baseAddress);
            }).AddTypedClient(client => new HttpClientGeneric<T>(client, prefix))
            .AddHttpMessageHandler<AuthStateHandler>();

            return services;
        }
    }
}
