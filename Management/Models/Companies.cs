using System;
using System.Collections.Generic;

namespace Vue.Models
{
    public partial class Companies
    {
        public Companies()
        {
            CampanyAttachments = new HashSet<CampanyAttachments>();
            Issuse = new HashSet<Issuse>();
            Lincense = new HashSet<Lincense>();
        }

        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? MunicipalitiesId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime? EstablishmentDate { get; set; }
        public string LicenseNumber { get; set; }
        public string CommercialRegistrationNumber { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public short? Levels { get; set; }
        public short? Status { get; set; }

        public virtual Municipalities Municipalities { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<CampanyAttachments> CampanyAttachments { get; set; }
        public virtual ICollection<Issuse> Issuse { get; set; }
        public virtual ICollection<Lincense> Lincense { get; set; }
    }
}
