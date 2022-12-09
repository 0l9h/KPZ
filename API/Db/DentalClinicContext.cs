using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace API.Db
{
    public partial class DentalClinicContext : DbContext
    {
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=DentalClinic;Trusted_Connection=True;";

        public DentalClinicContext()
        {
        }

        public DentalClinicContext(DbContextOptions<DentalClinicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Disease> Diseases { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasOne(a => a.Disease)
                .WithOne(a => a.Patient)
                .HasForeignKey<Disease>(c => c.PatientId);

            modelBuilder.Entity<Disease>()
                .HasData(new Disease
                {
                    Id = 1,
                    Start = DateTime.Now.AddDays(-10),
                    End = DateTime.Now,
                    PatientId = 1
                });

            modelBuilder.Entity<Patient>()
                .HasData(new Patient
                {
                    Id = 1,
                    Name = "Test",
                    Surname = "Testenko",
                    DateOfBirth = DateTime.Now.AddYears(-18),
                    DiseaseId = 1,
                });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
