using System;
using System.Collections.Generic;

namespace Vue.Models
{
    public partial class Users
    {
        public Users()
        {
            Companies = new HashSet<Companies>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public short? UserType { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string Phone { get; set; }
        public string ExtraPhone { get; set; }
        public DateTime? BirthDate { get; set; }
        public short? Gender { get; set; }
        public string Otp { get; set; }
        public DateTime? Otpdate { get; set; }
        public int? OtptryAtempt { get; set; }
        public DateTime? OtptryAtemptDate { get; set; }
        public DateTime? LoginTryAttemptDate { get; set; }
        public short? LoginTryAttempts { get; set; }
        public DateTime? LastLoginOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public short? Status { get; set; }

        public virtual ICollection<Companies> Companies { get; set; }
    }
}
