using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Innowave.FreedomAdmin.DataContext.Interfaces;
using System.Threading.Tasks;
using Innowave.FreedomAdmin.DataModel.ViewModel;
using Dapper;

namespace Innowave.FreedomAdmin.DataContext.Repositories
{
    public class RoleRepository : RepositoryBase, IRoleRepository
    {
        public RoleRepository(IDbTransaction transaction):base(transaction)
        {

        }

        public async Task<RolesViewModel> Get(Guid roleId)
        {
            var result = await Connection.QueryFirstOrDefaultAsync<RolesViewModel>($"SELECT * FROM Roles WHERE Id='{roleId}'",
                transaction: Transaction);
            return result;
        }
    }
}
