using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Innowave.FreedomAdmin.Business.Connector
{
    public class ConnectorValidation : AbstractValidator<ConnectorCommand>
    {
        public ConnectorValidation()
        {
            RuleFor(c => c.UserName)
                .NotEmpty()
                .WithMessage(command => "{PropertyName} should not be blank.");

            RuleFor(c => c.Password)
                .NotEmpty()
                .WithMessage(command => "{PropertyName} should not be blank.");
        }
    }
}
