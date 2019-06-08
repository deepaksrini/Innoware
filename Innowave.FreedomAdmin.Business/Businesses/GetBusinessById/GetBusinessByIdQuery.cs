using System;
using System.Collections.Generic;
using System.Text;
using Innowave.FreedomAdmin.Business.Interfaces;
using Innowave.FreedomAdmin.DataModel.ViewModel;
using MediatR;

namespace Innowave.FreedomAdmin.Business.Businesses.GetBusinessById
{
    public class GetBusinessByIdQuery : IRequest<BusinessViewModel>, IBusinessId
    {
        public Guid BusinessId { get; set; }
    }
}
