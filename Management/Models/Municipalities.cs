using System;
using System.Collections.Generic;

namespace Vue.Models
{
    public partial class Municipalities
    {
        public Municipalities()
        {
            Companies = new HashSet<Companies>();
        }

        public long Id { get; set; }
        public long? CitiesId { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public short? Status { get; set; }

        public virtual Cities Cities { get; set; }
        public virtual ICollection<Companies> Companies { get; set; }
    }
}
