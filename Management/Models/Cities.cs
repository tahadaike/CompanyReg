using System;
using System.Collections.Generic;

namespace Vue.Models
{
    public partial class Cities
    {
        public Cities()
        {
            Municipalities = new HashSet<Municipalities>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public short? Status { get; set; }

        public virtual ICollection<Municipalities> Municipalities { get; set; }
    }
}
