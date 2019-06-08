using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Innowave.FreedomAdmin.DataContext;
using Innowave.FreedomAdmin.DataModel.ViewModel;
using MediatR;

namespace Innowave.FreedomAdmin.Business.Businesses.GetBusinessById
{
    public class GetBusinessByIdHandler : IRequestHandler<GetBusinessByIdQuery, BusinessViewModel>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetBusinessByIdHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<BusinessViewModel> Handle(GetBusinessByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.BusinessRepository.GetBusinessById(request.BusinessId);
            return result;
        }
    }
}
