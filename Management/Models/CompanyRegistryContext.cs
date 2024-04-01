using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Vue.Models
{
    public partial class CompanyRegistryContext : DbContext
    {
        public CompanyRegistryContext()
        {
        }

        public CompanyRegistryContext(DbContextOptions<CompanyRegistryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CampanyAttachments> CampanyAttachments { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Issuse> Issuse { get; set; }
        public virtual DbSet<Lincense> Lincense { get; set; }
        public virtual DbSet<Municipalities> Municipalities { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=DESKTOP-PU8ELBN;database=CompanyRegistry;uid=taha;pwd=0260;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CampanyAttachments>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(550);

                entity.Property(e => e.Path).HasMaxLength(550);

                entity.Property(e => e.Status)
                    .HasDefaultValueSql("((0))")
                    .HasComment(@"1-active
2-request
3-stopped
9-delete
");

                entity.HasOne(d => d.Companies)
                    .WithMany(p => p.CampanyAttachments)
                    .HasForeignKey(d => d.CompaniesId)
                    .HasConstraintName("FK_CampanyAttachments_Companies");
            });

            modelBuilder.Entity<Cities>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Status).HasComment(@"1-active
2-not active
9-delete");
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.Property(e => e.CommercialRegistrationNumber).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.EstablishmentDate).HasColumnType("datetime");

                entity.Property(e => e.Levels).HasComment(@"1-Not Compleate 
2-Confirm Phone
3-Requested
4-Accepted
5-Rejected
");

                entity.Property(e => e.LicenseNumber).HasMaxLength(50);

                entity.Property(e => e.OwnerName).HasMaxLength(250);

                entity.Property(e => e.OwnerPhone).HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.ReceiptNumber).HasMaxLength(50);

                entity.Property(e => e.Status).HasComment(@"1-active
2-not active
3-stopped
4-admin
9-delete
");

                entity.HasOne(d => d.Municipalities)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.MunicipalitiesId)
                    .HasConstraintName("FK_Companies_Municipalities");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Companies_Users");
            });

            modelBuilder.Entity<Issuse>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ResolvedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Companies)
                    .WithMany(p => p.Issuse)
                    .HasForeignKey(d => d.CompaniesId)
                    .HasConstraintName("FK_Problems_Companies");
            });

            modelBuilder.Entity<Lincense>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ExpiryOn).HasColumnType("datetime");

                entity.Property(e => e.Status).HasComment(@"1-active
2-not active
3-stopped
4-admin
9-delete
");

                entity.HasOne(d => d.Companies)
                    .WithMany(p => p.Lincense)
                    .HasForeignKey(d => d.CompaniesId)
                    .HasConstraintName("FK_Lincense_Companies");
            });

            modelBuilder.Entity<Municipalities>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Status).HasComment(@"1-active
2-not active
9-delete");

                entity.HasOne(d => d.Cities)
                    .WithMany(p => p.Municipalities)
                    .HasForeignKey(d => d.CitiesId)
                    .HasConstraintName("FK_Municipalities_Cities");
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.Property(e => e.Controller).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Operations)
                    .HasMaxLength(50)
                    .HasComment(@"1-Add
2-Edit
3-Delete
4-ChangeStatus
5-Other");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.ExtraPhone).HasMaxLength(50);

                entity.Property(e => e.LastLoginOn).HasColumnType("datetime");

                entity.Property(e => e.LoginName).HasMaxLength(50);

                entity.Property(e => e.LoginTryAttemptDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Otp)
                    .HasColumnName("OTP")
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Otpdate)
                    .HasColumnName("OTPDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.OtptryAtempt).HasColumnName("OTPTryAtempt");

                entity.Property(e => e.OtptryAtemptDate)
                    .HasColumnName("OTPTryAtemptDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(25);

                entity.Property(e => e.Status).HasComment(@"1-active
2-not active
3-stopped
4-admin
9-delete
");

                entity.Property(e => e.UserType)
                    .HasDefaultValueSql("((2))")
                    .HasComment(@"1-admin
2-Creator
3-service lead 
4-Clints

20-traning center 
40-instarcture
60-student

");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
