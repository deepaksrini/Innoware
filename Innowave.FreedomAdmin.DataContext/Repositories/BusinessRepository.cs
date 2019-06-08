using Innowave.FreedomAdmin.DataContext.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Innowave.FreedomAdmin.DataModel.Model;
using Innowave.FreedomAdmin.DataModel.ViewModel;

namespace Innowave.FreedomAdmin.DataContext.Repositories
{
    public class BusinessRepository : RepositoryBase, IBusinessRepository
    {
        public BusinessRepository(IDbTransaction transaction)
         : base(transaction)
        {
            
        }

        public async Task<bool> Exist(string name)
        {
            var result = await Connection.QueryFirstOrDefaultAsync<Businesses>($"SELECT * FROM Businesses WHERE Name='{name}'",
                transaction: Transaction);
            return result == null ? true : string.IsNullOrEmpty(result.Name) ? true : false;
        }

        public async Task<bool> Exist(Guid id)
        {
            var result = await Connection.QueryFirstOrDefaultAsync<Businesses>($"SELECT * FROM Businesses WHERE Id='{id}'",
                transaction: Transaction);
            return result == null ? false : true;
        }

        public async Task<int> CreateNewBusiness(Businesses businesses)
        {
            string query = $"INSERT INTO Businesses(Id,Name,IsActive,Logo,MobileTelephone,WorkTelephone,Fax,Website,Email,Created,CreatedBy,LastUpdated,LastUpdatedBy)" +
                $"VALUES(@Id,@Name,@IsActive,@Logo,@MobileTelephone,@WorkTelephone,@Fax,@Website,@Email,@Created,@CreatedBy,@LastUpdated,@LastUpdatedBy)";

            var affectedRows = await Connection.ExecuteAsync(query, new
            {
                Id = businesses.Id,
                Name = businesses.Name,
                IsActive = businesses.IsActive,
                Logo = businesses.Logo,
                MobileTelephone = businesses.MobileTelephone,
                WorkTelephone = businesses.WorkTelephone,
                Fax = businesses.Fax,
                Website = businesses.Website,
                Email = businesses.Email,
                Created = businesses.Created,
                CreatedBy = businesses.CreatedBy,
                LastUpdated = businesses.LastUpdated,
                LastUpdatedBy = businesses.CreatedBy
            }, transaction: Transaction);
            return affectedRows;
        }

        public async Task InsertProductsByBusinesses(ProductsByBusinesses productsByBusinesses)
        {
            string query = $"INSERT INTO ProductsByBusinesses(ProductId,BusinessId,AssignedBy,AssignedDate)" +
                $"VALUES(@ProductId,@BusinessId,@AssignedBy,@AssignedDate)";
            await Connection.ExecuteAsync(query, new
            {
                ProductId = productsByBusinesses.ProductId,
                BusinessId = productsByBusinesses.BusinessId,
                AssignedBy = productsByBusinesses.AssignedBy,
                AssignedDate = productsByBusinesses.AssignedDate
            }, transaction: Transaction);
        }

        public async Task<List<Businesses>> GetAllBusiness()
        {
            var result = await Connection.QueryAsync<Businesses>($"SELECT * FROM Businesses", transaction: Transaction);
            return result.AsList();
        }

        public async Task<BusinessViewModel> GetBusinessById(Guid Id)
        {
            var business = await Connection.QueryFirstOrDefaultAsync<Businesses>($"SELECT * FROM Businesses WHERE Id='{Id}'", transaction: Transaction);
            var user = await Connection.QueryFirstOrDefaultAsync<User>($"SELECT * FROM Users WHERE Id='{business.CreatedBy}'", transaction: Transaction);
            var address = await Connection.QueryFirstOrDefaultAsync<Address>($"SELECT * FROM Addresses WHERE Id='{business.Id}'", transaction: Transaction);

            return new BusinessViewModel()
            {
                Businesses = business,
                Address = address,
                User = user
            };
        }

        public async Task Activate(Guid businessId, string reason, Guid userId)
        {
            await ActivateOrDeactivate(businessId, reason, userId, true);
        }

        public async Task DeActivate(Guid businessId, string reason, Guid userId)
        {
            await ActivateOrDeactivate(businessId, reason, userId, false);
        }

        public async Task RemoveExistingProducts(Guid businessId)
        {
            string query = $"DELETE FROM ProductsByBusinesses WHERE BusinessId='{businessId}'";
            await Connection.ExecuteAsync(query, transaction: Transaction);
        }

        private async Task ActivateOrDeactivate(Guid businessId, string reason, Guid userId, bool active)
        {
            string query = $"UPDATE Businesses SET IsActive=@Active,Reason=@Reason,LastUpdatedBy=@UserId,LastUpdated=@LastUpdated WHERE Id='{businessId}'";
            await Connection.ExecuteAsync(query, new
            {
                Active = active,
                Reason = reason,
                UserId = userId,
                LastUpdated = DateTime.Now
            }, transaction: Transaction);
        }
    }
}
