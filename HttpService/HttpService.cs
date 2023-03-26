using System.Text;

namespace HttpService
{
    public class HttpService : IHttpService
    {
        private string _mediaType;
        private HttpClient _httpClient;
        private ISerializationHelper _serializer;

        public HttpService(HttpClient httpClient, ISerializationHelper serializationHelper, string mediaType)
        {
            _httpClient = httpClient;
            _serializer = serializationHelper;
            _mediaType = mediaType;
        }

        public async Task<HttpDataResponseWrapper<T>> Get<T>(string uri, Dictionary<string, IEnumerable<string>>? headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            AddHeaders(ref request, headers);
            return await SendRequest<T>(request);
        }


        private void AddHeaders(ref HttpRequestMessage request, Dictionary<string, IEnumerable<string>>? headers = null)
        {
            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
        }

        public Task<HttpDataResponseWrapper<T>> Post<T>(string uri, object value, Dictionary<string, IEnumerable<string>>? headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            AddHeaders(ref request, headers);
            request.Content = new StringContent(_serializer.Serialize(value), Encoding.UTF8, _mediaType);
            return SendRequest<T>(request);
        }

        public Task<HttpDataResponseWrapper<T>> Put<T>(string uri, object value, Dictionary<string, IEnumerable<string>>? headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            AddHeaders(ref request, headers);
            request.Content = new StringContent(_serializer.Serialize(value), Encoding.UTF8, _mediaType);
            return SendRequest<T>(request);
        }

        public Task Put(string uri, object value, Dictionary<string, IEnumerable<string>>? headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            AddHeaders(ref request, headers);
            request.Content = new StringContent(_serializer.Serialize(value), Encoding.UTF8, _mediaType);
            return SendRequest(request);
        }

        public Task Delete(string uri, Dictionary<string, IEnumerable<string>>? headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            AddHeaders(ref request, headers);
            return SendRequest<int>(request);
        }

        public Task Post(string uri, object value, Dictionary<string, IEnumerable<string>>? headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            AddHeaders(ref request, headers);
            request.Content = new StringContent(_serializer.Serialize(value), Encoding.UTF8, _mediaType);
            return SendRequest(request);
        }


        private async Task<HttpDataResponseWrapper<T>> SendRequest<T>(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return new HttpDataResponseWrapper<T>()
                {
                    StatusCode = response.StatusCode,
                    ErrorDetails = await response.Content.ReadAsStringAsync()
                };
            }

            return new HttpDataResponseWrapper<T>()
            {
                Data = _serializer.Deserialize<T>(await response.Content.ReadAsStringAsync()),
                StatusCode = response.StatusCode,
            };
        }

        private async Task SendRequest(HttpRequestMessage request)
        {

            var response = await _httpClient.SendAsync(request);

            var resut = new HttpResponseWrapper()
            {
                StatusCode = response.StatusCode,
            };

            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {
                resut.ErrorDetails = await response.Content.ReadAsStringAsync();
            }
        }

      
    }
}
