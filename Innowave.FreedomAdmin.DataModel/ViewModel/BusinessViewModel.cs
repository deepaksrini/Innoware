using Innowave.FreedomAdmin.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innowave.FreedomAdmin.DataModel.ViewModel
{
    public class BusinessViewModel
    {
        public BusinessViewModel()
        {
            Businesses = new Businesses();
            User = new User();
            Address = new Address();
        }

        public Businesses Businesses { get; set; }

        public User User { get; set; }

        public Address Address { get; set; }
    }
}
