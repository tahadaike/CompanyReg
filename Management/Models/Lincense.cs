using System;
using System.Collections.Generic;

namespace Vue.Models
{
    public partial class Lincense
    {
        public long Id { get; set; }
        public long? CompaniesId { get; set; }
        public DateTime? ExpiryOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public short? Status { get; set; }

        public virtual Companies Companies { get; set; }
    }
}
