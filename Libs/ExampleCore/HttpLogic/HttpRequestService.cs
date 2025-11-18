using ExampleCore.TraceIdLogic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ExampleCore.HttpLogic
{
    public class HttpRequestService : IHttpRequestService
    {
        private readonly IHttpClientFactory _factory;
        private readonly ITraceIdAccessor _trace;

        public HttpRequestService(IHttpClientFactory factory, ITraceIdAccessor trace)
        {
            _factory = factory;
            _trace = trace;
        }

        private HttpClient CreateClient()
        {
            var client = _factory.CreateClient();
            if (!string.IsNullOrEmpty(_trace.TraceId))
            {
                if (!client.DefaultRequestHeaders.Contains("X-Trace-Id"))
                    client.DefaultRequestHeaders.Add("X-Trace-Id", _trace.TraceId);
            }
            return client;
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            var client = CreateClient();
            var res = await client.GetAsync(url);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<T>();
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest body)
        {
            var client = CreateClient();
            var res = await client.PostAsJsonAsync(url, body);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<TResponse>();
        }
    }
}
