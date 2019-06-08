using System;
using System.Net;
using Innowave.FreedomAdmin.Business.Interfaces;
using MediatR;
using Newtonsoft.Json;

namespace Innowave.FreedomAdmin.Business.Businesses.ActivateBusiness
{
    public class ActivateBusinessCommand : IRequest<HttpStatusCode>, IBusinessId
    {
        public Guid BusinessId { get; set; }

        public string Reason { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
