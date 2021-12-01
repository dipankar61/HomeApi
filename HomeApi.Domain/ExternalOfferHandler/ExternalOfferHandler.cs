using HomeApi.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace HomeApi.Domain.ExternalOfferHandler
{
    public class ExternalOfferHandler : IExternalOfferHandler
    {
        private readonly IEnumerable<IOffer> _companyOffers;
        public ExternalOfferHandler(IEnumerable<IOffer> companyOffers)
        {
            _companyOffers = companyOffers;

        }
        public async Task<int> GetMinOfferFromExternalSystems(RequestModel input)
        {
            var listOfTasks = new List<Task<OfferResponse>>();

            foreach (var compOffers in _companyOffers)
            {
                listOfTasks.Add(compOffers.GetOffer(input));
                
            }
            var offers = await Task.WhenAll<OfferResponse>(listOfTasks);
            return offers.Where(x=>x.Price !=0).ToList().Min(x => x.Price);
        }
    }
}
