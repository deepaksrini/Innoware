using System;
using System.Collections.Generic;
using System.Text;

namespace Innowave.FreedomAdmin.DataModel.Model
{
    public class SubMenus
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid MenuId { get; set; }

        public int OrderNo { get; set; }

        public string Icon { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public bool IsActive { get; set; }
    }
}
