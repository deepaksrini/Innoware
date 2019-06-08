using Innowave.FreedomAdmin.DataModel.Model;
using Innowave.FreedomAdmin.DataModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Innowave.FreedomAdmin.DataContext.Interfaces
{
    public interface IBusinessRepository
    {
        Task<bool> Exist(string name);

        Task<bool> Exist(Guid id);

        Task<int> CreateNewBusiness(Businesses businesses);

        Task InsertProductsByBusinesses(ProductsByBusinesses productsByBusinesses);

        Task<List<Businesses>> GetAllBusiness();

        Task<BusinessViewModel> GetBusinessById(Guid Id);

        Task Activate(Guid businessId, string reason, Guid userId);

        Task DeActivate(Guid businessId, string reason, Guid userId);

        Task RemoveExistingProducts(Guid businessId);
    }
}
