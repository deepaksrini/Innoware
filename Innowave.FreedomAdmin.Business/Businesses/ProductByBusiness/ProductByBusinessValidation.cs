using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Innowave.FreedomAdmin.Business.Validation;
using Innowave.FreedomAdmin.DataContext;

namespace Innowave.FreedomAdmin.Business.Businesses.ProductByBusiness
{
    public class ProductByBusinessValidation : AbstractValidator<ProductByBusinessCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductByBusinessValidation(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            this.BusinessIdValidator(unitOfWork);

            this.ProductsValidator(unitOfWork);
        }

    }
}
