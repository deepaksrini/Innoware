using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Innowave.FreedomAdmin.DataContext;
using Innowave.FreedomAdmin.DataModel.Model;
using MediatR;

namespace Innowave.FreedomAdmin.Business.Businesses.GetAllBusiness
{
    public class GetAllBusinessHandler : IRequestHandler<GetAllBusinessQuery, List<DataModel.Model.Businesses>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllBusinessHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<DataModel.Model.Businesses>> Handle(GetAllBusinessQuery request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.BusinessRepository.GetAllBusiness();
            return result;
        }
    }
}
