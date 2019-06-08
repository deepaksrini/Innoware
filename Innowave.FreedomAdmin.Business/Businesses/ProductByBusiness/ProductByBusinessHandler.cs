using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Innowave.FreedomAdmin.DataContext;
using Innowave.FreedomAdmin.DataModel.Model;
using MediatR;

namespace Innowave.FreedomAdmin.Business.Businesses.ProductByBusiness
{
    public class ProductByBusinessHandler : IRequestHandler<ProductByBusinessCommand, HttpStatusCode>
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductByBusinessHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<HttpStatusCode> Handle(ProductByBusinessCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.BusinessRepository.RemoveExistingProducts(request.BusinessId);
            foreach (var product in request.Products)
            {
                var productsByBusinesses = new ProductsByBusinesses()
                {
                    ProductId = product,
                    BusinessId = request.BusinessId,
                    AssignedBy = request.UserId,
                    AssignedDate = DateTime.Now
                };
                await unitOfWork.BusinessRepository.InsertProductsByBusinesses(productsByBusinesses);
            }
            unitOfWork.Commit();
            return HttpStatusCode.OK;
        }
    }
}
