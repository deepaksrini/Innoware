using FluentValidation;
using Innowave.FreedomAdmin.Business.Model;

namespace Innowave.FreedomAdmin.Business.Validation
{
    public class AddressValidator<T> : AbstractValidator<T> where T : Address
    {
        public AddressValidator()
        {
            RuleFor(a => a.City).MaximumLength(128);

            RuleFor(a => a.AddressLine1)
                .NotEmpty()
                .WithMessage(command => "{PropertyName} should not blank.");

            RuleFor(a => a.AddressLine2).MaximumLength(128);

            RuleFor(a => a.State).MaximumLength(128);

            When(c => c.PostalCode.HasValue, () =>
               {
                   RuleFor(a => a.PostalCode).GreaterThanOrEqualTo(6);
               });
        }
    }
}
