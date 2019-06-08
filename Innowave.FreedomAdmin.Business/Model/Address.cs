using System;
using System.Collections.Generic;
using System.Text;

namespace Innowave.FreedomAdmin.Business.Model
{
    public class Address
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public int? PostalCode { get; set; }
    }
}
