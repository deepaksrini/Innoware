using FluentValidation;
using Innowave.FreedomAdmin.Business.Interfaces;
using Innowave.FreedomAdmin.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innowave.FreedomAdmin.Business.Validation
{
    public static class BusinessNameValidationExtension
    {
        public static void BusinessNameValidator<T>(this AbstractValidator<T> validator, IUnitOfWork unitOfWork) where T : IBusinessName
        {
            validator.RuleFor(c => c.Name)
                     .NotEmpty()
                     .MustAsync(async (name, _) => await unitOfWork.BusinessRepository.Exist(name))
                     .WithMessage(command => "{PropertyName} already exist");
        }
    }
}
