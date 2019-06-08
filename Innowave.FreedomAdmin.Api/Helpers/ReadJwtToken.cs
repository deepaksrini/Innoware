using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Innowave.FreedomAdmin.Api.Helpers
{
    public class ReadJwtToken
    {
        public static Guid GetUser(HttpContext context)
        {
            string token = GetBearerToken(context);
            var jwtToken = new JwtSecurityToken(token);
            return Guid.Parse(jwtToken.Subject);
        }

        private static string GetBearerToken(HttpContext context)
        {
            string accessToken = context.Request.Headers["Authorization"];
            if (accessToken.Contains("Bearer"))
                return accessToken.Replace("Bearer ", "");
            else
                throw new Exception("Authorization header not found!");
        }
    }
}
