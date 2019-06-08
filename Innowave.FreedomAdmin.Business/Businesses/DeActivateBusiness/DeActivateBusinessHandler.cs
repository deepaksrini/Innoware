using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Innowave.FreedomAdmin.DataContext;
using MediatR;

namespace Innowave.FreedomAdmin.Business.Businesses.DeActivateBusiness
{
    public class DeActivateBusinessHandler : IRequestHandler<DeActivateBusinessCommand, HttpStatusCode>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeActivateBusinessHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<HttpStatusCode> Handle(DeActivateBusinessCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.BusinessRepository.DeActivate(request.BusinessId, request.Reason, request.UserId);
            unitOfWork.Commit();
            return HttpStatusCode.OK;
        }
    }
}
