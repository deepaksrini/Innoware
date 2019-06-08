using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Innowave.FreedomAdmin.Business.Interfaces;
using MediatR;
using Newtonsoft.Json;

namespace Innowave.FreedomAdmin.Business.Businesses.DeActivateBusiness
{
    public class DeActivateBusinessCommand : IRequest<HttpStatusCode>, IBusinessId
    {
        public Guid BusinessId { get; set; }

        public string Reason { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
