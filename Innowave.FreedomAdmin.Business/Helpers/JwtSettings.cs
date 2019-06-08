using System;
using System.Collections.Generic;
using System.Text;

namespace Innowave.FreedomAdmin.Business.Helpers
{
    public class JwtSettings
    {
        public string TokenType { get; set; }

        public string LifeTime { get; set; }

        public string JwtPublicKey { get; set; }

        public string Issuer { get; set; }
    }
}
