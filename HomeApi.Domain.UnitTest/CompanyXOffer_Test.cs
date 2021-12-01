using HomeApi.Domain.DTO;
using HomeApi.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HomeApi.Domain.UnitTest
{
    [TestClass]
    public class CompanyXOffer_Test
    {
        private Mock<IExternalApiClient> _apiClient;

        [TestInitialize]
        public void Initialize()
        {
            _apiClient = new Mock<IExternalApiClient>();

        }
        [DataTestMethod]
        [DataRow(true, 20)]
        [DataRow(false, 0)]
        public async Task GetOffer_Test(bool isSuccessTest, int expectedReturn)
        {

            var callBackAbsoluteUrl = string.Empty;
            var callBackRequest = string.Empty;
            var callBackMediaType = string.Empty;
            var expectedAbsoluteUrl = "CompanyX/ProductOffer";
            var expectedMediaType = "application/xml";
            var response = isSuccessTest ? GetHttpReponse() : GetHttpErrorReponse();
            var inputRequest = GetMappedValue(GetInput());
            var serializer = new System.Xml.Serialization.XmlSerializer(inputRequest.GetType());
            var expectedRequest = Serializing(serializer, inputRequest);
            var offer = new CompanyXOffer(_apiClient.Object);
            _apiClient.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(response)
                     .Callback((string url, string req, string meditype, string token) => { callBackAbsoluteUrl = url; callBackRequest = req; callBackMediaType = meditype; });
            var result = await offer.GetOffer(GetInput());
            _apiClient.Verify(x => x.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.AreEqual(expectedAbsoluteUrl, callBackAbsoluteUrl);
            Assert.AreEqual(expectedMediaType, callBackMediaType);
            Assert.AreEqual(expectedRequest, callBackRequest);
            Assert.AreEqual(expectedReturn, result.Price);

        }
        private RequestModel GetInput()
        {
            Dimension d1 = new Dimension() { Height = 10, Width = 5 };
            Dimension d2 = new Dimension() { Height = 15, Width = 10 };
            var listDimensions = new List<Dimension>();
            listDimensions.Add(d1);
            listDimensions.Add(d2);
            return new RequestModel() { SourceAddress = "AAA", DestinationAddress = "BBB", Dimensions = listDimensions };
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
        private Task<HttpResponseMessage> GetHttpReponse()
        {
            var result = "{\"Price\":20}";
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(result, System.Text.Encoding.UTF8, "application/json")
            };
            return Task.FromResult(response);

        }
        private Task<HttpResponseMessage> GetHttpErrorReponse()
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable)
            {
                Content = new StringContent(string.Empty, System.Text.Encoding.UTF8, "application/json")
            };
            return Task.FromResult(response);

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
                using (XmlWriter writer = XmlWriter.Create(sww,settings))
                {
                    serializer.Serialize(writer, request,ns1);
                    serializeValue = sww.ToString();
                }
            }
            return serializeValue;
        }
    }
}
