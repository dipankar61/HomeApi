using System;
using System.Collections.Generic;
using System.Text;

namespace HomeApi.Domain.DTO
{
    
        public class CompanyBOfferRequest
        {
            public string Consignee { get; set; }

            public string Consignor { get; set; }

            public IEnumerable<Dimension> Cartons { get; set; }
        }
        
    
}
