using Microsoft.EntityFrameworkCore;
using Cw11.Models;

namespace Cw11.Data;

public class DatabaseContext : DbContext {
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });
        
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "Kowalski", Email = "jan.kowalski@example.com" },
            new Doctor { IdDoctor = 2, FirstName = "Anna", LastName = "Nowak", Email = "anna.nowak@example.com" }
        );
        
        modelBuilder.Entity<Patient>().HasData(
            new Patient { IdPatient = 1, FirstName = "Piotr", LastName = "Zieliński", Birthdate = new DateTime(1985, 7, 12) },
            new Patient { IdPatient = 2, FirstName = "Maria", LastName = "Wiśniewska", Birthdate = new DateTime(1990, 3, 22) }
        );
        
        modelBuilder.Entity<Medicament>().HasData(
            new Medicament { IdMedicament = 1, Name = "Ibuprofen", Description = "Lek przeciwbólowy", Type = "NSAID" },
            new Medicament { IdMedicament = 2, Name = "Paracetamol", Description = "Środek przeciwgorączkowy", Type = "Analgetic" },
            new Medicament { IdMedicament = 3, Name = "Amoksycylina", Description = "Antybiotyk", Type = "Antibiotic" }
        );
    }
}