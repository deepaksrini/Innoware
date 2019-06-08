using System;
using System.Collections.Generic;
using System.Text;

namespace Innowave.FreedomAdmin.DataModel.ViewModel
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid? DefaultProductId { get; set; }

        public Guid DefaultRoleId { get; set; }

        public Guid? BusinessId { get; set; }

        public Guid? DefaultTenentId { get; set; }

        public DateTime? LastLogin { get; set; }
    }
}
