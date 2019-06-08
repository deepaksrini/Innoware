using FluentValidation;
using Innowave.FreedomAdmin.Business.Validation;
using Innowave.FreedomAdmin.DataContext;

namespace Innowave.FreedomAdmin.Business.Businesses.CreateNewBusiness
{
    public class CreateNewBusinessValidation : AbstractValidator<CreateNewBusinessCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateNewBusinessValidation(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage(command => "{PropertyName} should not be blank.");

            this.BusinessNameValidator(unitOfWork);

            this.UserNameValidator(unitOfWork);

            When(c => !(string.IsNullOrEmpty(c.Email)), () =>
               {
                   RuleFor(c => c.Email)
                   .EmailAddress()
                   .WithMessage(command => "{PropertyName} is not valid.");

               });

            RuleFor(c => c.Address)
              .NotNull()
              .IsValidAddress();
        }

    }
}
