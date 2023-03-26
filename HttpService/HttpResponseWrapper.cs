using System.Net;

namespace HttpService
{
    public class HttpResponseWrapper
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess => ((int)StatusCode >= 200) && ((int)StatusCode <= 299);

        public string? ErrorDetails { get; set; }
    }
}
