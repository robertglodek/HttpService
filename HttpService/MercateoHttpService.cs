using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace HttpService
{
    public class MercateoHttpService : HttpService
    {

        public MercateoHttpService(HttpClient httpClient) : base(httpClient, new JsonSerializationHelper(), MediaTypeNames.Application.Json)
        {

        }

        public async Task<HttpDataResponseWrapper<string>> GetById(Guid id, Guid sectionId)
        {
            return await Get<string>("siema", new Dictionary<string, IEnumerable<string>>());
        }
    }
}
