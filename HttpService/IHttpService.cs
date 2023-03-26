

namespace HttpService
{
    public interface IHttpService
    {
        Task<HttpDataResponseWrapper<T>> Get<T>(string uri, Dictionary<string, IEnumerable<string>>? headers = null);
        Task<HttpDataResponseWrapper<T>> Post<T>(string uri, object value, Dictionary<string, IEnumerable<string>>? headers = null);
        Task Post(string uri, object value, Dictionary<string, IEnumerable<string>>? headers = null);
        Task<HttpDataResponseWrapper<T>> Put<T>(string uri, object value, Dictionary<string, IEnumerable<string>>? headers = null);
        Task Put(string uri, object value, Dictionary<string, IEnumerable<string>>? headers = null);
        Task Delete(string uri, Dictionary<string, IEnumerable<string>>? headers = null);
    }
}
