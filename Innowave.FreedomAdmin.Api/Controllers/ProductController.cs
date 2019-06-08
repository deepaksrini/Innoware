using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Innowave.FreedomAdmin.Api.Filters;
using Innowave.FreedomAdmin.Business.Products.GetAllProducts;
using Microsoft.AspNetCore.Authorization;

namespace Innowave.FreedomAdmin.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [AuthorizationModelAttribute]
        [Route("getallproducts")]
        public async Task<IActionResult> GetAllProducts(GetAllProductsCommand query)
        {
            var response = await mediator.Send(query);
            return Ok(response);
        }
    }
}