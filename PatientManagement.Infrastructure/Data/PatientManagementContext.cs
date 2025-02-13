using Microsoft.EntityFrameworkCore;
using PatientManagement.Application.Models;

namespace PatientManagement.Infrastructure.Data
{
    public class PatientManagementContext : DbContext
    {
        public PatientManagementContext(DbContextOptions<PatientManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientRecord> PatientRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Patient entity
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(p => p.PatientId); // Primary Key

                entity.Property(p => p.FirstName)
                    .IsRequired()
                    .HasMaxLength(100); // Add a max length constraint

                entity.Property(p => p.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Gender)
                    .HasMaxLength(10); // Optional: Gender is usually a short string

                entity.Property(p => p.Email)
                    .HasMaxLength(255); // Optional: Set a max length for email

                entity.Property(p => p.Phone)
                    .HasMaxLength(15); // Optional: Phone numbers rarely exceed this length

                entity.Property(p => p.IsDeleted)
                    .HasDefaultValue(false); // Set a default value for IsDeleted
            });

            // Configure the PatientRecord entity
            modelBuilder.Entity<PatientRecord>(entity =>
            {
                entity.HasKey(pr => pr.PatientRecordId); // Primary Key

                // Foreign Key Relationship
                entity.HasOne<Patient>()
                      .WithMany(p => p.PatientRecords)
                      .HasForeignKey(pr => pr.PatientId)
                      .OnDelete(DeleteBehavior.Cascade); // Cascade delete on patient deletion

                entity.Property(pr => pr.Description)
                      .HasMaxLength(500); // Optional: Set max length for record descriptions
            });
        }
    }
}
