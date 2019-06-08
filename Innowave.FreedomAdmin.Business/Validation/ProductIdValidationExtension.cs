using FluentValidation;
using Innowave.FreedomAdmin.Business.Interfaces;
using Innowave.FreedomAdmin.DataContext;

namespace Innowave.FreedomAdmin.Business.Validation
{
    public static class ProductIdValidationExtension
    {
        public static void ProductIdValidator<T>(this AbstractValidator<T> validator, IUnitOfWork unitOfWork) where T : IProductId
        {
            validator.When(c => c.ProductId.HasValue, () =>
              {
                  validator.RuleFor(c => c.ProductId)
                           .NotEmpty()
                           .MustAsync((productId, _) => unitOfWork.ProductRepository.Exist(productId.Value))
                           .WithMessage(command => "{PropertyName} does not exist");
              });
        }
    }
}
