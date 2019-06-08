using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Innowave.FreedomAdmin.Business.Model;
using MediatR;

namespace Innowave.FreedomAdmin.Business.Addresses.UpdateAddress
{
    public class UpdateAddressCommand : IRequest<HttpStatusCode>
    {
        public UpdateAddressCommand()
        {
            Address = new Address();
        }
        public Guid Id { get; set; }

        public Address Address { get; set; }
    }
}
