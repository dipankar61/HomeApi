using System;
using System.Collections.Generic;
using System.Text;

namespace HomeApi.Domain.DTO
{
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/OfferApi", IsNullable = true)]
    public class CompanyXOfferRequest
    {

       
        public string Destination { get; set; }

        public List<Package> Packages { get; set; }
        public string Source { get; set; }
    }
    public class Package
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }
    
}
