using System;
using System.Collections.Generic;
using System.Text;

namespace HomeApi.Domain.DTO
{
    public class RequestModel
    {
        public string SourceAddress { get; set; }

        public string DestinationAddress { get; set; }

        public IEnumerable<Dimension> Dimensions { get; set; }
    }
}
