using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Innowave.FreedomAdmin.Business.Businesses.CreateNewBusiness;
using Innowave.FreedomAdmin.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Innowave.FreedomAdmin.Api.Swagger;
using Innowave.FreedomAdmin.Api.Helpers;
using Innowave.FreedomAdmin.Business.Businesses.GetAllBusiness;
using Innowave.FreedomAdmin.Business.Businesses.GetBusinessById;
using Innowave.FreedomAdmin.Business.Businesses.ActivateBusiness;
using System.Net;
using Innowave.FreedomAdmin.Business.Businesses.DeActivateBusiness;
using Innowave.FreedomAdmin.Business.Businesses.ProductByBusiness;
using Innowave.FreedomAdmin.Business.Addresses.UpdateAddress;

namespace Innowave.FreedomAdmin.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [AuthorizationModelAttribute]
    public class BusinessController : Controller
    {
        private readonly IMediator mediator;

        public BusinessController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("CreateNewBusiness")]
        [ProducesValidationError]
        public async Task<IActionResult> CreateNewBusiness([FromBody] CreateNewBusinessCommand command)
        {
            command.UserId = ReadJwtToken.GetUser(HttpContext);
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllBusiness(GetAllBusinessQuery query)
        {
            var response = await mediator.Send(query);
            return Ok(response);

        }

        [HttpGet]
        [Route("{businessId}/Get")]
        public async Task<IActionResult> GetBusiness(GetBusinessByIdQuery query)
        {
            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpPut]
        [Route("Activate")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesValidationError]
        public async Task Activate([FromBody] ActivateBusinessCommand command)
        {
            command.UserId = ReadJwtToken.GetUser(HttpContext);
            await mediator.Send(command);
        }

        [HttpPut]
        [Route("DeActivate")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesValidationError]
        public async Task DeActivate([FromBody] DeActivateBusinessCommand command)
        {
            command.UserId = ReadJwtToken.GetUser(HttpContext);
            await mediator.Send(command);
        }

        [HttpPut]
        [Route("UpdateProducts")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesValidationError]
        public async Task UpdateProducts([FromBody] ProductByBusinessCommand command)
        {
            command.UserId = ReadJwtToken.GetUser(HttpContext);
            await mediator.Send(command);
        }

        [HttpPut]
        [Route("UpdateAddress")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesValidationError]
        public async Task UpdateAddress([FromBody] UpdateAddressCommand command)
        {
            await mediator.Send(command);
        }

    }
}