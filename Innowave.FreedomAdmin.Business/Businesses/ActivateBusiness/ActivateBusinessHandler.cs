using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Innowave.FreedomAdmin.DataContext;
using MediatR;

namespace Innowave.FreedomAdmin.Business.Businesses.ActivateBusiness
{
    public class ActivateBusinessHandler : IRequestHandler<ActivateBusinessCommand, HttpStatusCode>
    {
        private readonly IUnitOfWork unitOfWork;

        public ActivateBusinessHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<HttpStatusCode> Handle(ActivateBusinessCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.BusinessRepository.Activate(request.BusinessId, request.Reason, request.UserId);
            unitOfWork.Commit();
            return HttpStatusCode.OK;
        }
    }
}
