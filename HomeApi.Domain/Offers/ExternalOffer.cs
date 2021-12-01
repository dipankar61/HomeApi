
using HomeApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using HomeApi.Domain.DTO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HomeApi.Domain
{
    public class ExternalOffer
    {
        protected readonly IExternalApiClient _apiClient;
        protected const string mediaType = "application/json";
        
        public ExternalOffer(IExternalApiClient apiClient)
        {
            _apiClient = apiClient;
          
        }  
        
        protected virtual async Task<OfferResponse> ExternalApiCall(string relativeUrl, string request, string mediaType)
        {
            var result = await _apiClient.PostAsync(relativeUrl, request, mediaType);
            if (result.IsSuccessStatusCode)
            {
                var resultString = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OfferResponse>(resultString);
            }
            else
            {
                return new OfferResponse() { Price = 0 };
            }
        }

    }
}
