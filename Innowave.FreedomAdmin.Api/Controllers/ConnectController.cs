using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Innowave.FreedomAdmin.Business.Connector;

namespace Innowave.FreedomAdmin.Api.Controllers
{
    [Route("api/[controller]")]
    public class ConnectController : Controller
    {
        private readonly IMediator mediator;

        public ConnectController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Connect([FromBody] ConnectorCommand command)
        {
            var response = await mediator.Send(command);

            if (response == null)
            {
                return Unauthorized();
            }
            return Ok(response);
        }
    }
}