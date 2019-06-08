using FluentValidation;
using Innowave.FreedomAdmin.Business.Model;

namespace Innowave.FreedomAdmin.Business.Validation
{
    public static class AddessValidationExtension
    {
        public static IRuleBuilderOptions<T, Address> IsValidAddress<T>(this IRuleBuilder<T, Address> ruleBuilder)
            => ruleBuilder.SetValidator(new AddressValidator<Address>());
    }
}
