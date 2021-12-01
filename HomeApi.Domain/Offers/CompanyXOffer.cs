using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HomeApi.Infrastructure;
using System.Xml.Serialization;
using HomeApi.Domain.DTO;
using System.IO;
using System.Xml;

namespace HomeApi.Domain
{
    public class CompanyXOffer : ExternalOffer, IOffer
    {
        private const string relativeUrl = "CompanyX/ProductOffer";
        protected new const string mediaType = "application/xml";
        public CompanyXOffer(IExternalApiClient apiClient) : base(apiClient)
        {

        }
        public async Task<OfferResponse> GetOffer(RequestModel request)
        {
            var inputRequest = GetMappedValue(request);
            var serializer = new System.Xml.Serialization.XmlSerializer(inputRequest.GetType());
            var resultOffer = await ExternalApiCall(relativeUrl, Serializing(serializer, inputRequest), mediaType);
            return resultOffer;


        }
        private CompanyXOfferRequest GetMappedValue(RequestModel request)
        {
            var lstPackages = new List<Package>();
            foreach (var dim in request.Dimensions)
            {
                var pkg = new Package() { Height = dim.Height, Width = dim.Width };
                lstPackages.Add(pkg);
            }

            return new CompanyXOfferRequest() { Source = request.SourceAddress, Destination = request.DestinationAddress, Packages = lstPackages };
        }
        private string Serializing(XmlSerializer serializer, CompanyXOfferRequest request)
        {
            XmlSerializerNamespaces ns1 = new XmlSerializerNamespaces();
            ns1.Add("", "http://schemas.datacontract.org/2004/07/OfferApi");
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;
            string serializeValue = string.Empty;
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww, settings))
                {
                    serializer.Serialize(writer, request, ns1);
                    serializeValue = sww.ToString();
                }
            }
            return serializeValue;
        }
    }
}
