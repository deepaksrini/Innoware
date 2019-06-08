using System;
using System.Collections.Generic;
using System.Text;
using Innowave.FreedomAdmin.DataModel.Model;
using MediatR;

namespace Innowave.FreedomAdmin.Business.Products.GetAllProducts
{
    public class GetAllProductsCommand : IRequest<List<Product>>
    {
    }
}
