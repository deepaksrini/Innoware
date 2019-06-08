using System;
using System.Net;
using Innowave.FreedomAdmin.Business.Interfaces;
using MediatR;
using Newtonsoft.Json;

namespace Innowave.FreedomAdmin.Business.Businesses.ProductByBusiness
{
    public class ProductByBusinessCommand : IRequest<HttpStatusCode>, IBusinessId, IProducts, IUserId
    {
        public Guid BusinessId { get; set; }

        public Guid[] Products { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
