using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HomeApi.Infrastructure;
using Newtonsoft.Json;
using HomeApi.Domain.DTO;

namespace HomeApi.Domain
{
    public class CompanyBOffer: ExternalOffer, IOffer
    {
        private const string relativeUrl = "CompanyB/ProductOffer";
        public CompanyBOffer(IExternalApiClient apiClient) : base(apiClient)
        {

        }
        public async Task<OfferResponse> GetOffer(RequestModel request)
        {
            var inputRequest = GetMappedValue(request);
            var jsonString = JsonConvert.SerializeObject(inputRequest);
            var resultOffer = await ExternalApiCall(relativeUrl, jsonString, mediaType);
            return resultOffer;


        }
        private CompanyBOfferRequest GetMappedValue(RequestModel request)
        {

            return new CompanyBOfferRequest() { Consignee = request.SourceAddress, Consignor = request.DestinationAddress, Cartons = request.Dimensions };
        }
    }
}
