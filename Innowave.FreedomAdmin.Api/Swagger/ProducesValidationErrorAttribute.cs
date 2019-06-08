using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Innowave.FreedomAdmin.Api.Swagger
{
    public class ProducesValidationErrorAttribute : ProducesResponseTypeAttribute
    {
        public ProducesValidationErrorAttribute() : base(typeof(BadRequestSampleModel), (int)HttpStatusCode.BadRequest)
        {
        }
    }
}
