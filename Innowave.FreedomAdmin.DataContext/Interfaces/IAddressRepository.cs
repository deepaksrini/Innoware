using Innowave.FreedomAdmin.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Innowave.FreedomAdmin.DataContext.Interfaces
{
    public interface IAddressRepository
    {
        Task<bool> Exists(Guid id);

        Task Insert(Address addresses);

        Task Update(Address address);
    }
}
