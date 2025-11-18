using System.Threading.Tasks;

namespace ExampleCore.HttpLogic
{
    public interface IHttpRequestService
    {
        Task<T?> GetAsync<T>(string url);
        Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest body);
    }
}
