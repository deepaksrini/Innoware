using Innowave.FreedomAdmin.Business.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Innowave.FreedomAdmin.Business.Model
{
    public class Business : IBusinessName, IUserName
    {
        public Business()
        {
            Address = new Address();
        }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string Logo { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string MobileTelephone { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string WorkTelephone { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }

        public string Website { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }

        public List<Guid> Products { get; set; }

        public Address Address { get; set; }
    }
}
