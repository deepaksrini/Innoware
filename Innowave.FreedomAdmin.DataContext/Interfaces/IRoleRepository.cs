using Innowave.FreedomAdmin.DataModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Innowave.FreedomAdmin.DataContext.Interfaces
{
    public interface IRoleRepository
    {
        Task<RolesViewModel> Get(Guid roleId);
    }
}
