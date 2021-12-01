using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Moq;
using System.Collections.Generic;
using HomeApi.Domain.DTO;
using handler=HomeApi.Domain.ExternalOfferHandler;

namespace HomeApi.Domain.UnitTest
{
   

   
    [TestClass]
    public class ExternalOfferHandler_Test
    {
        private Mock<IOffer> _companyAOffer;
        private Mock<IOffer> _companyBOffer;
        private Mock<IOffer> _companyXOffer;

        [TestInitialize]
        public void Initialize()
        {
            _companyAOffer = new Mock<IOffer>();
            _companyBOffer = new Mock<IOffer>();
            _companyXOffer= new Mock<IOffer>();
        }
        [TestMethod]
        public async Task GetMinOfferFromExternalSystems()
        {
            int expectedValue = 10;
            var listOffer = new List<IOffer>();
            listOffer.Add(_companyAOffer.Object);
            listOffer.Add(_companyBOffer.Object);
            listOffer.Add(_companyXOffer.Object);
            var offerResponseComapanyA = new OfferResponse() { Price = 10 };
            var offerResponseComapanyB = new OfferResponse() { Price = 15 };
            var offerResponseComapanyC = new OfferResponse() { Price = 0 };
            _companyAOffer.Setup(x => x.GetOffer(It.IsAny<RequestModel>())).Returns(Task.FromResult(offerResponseComapanyA)) ;
            _companyBOffer.Setup(x => x.GetOffer(It.IsAny<RequestModel>())).Returns(Task.FromResult(offerResponseComapanyB));
            _companyXOffer.Setup(x => x.GetOffer(It.IsAny<RequestModel>())).Returns(Task.FromResult(offerResponseComapanyC));
            var handler = new handler.ExternalOfferHandler(listOffer);
            var result = await handler.GetMinOfferFromExternalSystems(new RequestModel());
            _companyAOffer.Verify(x => x.GetOffer(It.IsAny<RequestModel>()), Times.Once);
            _companyBOffer.Verify(x => x.GetOffer(It.IsAny<RequestModel>()), Times.Once);
            _companyXOffer.Verify(x => x.GetOffer(It.IsAny<RequestModel>()), Times.Once);
            Assert.AreEqual(expectedValue, result);



        }
    }
}
