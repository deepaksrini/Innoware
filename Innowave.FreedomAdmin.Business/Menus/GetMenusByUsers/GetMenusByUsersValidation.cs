using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Innowave.FreedomAdmin.Business.Validation;
using Innowave.FreedomAdmin.DataContext;

namespace Innowave.FreedomAdmin.Business.Menus.GetMenusByUsers
{
   public class GetMenusByUsersValidation :AbstractValidator<GetMenusByUsersCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetMenusByUsersValidation(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            this.UserIdValidator(unitOfWork);

            this.ProductIdValidator(unitOfWork);
        }


    }
}
