using Innowave.FreedomAdmin.DataModel.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innowave.FreedomAdmin.DataModel.Model
{
    public class User : BaseEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public Guid DefaultProductId { get; set; }

        public Guid DefaultRoleId { get; set; }

        public Guid BusinessId { get; set; }

        public Guid? DefaultTenentId { get; set; }

        public bool IsActive { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid LastUpdatedBy { get; set; }
    }
}
