using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Innowave.FreedomAdmin.Business.Validation;
using Innowave.FreedomAdmin.DataContext;

namespace Innowave.FreedomAdmin.Business.Addresses.UpdateAddress
{
    public class UpdateAddressValidation : AbstractValidator<UpdateAddressCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateAddressValidation(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            RuleFor(c => c.Id)
                .NotNull()
                .MustAsync((id, _) => unitOfWork.AddressRepository.Exists(id))
                .WithMessage(command => "{PropertyName} does not exist.");

            RuleFor(c => c.Address)
              .NotNull()
              .IsValidAddress();
        }
    }
}
