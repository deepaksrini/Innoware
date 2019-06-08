using System;
using System.Collections.Generic;
using System.Text;
using Innowave.FreedomAdmin.Business.Model;
using MediatR;

namespace Innowave.FreedomAdmin.Business.Connector
{
    public class ConnectorCommand : IRequest<ConnectorResponse>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
