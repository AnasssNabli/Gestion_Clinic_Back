using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestionHopital.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateNaissance = table.Column<DateOnly>(type: "date", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departements",
                columns: table => new
                {
                    Id_dep = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departements", x => x.Id_dep);
                });

            migrationBuilder.CreateTable(
                name: "Médicaments",
                columns: table => new
                {
                    Id_medicament = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Médicaments", x => x.Id_medicament);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id_admin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilisateurID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id_admin);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_UtilisateurID",
                        column: x => x.UtilisateurID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id_patient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilisateurID = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Historiquemedical = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id_patient);
                    table.ForeignKey(
                        name: "FK_Patients_AspNetUsers_UtilisateurID",
                        column: x => x.UtilisateurID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medecins",
                columns: table => new
                {
                    Id_medecin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilisateurID = table.Column<int>(type: "int", nullable: false),
                    Specialisation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medecins", x => x.Id_medecin);
                    table.ForeignKey(
                        name: "FK_Medecins_AspNetUsers_UtilisateurID",
                        column: x => x.UtilisateurID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medecins_Departements_DepartementID",
                        column: x => x.DepartementID,
                        principalTable: "Departements",
                        principalColumn: "Id_dep",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    Id_Facture = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    Montant = table.Column<decimal>(type: "decimal(16,2)", precision: 16, scale: 2, nullable: false),
                    Paiement = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.Id_Facture);
                    table.ForeignKey(
                        name: "FK_Factures_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "Id_patient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disponibilites",
                columns: table => new
                {
                    Id_disponibilte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Medecin = table.Column<int>(type: "int", nullable: false),
                    JourDeLaSemaine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeureDebut = table.Column<TimeSpan>(type: "time", nullable: false),
                    HeureFin = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disponibilites", x => x.Id_disponibilte);
                    table.ForeignKey(
                        name: "FK_Disponibilites_Medecins_Id_Medecin",
                        column: x => x.Id_Medecin,
                        principalTable: "Medecins",
                        principalColumn: "Id_medecin",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    ID_Message = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpéditeurID = table.Column<int>(type: "int", nullable: false),
                    DestinataireID = table.Column<int>(type: "int", nullable: false),
                    Contenu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateEnvoi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpediteurPatientId_patient = table.Column<int>(type: "int", nullable: true),
                    ExpediteurMédecinId_medecin = table.Column<int>(type: "int", nullable: true),
                    DestinatairePatientId_patient = table.Column<int>(type: "int", nullable: true),
                    DestinataireMédecinId_medecin = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.ID_Message);
                    table.ForeignKey(
                        name: "FK_Messages_Medecins_DestinataireMédecinId_medecin",
                        column: x => x.DestinataireMédecinId_medecin,
                        principalTable: "Medecins",
                        principalColumn: "Id_medecin");
                    table.ForeignKey(
                        name: "FK_Messages_Medecins_ExpediteurMédecinId_medecin",
                        column: x => x.ExpediteurMédecinId_medecin,
                        principalTable: "Medecins",
                        principalColumn: "Id_medecin");
                    table.ForeignKey(
                        name: "FK_Messages_Patients_DestinatairePatientId_patient",
                        column: x => x.DestinatairePatientId_patient,
                        principalTable: "Patients",
                        principalColumn: "Id_patient");
                    table.ForeignKey(
                        name: "FK_Messages_Patients_ExpediteurPatientId_patient",
                        column: x => x.ExpediteurPatientId_patient,
                        principalTable: "Patients",
                        principalColumn: "Id_patient");
                });

            migrationBuilder.CreateTable(
                name: "RendezVous",
                columns: table => new
                {
                    Id_RendezVous = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_patient = table.Column<int>(type: "int", nullable: false),
                    Id_Medecin = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Heure = table.Column<TimeSpan>(type: "time", nullable: false),
                    Raison = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RendezVous", x => x.Id_RendezVous);
                    table.ForeignKey(
                        name: "FK_RendezVous_Medecins_Id_Medecin",
                        column: x => x.Id_Medecin,
                        principalTable: "Medecins",
                        principalColumn: "Id_medecin",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RendezVous_Patients_Id_patient",
                        column: x => x.Id_patient,
                        principalTable: "Patients",
                        principalColumn: "Id_patient",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Secretaries",
                columns: table => new
                {
                    SecrétaireID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilisateurID = table.Column<int>(type: "int", nullable: false),
                    Superieurid_medecin = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secretaries", x => x.SecrétaireID);
                    table.ForeignKey(
                        name: "FK_Secretaries_AspNetUsers_UtilisateurID",
                        column: x => x.UtilisateurID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Secretaries_Medecins_Superieurid_medecin",
                        column: x => x.Superieurid_medecin,
                        principalTable: "Medecins",
                        principalColumn: "Id_medecin");
                });

            migrationBuilder.CreateTable(
                name: "Visites",
                columns: table => new
                {
                    IdVisite = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    MedecinID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrdonnanceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visites", x => x.IdVisite);
                    table.ForeignKey(
                        name: "FK_Visites_Medecins_MedecinID",
                        column: x => x.MedecinID,
                        principalTable: "Medecins",
                        principalColumn: "Id_medecin",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visites_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "Id_patient",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ordonnances",
                columns: table => new
                {
                    Id_Ordonnance = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisiteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordonnances", x => x.Id_Ordonnance);
                    table.ForeignKey(
                        name: "FK_Ordonnances_Visites_VisiteID",
                        column: x => x.VisiteID,
                        principalTable: "Visites",
                        principalColumn: "IdVisite",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdonnanceMédicaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdonnanceID = table.Column<int>(type: "int", nullable: false),
                    MedicamentID = table.Column<int>(type: "int", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdonnanceMédicaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdonnanceMédicaments_Médicaments_MedicamentID",
                        column: x => x.MedicamentID,
                        principalTable: "Médicaments",
                        principalColumn: "Id_medicament",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdonnanceMédicaments_Ordonnances_OrdonnanceID",
                        column: x => x.OrdonnanceID,
                        principalTable: "Ordonnances",
                        principalColumn: "Id_Ordonnance",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UtilisateurID",
                table: "Admins",
                column: "UtilisateurID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Disponibilites_Id_Medecin",
                table: "Disponibilites",
                column: "Id_Medecin");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_PatientID",
                table: "Factures",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Medecins_DepartementID",
                table: "Medecins",
                column: "DepartementID");

            migrationBuilder.CreateIndex(
                name: "IX_Medecins_UtilisateurID",
                table: "Medecins",
                column: "UtilisateurID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_DestinataireMédecinId_medecin",
                table: "Messages",
                column: "DestinataireMédecinId_medecin");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_DestinatairePatientId_patient",
                table: "Messages",
                column: "DestinatairePatientId_patient");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ExpediteurMédecinId_medecin",
                table: "Messages",
                column: "ExpediteurMédecinId_medecin");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ExpediteurPatientId_patient",
                table: "Messages",
                column: "ExpediteurPatientId_patient");

            migrationBuilder.CreateIndex(
                name: "IX_OrdonnanceMédicaments_MedicamentID",
                table: "OrdonnanceMédicaments",
                column: "MedicamentID");

            migrationBuilder.CreateIndex(
                name: "IX_OrdonnanceMédicaments_OrdonnanceID",
                table: "OrdonnanceMédicaments",
                column: "OrdonnanceID");

            migrationBuilder.CreateIndex(
                name: "IX_Ordonnances_VisiteID",
                table: "Ordonnances",
                column: "VisiteID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UtilisateurID",
                table: "Patients",
                column: "UtilisateurID");

            migrationBuilder.CreateIndex(
                name: "IX_RendezVous_Id_Medecin",
                table: "RendezVous",
                column: "Id_Medecin");

            migrationBuilder.CreateIndex(
                name: "IX_RendezVous_Id_patient",
                table: "RendezVous",
                column: "Id_patient");

            migrationBuilder.CreateIndex(
                name: "IX_Secretaries_Superieurid_medecin",
                table: "Secretaries",
                column: "Superieurid_medecin");

            migrationBuilder.CreateIndex(
                name: "IX_Secretaries_UtilisateurID",
                table: "Secretaries",
                column: "UtilisateurID");

            migrationBuilder.CreateIndex(
                name: "IX_Visites_MedecinID",
                table: "Visites",
                column: "MedecinID");

            migrationBuilder.CreateIndex(
                name: "IX_Visites_PatientID",
                table: "Visites",
                column: "PatientID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Disponibilites");

            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "OrdonnanceMédicaments");

            migrationBuilder.DropTable(
                name: "RendezVous");

            migrationBuilder.DropTable(
                name: "Secretaries");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Médicaments");

            migrationBuilder.DropTable(
                name: "Ordonnances");

            migrationBuilder.DropTable(
                name: "Visites");

            migrationBuilder.DropTable(
                name: "Medecins");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Departements");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
