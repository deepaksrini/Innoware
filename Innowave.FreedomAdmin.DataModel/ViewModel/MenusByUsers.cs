using Innowave.FreedomAdmin.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innowave.FreedomAdmin.DataModel.ViewModel
{
    public class MenusByUsers
    {
        public MenusByUsers()
        {
            Menus = new List<Menus>();
        }

        public List<Menus> Menus { get; set; }
    }
}
