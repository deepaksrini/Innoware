using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using Innowave.FreedomAdmin.DataModel.Model;
using Innowave.FreedomAdmin.DataContext.Interfaces;
using System.Linq;

namespace Innowave.FreedomAdmin.DataContext.Repositories
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        public async Task<bool> Exist(Guid productId)
        {
            var result = await Connection.QueryFirstOrDefaultAsync<Product>($"SELECT * FROM Products WHERE Id='{productId}'",
                transaction: Transaction);
            if (result != null)
                return true;
            else
                return false;
        }

        public async Task<List<Product>> Get()
        {
            var result = await Connection.QueryAsync<Product>("SELECT * FROM Products",
                transaction: Transaction);
            return result.AsList();
        }

        public async Task<bool> Exist(Guid[] productIds)
        {
            string query = $"SELECT * FROM Products WHERE Id IN @ids";
            var result = await Connection.QueryAsync<Product>(query, new { ids = new[] { productIds } }, transaction: Transaction);
            var existingProducts = result.Select(c => c.Id).ToArray();
            return !existingProducts.Except(productIds).Any();
        }
    }
}
