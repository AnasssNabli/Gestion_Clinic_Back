﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using gestionHopital.Data;

#nullable disable

namespace gestionHopital.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("VisiteMedicament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MedicamentID")
                        .HasColumnType("int");

                    b.Property<int>("VisiteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicamentID");

                    b.HasIndex("VisiteId");

                    b.ToTable("VisiteMedicaments");
                });

            modelBuilder.Entity("gestionHopital.Models.Admin", b =>
                {
                    b.Property<int>("Id_admin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_admin"));

                    b.Property<int>("UtilisateurID")
                        .HasColumnType("int");

                    b.HasKey("Id_admin");

                    b.HasIndex("UtilisateurID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("gestionHopital.Models.Departement", b =>
                {
                    b.Property<int>("Id_dep")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_dep"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_dep");

                    b.ToTable("Departements");
                });

            modelBuilder.Entity("gestionHopital.Models.Disponibilite", b =>
                {
                    b.Property<int>("Id_disponibilte")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_disponibilte"));

                    b.Property<string>("HeureDebut")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeureFin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id_Medecin")
                        .HasColumnType("int");

                    b.Property<string>("JourDeLaSemaine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_disponibilte");

                    b.HasIndex("Id_Medecin");

                    b.ToTable("Disponibilites");
                });

            modelBuilder.Entity("gestionHopital.Models.Medecin", b =>
                {
                    b.Property<int>("Id_medecin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_medecin"));

                    b.Property<int>("DepartementID")
                        .HasColumnType("int");

                    b.Property<string>("Specialisation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UtilisateurID")
                        .HasColumnType("int");

                    b.HasKey("Id_medecin");

                    b.HasIndex("DepartementID");

                    b.HasIndex("UtilisateurID");

                    b.ToTable("Medecins");
                });

            modelBuilder.Entity("gestionHopital.Models.Medicament", b =>
                {
                    b.Property<int>("Id_medicament")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_medicament"));

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_medicament");

                    b.ToTable("Medicaments");
                });

            modelBuilder.Entity("gestionHopital.Models.Patient", b =>
                {
                    b.Property<int>("Id_patient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_patient"));

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("Historiquemedical")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UtilisateurID")
                        .HasColumnType("int");

                    b.HasKey("Id_patient");

                    b.HasIndex("UtilisateurID");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("gestionHopital.Models.RendezVous", b =>
                {
                    b.Property<int>("Id_RendezVous")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_RendezVous"));

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Heure")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id_Medecin")
                        .HasColumnType("int");

                    b.Property<int>("Id_patient")
                        .HasColumnType("int");

                    b.Property<int?>("MedecinId_medecin")
                        .HasColumnType("int");

                    b.Property<int?>("PatientId_patient")
                        .HasColumnType("int");

                    b.Property<string>("Raison")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Statut")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_RendezVous");

                    b.HasIndex("Id_Medecin");

                    b.HasIndex("Id_patient");

                    b.HasIndex("MedecinId_medecin");

                    b.HasIndex("PatientId_patient");

                    b.ToTable("RendezVous");
                });

            modelBuilder.Entity("gestionHopital.Models.Secretary", b =>
                {
                    b.Property<int>("SecrétaireID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SecrétaireID"));

                    b.Property<int?>("MedecinId_medecin")
                        .HasColumnType("int");

                    b.Property<int?>("Superieurid_medecin")
                        .HasColumnType("int");

                    b.Property<int>("UtilisateurID")
                        .HasColumnType("int");

                    b.HasKey("SecrétaireID");

                    b.HasIndex("MedecinId_medecin");

                    b.HasIndex("Superieurid_medecin");

                    b.HasIndex("UtilisateurID");

                    b.ToTable("Secretaries");
                });

            modelBuilder.Entity("gestionHopital.Models.Utilisateur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Cin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("DateNaissance")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("gestionHopital.Models.Visite", b =>
                {
                    b.Property<int>("IdVisite")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdVisite"));

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MedecinID")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.Property<int>("montant")
                        .HasColumnType("int");

                    b.HasKey("IdVisite");

                    b.HasIndex("MedecinID");

                    b.HasIndex("PatientID");

                    b.ToTable("Visites");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("gestionHopital.Models.Utilisateur", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("gestionHopital.Models.Utilisateur", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("gestionHopital.Models.Utilisateur", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("gestionHopital.Models.Utilisateur", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VisiteMedicament", b =>
                {
                    b.HasOne("gestionHopital.Models.Medicament", "Medicament")
                        .WithMany("VisiteMedicaments")
                        .HasForeignKey("MedicamentID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("gestionHopital.Models.Visite", "Visite")
                        .WithMany("VisiteMedicaments")
                        .HasForeignKey("VisiteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Medicament");

                    b.Navigation("Visite");
                });

            modelBuilder.Entity("gestionHopital.Models.Admin", b =>
                {
                    b.HasOne("gestionHopital.Models.Utilisateur", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("gestionHopital.Models.Disponibilite", b =>
                {
                    b.HasOne("gestionHopital.Models.Medecin", "Medecin")
                        .WithMany()
                        .HasForeignKey("Id_Medecin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medecin");
                });

            modelBuilder.Entity("gestionHopital.Models.Medecin", b =>
                {
                    b.HasOne("gestionHopital.Models.Departement", "Departement")
                        .WithMany()
                        .HasForeignKey("DepartementID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("gestionHopital.Models.Utilisateur", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departement");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("gestionHopital.Models.Patient", b =>
                {
                    b.HasOne("gestionHopital.Models.Utilisateur", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("gestionHopital.Models.RendezVous", b =>
                {
                    b.HasOne("gestionHopital.Models.Medecin", "Medecin")
                        .WithMany()
                        .HasForeignKey("Id_Medecin")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("gestionHopital.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("Id_patient")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("gestionHopital.Models.Medecin", null)
                        .WithMany("RendezVous")
                        .HasForeignKey("MedecinId_medecin");

                    b.HasOne("gestionHopital.Models.Patient", null)
                        .WithMany("RendezVous")
                        .HasForeignKey("PatientId_patient");

                    b.Navigation("Medecin");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("gestionHopital.Models.Secretary", b =>
                {
                    b.HasOne("gestionHopital.Models.Medecin", null)
                        .WithMany("Secretaries")
                        .HasForeignKey("MedecinId_medecin");

                    b.HasOne("gestionHopital.Models.Medecin", "Supérieur")
                        .WithMany()
                        .HasForeignKey("Superieurid_medecin")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("gestionHopital.Models.Utilisateur", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supérieur");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("gestionHopital.Models.Visite", b =>
                {
                    b.HasOne("gestionHopital.Models.Medecin", "Medecin")
                        .WithMany()
                        .HasForeignKey("MedecinID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("gestionHopital.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Medecin");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("gestionHopital.Models.Medecin", b =>
                {
                    b.Navigation("RendezVous");

                    b.Navigation("Secretaries");
                });

            modelBuilder.Entity("gestionHopital.Models.Medicament", b =>
                {
                    b.Navigation("VisiteMedicaments");
                });

            modelBuilder.Entity("gestionHopital.Models.Patient", b =>
                {
                    b.Navigation("RendezVous");
                });

            modelBuilder.Entity("gestionHopital.Models.Visite", b =>
                {
                    b.Navigation("VisiteMedicaments");
                });
#pragma warning restore 612, 618
        }
    }
}
