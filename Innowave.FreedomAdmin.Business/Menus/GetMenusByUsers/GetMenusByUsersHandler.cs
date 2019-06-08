using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Innowave.FreedomAdmin.DataContext;
using Innowave.FreedomAdmin.DataModel.ViewModel;
using MediatR;

namespace Innowave.FreedomAdmin.Business.Menus.GetMenusByUsers
{
    public class GetMenusByUsersHandler : IRequestHandler<GetMenusByUsersCommand, MenusByUsers>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetMenusByUsersHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<MenusByUsers> Handle(GetMenusByUsersCommand request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.MenuRepository.GetMenusByUsers(request.UserId, request.ProductId);
            return result;
        }
    }
}
