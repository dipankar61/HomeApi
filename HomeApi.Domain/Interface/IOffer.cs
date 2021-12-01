using HomeApi.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Domain
{
    public interface IOffer
    {
          Task<OfferResponse> GetOffer(RequestModel request);
    }
}
