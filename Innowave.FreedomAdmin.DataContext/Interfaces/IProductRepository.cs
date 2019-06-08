using Innowave.FreedomAdmin.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Innowave.FreedomAdmin.DataContext.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> Exist(Guid productId);

        Task<List<Product>> Get();

        Task<bool> Exist(Guid[] productIds);
    }
}
