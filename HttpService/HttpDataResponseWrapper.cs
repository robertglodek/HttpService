
namespace HttpService
{
    public class HttpDataResponseWrapper<T>: HttpResponseWrapper
    {
        public T? Data { get; init; }  
    }
}
