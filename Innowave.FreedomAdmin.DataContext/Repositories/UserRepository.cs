using Innowave.FreedomAdmin.DataContext.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using Innowave.FreedomAdmin.DataModel.ViewModel;
using Innowave.FreedomAdmin.DataModel.Model;

namespace Innowave.FreedomAdmin.DataContext.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        public async Task<UserViewModel> UserValidation(string userName, string password)
        {
            var result = await Connection.QueryFirstOrDefaultAsync<UserViewModel>($"SELECT * FROM Users WHERE UserName='{userName}' AND Password='{password}'",
                transaction: Transaction);
            return result;
        }

        public async Task<bool> Exist(string userName)
        {
            var result = await Connection.QueryFirstOrDefaultAsync<User>($"SELECT * FROM Users WHERE UserName='{userName}'",
                transaction: Transaction);
            if (result == null)
                return true;
            else
                return false;
        }

        public async Task<bool> Exist(Guid userId)
        {
            var result = await Connection.QueryFirstOrDefaultAsync<User>($"SELECT * FROM Users WHERE Id='{userId}'",
                transaction: Transaction);
            if (result != null)
                return true;
            else
                return false;
        }

        public async Task CreateNewUser(User user)
        {
            string query = $"INSERT INTO Users (Id,FirstName,LastName,UserName,Password,DefaultProductId,DefaultRoleId,BusinessId,DefaultTenentId,IsActive,Created,CreatedBy,LastUpdated,LastUpdatedBy)" +
                $"VALUES(@Id,@FirstName,@LastName,@UserName,@Password,@DefaultProductId,@DefaultRoleId,@BusinessId,@DefaultTenentId,@IsActive,@Created,@CreatedBy,@LastUpdated,@LastUpdatedBy)";

            await Connection.ExecuteAsync(query, new
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = user.Password,
                DefaultProductId = user.DefaultProductId,
                DefaultRoleId = user.DefaultRoleId,
                BusinessId = user.BusinessId,
                DefaultTenentId = user.DefaultTenentId,
                IsActive = user.IsActive,
                Created = user.Created,
                CreatedBy = user.CreatedBy,
                LastUpdated = user.LastUpdated,
                LastUpdatedBy = user.LastUpdatedBy
            });
        }


    }
}
