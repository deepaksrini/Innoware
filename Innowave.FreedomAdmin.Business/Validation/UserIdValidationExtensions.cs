using FluentValidation;
using Innowave.FreedomAdmin.Business.Interfaces;
using Innowave.FreedomAdmin.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innowave.FreedomAdmin.Business.Validation
{
    public static class UserIdValidationExtensions
    {
        public static void UserIdValidator<T>(this AbstractValidator<T> validator, IUnitOfWork unitOfWork) where T : IUserId
        {
            validator.RuleFor(c => c.UserId)
                     .NotEmpty()
                     .MustAsync((userId, _) => unitOfWork.UserRepository.Exist(userId))
                     .WithMessage(command => "{PropertyName} does not exist");
        }
    }
}
