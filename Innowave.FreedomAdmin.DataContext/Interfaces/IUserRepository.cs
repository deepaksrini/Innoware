using Innowave.FreedomAdmin.DataModel.Model;
using Innowave.FreedomAdmin.DataModel.ViewModel;
using System;
using System.Threading.Tasks;

namespace Innowave.FreedomAdmin.DataContext.Interfaces
{
    public interface IUserRepository
    {
        Task<UserViewModel> UserValidation(string userName, string password);

        Task<bool> Exist(Guid userId);

        Task<bool> Exist(string userName);

        Task CreateNewUser(User user);
    }
}
