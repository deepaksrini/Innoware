using Innowave.FreedomAdmin.DataModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innowave.FreedomAdmin.Business.Model
{
    public class ConnectorResponse
    {
        public UserViewModel User { get; set; }

        public RolesViewModel Role { get; set; }

        public string TokenType { get; set; }

        public string Token { get; set; }

        public DateTime ExpiredAt { get; set; }
    }
}
