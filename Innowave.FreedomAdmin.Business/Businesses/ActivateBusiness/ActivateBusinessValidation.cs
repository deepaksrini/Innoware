using FluentValidation;
using Innowave.FreedomAdmin.Business.Validation;
using Innowave.FreedomAdmin.DataContext;

namespace Innowave.FreedomAdmin.Business.Businesses.ActivateBusiness
{
    public class ActivateBusinessValidation : AbstractValidator<ActivateBusinessCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public ActivateBusinessValidation(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            RuleFor(c => c.Reason)
                .NotEmpty()
                .WithMessage(command => "{PropertyName} should not be blank");

            this.BusinessIdValidator(unitOfWork);
        }
    }
}
