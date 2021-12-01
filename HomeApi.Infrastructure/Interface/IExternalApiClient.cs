using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Infrastructure
{
    public interface IExternalApiClient
    {
        Task<HttpResponseMessage> PostAsync(string relativeUrl, string request, string mediaType, string jwtToken = "");
    }
}
