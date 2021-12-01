using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Infrastructure
{
    public class ExternalApiClient : IExternalApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public ExternalApiClient(string baseUrl)
        {
            _baseUrl = baseUrl;

        }
        public ExternalApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<HttpResponseMessage> PostAsync(string relativeUrl,string request,string mediaType,string jwtToken="")
        {
            var httpClient = _httpClient == null ? new HttpClient() : _httpClient;
            AttachToken(httpClient, jwtToken);
            httpClient.BaseAddress = new Uri(_baseUrl);
            var response = await httpClient.PostAsync(relativeUrl,new StringContent(request,Encoding.UTF8, mediaType));
            return response;

        }
        private void AttachToken(HttpClient htpClient, string jwtToken)
        {
            if(!string.IsNullOrEmpty(jwtToken))
            {
                htpClient.DefaultRequestHeaders.Add("Authorization", jwtToken);
            }
        }
    }
}
