using FluentValidation;
using Innowave.FreedomAdmin.Business.Interfaces;
using Innowave.FreedomAdmin.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innowave.FreedomAdmin.Business.Validation
{
    public static class UserNameValidationExtension
    {
        public static void UserNameValidator<T>(this AbstractValidator<T> validator, IUnitOfWork unitOfWork) where T : IUserName
        {
            validator.RuleFor(c => c.UserName)
                     .NotEmpty()
                     .MustAsync((userName, _) => unitOfWork.UserRepository.Exist(userName))
                     .WithMessage(command => "{PropertyName} already exist");
        }
    }
}
