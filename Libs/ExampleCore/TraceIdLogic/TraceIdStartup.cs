using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleCore.TraceIdLogic
{
    public static class TraceIdStartup
    {
        public static IServiceCollection AddTraceId(this IServiceCollection services)
        {
            services.AddSingleton<ITraceIdAccessor, TraceIdAccessor>();
            return services;
        }

        public static IApplicationBuilder UseTraceId(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TraceIdMiddleware>();
        }
    }
}
