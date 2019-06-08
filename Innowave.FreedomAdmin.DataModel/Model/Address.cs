using System;

namespace Innowave.FreedomAdmin.DataModel.Model
{
    public class Address
    {
        public Guid Id { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public int? PostalCode { get; set; }
    }
}
