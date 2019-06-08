using System;
using System.Collections.Generic;
using System.Text;
using Innowave.FreedomAdmin.Business.Interfaces;
using Innowave.FreedomAdmin.DataModel.ViewModel;
using MediatR;

namespace Innowave.FreedomAdmin.Business.Menus.GetMenusByUsers
{
    public class GetMenusByUsersCommand : IRequest<MenusByUsers>, IUserId, IProductId
    {
        public Guid UserId { get; set; }

        public Guid? ProductId { get; set; }
    }
}
