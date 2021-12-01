using HomeApi.Domain.DTO;
using HomeApi.Domain.ExternalOfferHandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController : ControllerBase
    {

        private IExternalOfferHandler _offerHandler;
        public OfferController(IExternalOfferHandler offerHandler)
        {
            _offerHandler = offerHandler;
        }

        [HttpGet]
        [Route("MinOfferAmount")]
        public async Task<ActionResult<int>> GetMinPriceFromDifferentOffers([FromBody]RequestModel input)
        {
           if(input !=null)
           {
                var minPrice = await _offerHandler.GetMinOfferFromExternalSystems(input);
                return Ok(minPrice);
           }
            else
            {
                return BadRequest();
            }
        }
    }
}
