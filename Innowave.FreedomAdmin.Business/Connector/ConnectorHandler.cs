using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Innowave.FreedomAdmin.Business.Helpers;
using Innowave.FreedomAdmin.Business.Model;
using Innowave.FreedomAdmin.Business.Security;
using Innowave.FreedomAdmin.DataContext;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Innowave.FreedomAdmin.Business.Connector
{
    public class ConnectorHandler : IRequestHandler<ConnectorCommand, ConnectorResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICryptographyHelper cryptographyHelper;
        private readonly IOptions<ConfigurationManager> configurationService;

        public ConnectorHandler(IUnitOfWork unitOfWork,
            ICryptographyHelper cryptographyHelper,
            IOptions<ConfigurationManager> configurationService)
        {
            this.unitOfWork = unitOfWork;
            this.cryptographyHelper = cryptographyHelper;
            this.configurationService = configurationService;
        }

        public async Task<ConnectorResponse> Handle(ConnectorCommand request, CancellationToken cancellationToken)
        {
            var password = cryptographyHelper.Encrypt(request.Password);
            var user = await unitOfWork.UserRepository.UserValidation(request.UserName, password);
            if (user == null)
            {
                return null;
            }
            var role = await unitOfWork.RoleRepository.Get(user.DefaultRoleId);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configurationService.Value.JwtSettings.JwtPublicKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.GivenName,$"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Role, role.Description)
                }),
                Issuer = configurationService.Value.JwtSettings.Issuer,
                Expires = DateTime.Now.AddSeconds(Convert.ToDouble(configurationService.Value.JwtSettings.LifeTime)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var response = new ConnectorResponse
            {
                User = user,
                Role = role,
                TokenType = configurationService.Value.JwtSettings.TokenType,
                Token = tokenHandler.WriteToken(token),
                ExpiredAt = tokenDescriptor.Expires.Value
            };

            return response;
        }
    }
}
