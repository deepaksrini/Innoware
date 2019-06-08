using FluentValidation;
using Innowave.FreedomAdmin.Business.Validation;
using Innowave.FreedomAdmin.DataContext;

namespace Innowave.FreedomAdmin.Business.Businesses.GetBusinessById
{
    public class GetBusinessByIdValidation : AbstractValidator<GetBusinessByIdQuery>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetBusinessByIdValidation(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            RuleFor(c => c.BusinessId)
                .NotEmpty()
                .WithMessage(command => "{PropertyName} should not be empty");

            this.BusinessIdValidator(unitOfWork);
        }
    }
}
