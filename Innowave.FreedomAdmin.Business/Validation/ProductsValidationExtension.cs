using FluentValidation;
using Innowave.FreedomAdmin.Business.Interfaces;
using Innowave.FreedomAdmin.DataContext;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innowave.FreedomAdmin.Business.Validation
{
    public static class ProductsValidationExtension
    {
        public static void ProductsValidator<T>(this AbstractValidator<T> validator, IUnitOfWork unitOfWork) where T : IProducts
        {
            validator.RuleFor(c => c.Products)
                .NotNull()
                .MustAsync((command, products, _) => unitOfWork.ProductRepository.Exist(products))
                .WithMessage("One or more products either do not exist")
                .Must(products => products.Length == 0)
                .WithMessage("You have to select at least one product.")
                .Must(products => products.GroupBy(x => x).All(x => x.Count() == 1))
                .WithMessage("Provided Products contains duplicate values");
        }
    }
}
