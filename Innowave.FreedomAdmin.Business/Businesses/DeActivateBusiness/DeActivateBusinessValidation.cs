using FluentValidation;
using Innowave.FreedomAdmin.Business.Validation;
using Innowave.FreedomAdmin.DataContext;

namespace Innowave.FreedomAdmin.Business.Businesses.DeActivateBusiness
{
    public  class DeActivateBusinessValidation : AbstractValidator<DeActivateBusinessCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeActivateBusinessValidation(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            RuleFor(c => c.Reason)
               .NotEmpty()
               .WithMessage(command => "{PropertyName} should not be blank");

            this.BusinessIdValidator(unitOfWork);
        }

    }
}
