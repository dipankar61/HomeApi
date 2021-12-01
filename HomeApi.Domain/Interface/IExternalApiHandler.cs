using HomeApi.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Domain.ExternalOfferHandler
{
    public interface IExternalOfferHandler
    {
        Task<int> GetMinOfferFromExternalSystems(RequestModel input);
       

    }
}
