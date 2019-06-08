using FluentValidation;
using Innowave.FreedomAdmin.Business.Interfaces;
using Innowave.FreedomAdmin.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innowave.FreedomAdmin.Business.Validation
{
    public static class BusinessIdValidationExtension
    {
        public static void BusinessIdValidator<T>(this AbstractValidator<T> validator, IUnitOfWork unitOfWork) where T : IBusinessId
        {
            validator.RuleFor(c => c.BusinessId)
                     .NotEmpty()
                     .MustAsync((businessId, _) => unitOfWork.BusinessRepository.Exist(businessId))
                     .WithMessage(command => "{PropertyName} does not exist");
        }
    }
}
