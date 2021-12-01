
using HomeApi.Domain.DTO;
using HomeApi.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Domain.UnitTest
{
    [TestClass]
    public class CompanyAOffer_Test
    {
        private Mock<IExternalApiClient> _apiClient;
       
        [TestInitialize]
        public void Initialize()
        {
            _apiClient = new Mock<IExternalApiClient>();
           
        }
        [DataTestMethod]
        [DataRow(true, 10)]
        [DataRow(false, 0)]
        public async Task GetOffer_Test(bool isSuccessTest, int expectedReturn)
        {
            
            var callBackAbsoluteUrl = string.Empty;
            var callBackRequest = string.Empty;
            var callBackMediaType = string.Empty;
            var expectedAbsoluteUrl = "CompanyA/ProductOffer";
            var expectedMediaType = "application/json";
            var expectedRequest = "{\"ContactAddress\":\"AAA\",\"WareHouseAddress\":\"BBB\",\"Dimensions\":[{\"Height\":10,\"Width\":5},{\"Height\":15,\"Width\":10}]}";
            var response = isSuccessTest ? GetHttpReponse() : GetHttpErrorReponse();
            var offer = new CompanyAOffer(_apiClient.Object);
            _apiClient.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(response)
                     .Callback((string url,string req,string meditype,string token) => { callBackAbsoluteUrl = url; callBackRequest = req; callBackMediaType = meditype; });
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
        private Task<HttpResponseMessage> GetHttpReponse()
        {
            var result = "{\"Price\":10}";
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
    }
}
