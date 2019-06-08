using System;
using MySql.Data.MySqlClient;
using System.Data;
using Innowave.FreedomAdmin.DataContext.Interfaces;
using Innowave.FreedomAdmin.DataContext.Repositories;

namespace Innowave.FreedomAdmin.DataContext
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection connection;
        private IDbTransaction transaction;
        private bool disposed;

        private IBusinessRepository businessRepository;
        private IUserRepository userRepository;
        private IRoleRepository roleRepository;
        private IMenuRepository menuRepository;
        private IProductRepository productRepository;
        private IAddressRepository addressRepository;

        public IBusinessRepository BusinessRepository
        {
            get { return businessRepository ?? (businessRepository = new BusinessRepository(transaction)); }
        }

        public IUserRepository UserRepository
        {
            get { return userRepository ?? (userRepository = new UserRepository(transaction)); }
        }

        public IRoleRepository RoleRepository
        {
            get { return roleRepository ?? (roleRepository = new RoleRepository(transaction)); }
        }

        public IMenuRepository MenuRepository
        {
            get { return menuRepository ?? (menuRepository = new MenuRepository(transaction)); }
        }

        public IProductRepository ProductRepository
        {
            get { return productRepository ?? (productRepository = new ProductRepository(transaction)); }
        }

        public IAddressRepository AddressRepository
        {
            get { return addressRepository ?? (addressRepository = new AddressRepository(transaction)); }
        }

        public UnitOfWork(string connectionString)
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
            transaction = connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                transaction.Commit();
            }
            catch (MySqlException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Dispose();
                transaction = connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            businessRepository = null;
            userRepository = null;
            roleRepository = null;
            menuRepository = null;
            productRepository = null;
            addressRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (transaction != null)
                    {
                        transaction.Dispose();
                        transaction = null;
                    }
                    if (connection != null)
                    {
                        connection.Dispose();
                        connection = null;
                    }
                }
                disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
