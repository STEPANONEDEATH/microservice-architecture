using Microsoft.Extensions.DependencyInjection;

namespace ExampleCore.HttpLogic
{
    public static class HttpServiceStartup
    {
        public static IServiceCollection AddHttpRequestService(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<IHttpRequestService, HttpRequestService>();
            return services;
        }
    }
}
