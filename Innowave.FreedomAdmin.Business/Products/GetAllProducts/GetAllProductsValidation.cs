using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using FluentValidation;

namespace Innowave.FreedomAdmin.Business.Products.GetAllProducts
{
    public class GetAllProductsValidation : AbstractValidator<GetAllProductsCommand>
    {
    }
}
