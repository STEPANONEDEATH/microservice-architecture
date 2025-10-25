using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ExampleCore.TraceIdLogic
{
    public class TraceIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HeaderName = "X-Trace-Id";
        private readonly ITraceIdAccessor _accessor;

        public TraceIdMiddleware(RequestDelegate next, ITraceIdAccessor accessor)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(HeaderName, out var incoming) && !string.IsNullOrWhiteSpace(incoming))
            {
                _accessor.TraceId = incoming;
            }
            else
            {
                _accessor.TraceId = Guid.NewGuid().ToString("N");
                context.Request.Headers[HeaderName] = _accessor.TraceId;
            }

            context.Response.OnStarting(() =>
            {
                if (!context.Response.Headers.ContainsKey(HeaderName) && _accessor.TraceId is not null)
                    context.Response.Headers.Add(HeaderName, _accessor.TraceId);
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
