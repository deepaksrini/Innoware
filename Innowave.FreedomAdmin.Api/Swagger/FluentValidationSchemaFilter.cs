using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Validators;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Innowave.FreedomAdmin.Api.Swagger
{
    public class FluentValidationSchemaFilter : ISchemaFilter
    {
        private readonly IValidatorFactory factory;

        public FluentValidationSchemaFilter(IValidatorFactory factory)
        {
            this.factory = factory;
        }

        public void Apply(Schema model, SchemaFilterContext context)
        {
            var validator = factory.GetValidator(context.SystemType);

            if (validator == null) return;

            if (model.Required == null) model.Required = new List<string>();

            var validatorDescriptor = validator.CreateDescriptor();

            foreach (var key in model.Properties.Keys)
            {
                foreach (var propertyValidator in validatorDescriptor.GetValidatorsForMember(key.ToPascalCase()))
                {
                    if (propertyValidator is NotNullValidator || propertyValidator is NotEmptyValidator)
                    {
                        model.Required.Add(key);
                    }

                    var lengthValidator = propertyValidator as LengthValidator;
                    if (lengthValidator != null)
                    {
                        if (lengthValidator.Max > 0)
                        {
                            model.Properties[key].MaxLength = lengthValidator.Max;
                        }

                        model.Properties[key].MinLength = lengthValidator.Min;
                    }

                    var expressionValidator = propertyValidator as RegularExpressionValidator;
                    if (expressionValidator != null)
                    {
                        model.Properties[key].Pattern = expressionValidator.Expression;
                    }
                }
            }
        }
    }
}
