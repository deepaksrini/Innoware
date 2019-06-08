using Innowave.FreedomAdmin.DataContext.Interfaces;
using System;

namespace Innowave.FreedomAdmin.DataContext
{
    public interface IUnitOfWork : IDisposable
    {
        IBusinessRepository BusinessRepository { get; }

        IUserRepository UserRepository { get; }

        IRoleRepository RoleRepository { get; }

        IMenuRepository MenuRepository { get; }

        IProductRepository ProductRepository { get; }

        IAddressRepository AddressRepository { get; }

        void Commit();
    }
}
