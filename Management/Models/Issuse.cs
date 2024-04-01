using System;
using System.Collections.Generic;

namespace Vue.Models
{
    public partial class Issuse
    {
        public long Id { get; set; }
        public long? CompaniesId { get; set; }
        public string Name { get; set; }
        public string Resone { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public short? ResolvedBy { get; set; }
        public DateTime? ResolvedOn { get; set; }
        public short? Status { get; set; }

        public virtual Companies Companies { get; set; }
    }
}
