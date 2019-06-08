using Innowave.FreedomAdmin.DataModel.Helpers;
using System;

namespace Innowave.FreedomAdmin.DataModel.Model
{
    public class Businesses : BaseEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string Logo { get; set; }

        public string MobileTelephone { get; set; }

        public string WorkTelephone { get; set; }

        public string Fax { get; set; }

        public string Website { get; set; }

        public string Email { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid LastUpdatedBy { get; set; }
    }
}
