using System;

namespace Innowave.FreedomAdmin.DataModel.Model
{
    public class ProductsByBusinesses
    {
        public Guid ProductId { get; set; }

        public Guid BusinessId { get; set; }

        public Guid AssignedBy { get; set; }

        public DateTime AssignedDate { get; set; } = DateTime.Now;
    }
}
