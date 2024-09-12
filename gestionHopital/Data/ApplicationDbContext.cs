using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using gestionHopital.Models;

namespace gestionHopital.Data
{
    public class ApplicationDbContext : IdentityDbContext<Utilisateur, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medecin> Medecins { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Secretary> Secretaries { get; set; }
        public DbSet<Disponibilite> Disponibilites { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<RendezVous> RendezVous { get; set; }
        public DbSet<Visite> Visites { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<VisiteMedicament> VisiteMedicaments { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration for Secretary and Utilisateur relationship
            modelBuilder.Entity<Secretary>()
                .HasOne(s => s.Utilisateur)
                .WithMany()
                .HasForeignKey(s => s.UtilisateurID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration for Secretary and Medecin relationship
            modelBuilder.Entity<Secretary>()
                .HasOne(s => s.Supérieur)
                .WithMany()
                .HasForeignKey(s => s.Superieurid_medecin)
                .OnDelete(DeleteBehavior.NoAction);

            // Configuration for Visite and Patient
            modelBuilder.Entity<Visite>()
                .HasOne(v => v.Patient)
                .WithMany()
                .HasForeignKey(v => v.PatientID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration for Visite and Medecin
            modelBuilder.Entity<Visite>()
                .HasOne(v => v.Medecin)
                .WithMany()
                .HasForeignKey(v => v.MedecinID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration for Utilisateur relationships with Patient, Medecin, Admin
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Utilisateur)
                .WithMany()
                .HasForeignKey(p => p.UtilisateurID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Medecin>()
                .HasOne(m => m.Utilisateur)
                .WithMany()
                .HasForeignKey(m => m.UtilisateurID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Admin>()
                .HasOne(a => a.Utilisateur)
                .WithMany()
                .HasForeignKey(a => a.UtilisateurID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration for RendezVous, Patient, and Medecin relationships
            modelBuilder.Entity<RendezVous>()
                .HasOne(r => r.Patient)
                .WithMany()
                .HasForeignKey(r => r.Id_patient)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RendezVous>()
                .HasOne(r => r.Medecin)
                .WithMany()
                .HasForeignKey(r => r.Id_Medecin)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration for Medecin and Departement
            modelBuilder.Entity<Medecin>()
                .HasOne(m => m.Departement)
                .WithMany()
                .HasForeignKey(m => m.DepartementID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration for VisiteMedicament
            modelBuilder.Entity<VisiteMedicament>()
                .HasOne(vm => vm.Visite)
                .WithMany(v => v.VisiteMedicaments)
                .HasForeignKey(vm => vm.VisiteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VisiteMedicament>()
                .HasOne(vm => vm.Medicament)
                .WithMany(m => m.VisiteMedicaments)
                .HasForeignKey(vm => vm.MedicamentID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
