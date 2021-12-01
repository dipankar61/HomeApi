using HomeApi.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HomeApi.Infrastructure;
using Newtonsoft.Json;

namespace HomeApi.Domain
{
    public class CompanyAOffer : ExternalOffer, IOffer
    {
       
        private const string relativeUrl = "CompanyA/ProductOffer";
        public CompanyAOffer(IExternalApiClient apiClient):base(apiClient)
        {
           
        }
        public async Task<OfferResponse> GetOffer(RequestModel request)
        {
           
            var inputRequest = GetMappedValue(request);
            var jsonString = JsonConvert.SerializeObject(inputRequest);
            var resultOffer = await ExternalApiCall(relativeUrl, jsonString, mediaType);
            return resultOffer;


        }
        private CompanyAOfferRquest GetMappedValue(RequestModel request)
        {

            return new CompanyAOfferRquest() { ContactAddress = request.SourceAddress, WareHouseAddress = request.DestinationAddress, Dimensions = request.Dimensions };
        }
    }
}
