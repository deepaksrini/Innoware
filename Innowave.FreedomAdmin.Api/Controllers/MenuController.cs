using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Innowave.FreedomAdmin.Business.Menus.GetMenusByUsers;
using Innowave.FreedomAdmin.Api.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Innowave.FreedomAdmin.Api.Controllers
{
    [Route("api/Menu")]
    [Authorize]
    public class MenuController : Controller
    {
        private readonly IMediator mediator;

        public MenuController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("getmenus")]
        public async Task<IActionResult> GetMenus(Guid? productId)
        {
            var command = new GetMenusByUsersCommand()
            {
                UserId = ReadJwtToken.GetUser(HttpContext),
                ProductId = productId
            };
            var response = await mediator.Send(command);
            return Ok(response);
        }
    }
}