using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using Innowave.FreedomAdmin.DataModel.Model;
using Innowave.FreedomAdmin.DataContext.Interfaces;

namespace Innowave.FreedomAdmin.DataContext.Repositories
{
    public class AddressRepository : RepositoryBase, IAddressRepository
    {
        public AddressRepository(IDbTransaction transaction) : base(transaction)
        {

        }
        public async Task<bool> Exists(Guid id)
        {
            string query = $"SELECT * FROM Addresses WHERE Id='{id}'";
            var result = await Connection.QueryFirstOrDefaultAsync<Address>(query, transaction: Transaction);
            return result == null ? false : true;
        }

        public async Task Insert(Address addresses)
        {
            string query = $"INSERT INTO Addresses(Id,AddressLine1,AddressLine2,City,State,Country,PostalCode)" +
                $"VALUES(@Id,@AddressLine1,@AddressLine2,@City,@State,@Country,@PostalCode)";

            await Connection.ExecuteAsync(query, new
            {
                Id = addresses.Id,
                AddressLine1 = addresses.AddressLine1,
                AddressLine2 = addresses.AddressLine2,
                City = addresses.City,
                State = addresses.State,
                Country = addresses.Country,
                PostalCode = addresses.PostalCode
            }, transaction: Transaction);
        }

        public async Task Update(Address address)
        {
            string query = $"UPDATE Addresses SET AddressLine1=@AddressLine1,AddressLine2=@AddressLine2,City=@City,State=@State,Country=@Country,PostalCode=@PostalCode WHERE Id='{address.Id}'";
            await Connection.ExecuteAsync(query, new
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                Country = address.Country,
                PostalCode = address.PostalCode

            }, transaction: Transaction);
        }
    }
}
