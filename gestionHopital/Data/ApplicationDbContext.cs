using gestionHopital.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Ordonnance> Ordonnances { get; set; }
        public DbSet<Medicament> Médicaments { get; set; }
        public DbSet<OrdonnanceMedicament> OrdonnanceMédicaments { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration des relations entre Secretary et Utilisateur
            modelBuilder.Entity<Secretary>()
                .HasOne(s => s.Utilisateur)
                .WithMany()
                .HasForeignKey(s => s.UtilisateurID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration des relations entre Secretary et Medecin
            modelBuilder.Entity<Secretary>()
                .HasOne(s => s.Supérieur)
                .WithMany()
                .HasForeignKey(s => s.Superieurid_medecin)
                .OnDelete(DeleteBehavior.NoAction);

            // Configuration de la relation un à un entre Visite et Ordonnance
            modelBuilder.Entity<Visite>()
                .HasOne(v => v.Ordonnance)
                .WithOne(o => o.Visite)
                .HasForeignKey<Visite>(v => v.OrdonnanceID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ordonnance>()
                .HasOne(o => o.Visite)
                .WithOne(v => v.Ordonnance)
                .HasForeignKey<Ordonnance>(o => o.VisiteID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration des relations entre Visite et Patient
            modelBuilder.Entity<Visite>()
                .HasOne(v => v.Patient)
                .WithMany()
                .HasForeignKey(v => v.PatientID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration des relations entre Visite et Medecin
            modelBuilder.Entity<Visite>()
                .HasOne(v => v.Medecin)
                .WithMany()
                .HasForeignKey(v => v.MedecinID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration des relations entre Utilisateur et Patient, Medecin, Admin
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

            // Configuration des relations entre RendezVous, Patient et Medecin
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
        }
    }
}
