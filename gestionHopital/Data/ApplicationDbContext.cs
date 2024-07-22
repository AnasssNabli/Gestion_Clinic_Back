using gestionHopital.Models;
using Microsoft.EntityFrameworkCore;

namespace gestionHopital.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medecin> Médecins { get; set; }
        public DbSet<Disponibilite> Disponibilités { get; set; }
        public DbSet<Departement> Départements { get; set; }
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

            // Configuration de la relation un à un entre Visite et Ordonnance
            modelBuilder.Entity<Visite>()
                .HasOne(v => v.Ordonnance)
                .WithOne(o => o.Visite)
                .HasForeignKey<Visite>(v => v.OrdonnanceID) // La clé étrangère est sur Visite
                .OnDelete(DeleteBehavior.Restrict); // Vous pouvez ajuster le comportement de suppression

            modelBuilder.Entity<Ordonnance>()
                .HasOne(o => o.Visite)
                .WithOne(v => v.Ordonnance)
                .HasForeignKey<Ordonnance>(o => o.VisiteID) // La clé étrangère est sur Ordonnance
                .OnDelete(DeleteBehavior.Restrict); // Vous pouvez ajuster le comportement de suppression

            // Configuration des relations entre Visite et Patient
            modelBuilder.Entity<Visite>()
                .HasOne(v => v.Patient)
                .WithMany() // Configuration de la relation entre Visite et Patient
                .HasForeignKey(v => v.PatientID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration des relations entre Visite et Medecin
            modelBuilder.Entity<Visite>()
                .HasOne(v => v.Medecin)
                .WithMany() // Configuration de la relation entre Visite et Médecin
                .HasForeignKey(v => v.MedecinID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
