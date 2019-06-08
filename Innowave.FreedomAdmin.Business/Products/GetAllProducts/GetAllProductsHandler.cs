using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Innowave.FreedomAdmin.DataContext;
using Innowave.FreedomAdmin.DataModel.Model;
using MediatR;

namespace Innowave.FreedomAdmin.Business.Products.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsCommand, List<Product>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllProductsHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<Product>> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.ProductRepository.Get();
            return result;
        }
    }
}
