using System;
using System.Collections.Generic;
using System.Text;

namespace HomeApi.Domain.DTO
{
    public class CompanyAOfferRquest
    {
        public string ContactAddress { get; set; }

        public string WareHouseAddress { get; set; }

        public IEnumerable<Dimension> Dimensions { get; set; }


    }
   
   
}
