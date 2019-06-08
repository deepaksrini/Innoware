using System;
using System.Collections.Generic;
using System.Text;

namespace Innowave.FreedomAdmin.DataModel.Helpers
{
    public abstract class BaseEntity
    {
        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
