using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using MediatR;

namespace Innowave.FreedomAdmin.Business.Businesses.CreateNewBusiness
{
    public class CreateNewBusinessCommand : Model.Business, IRequest<HttpStatusCode>
    {

    }
}
