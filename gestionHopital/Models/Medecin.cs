using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionHopital.Models
{
    public class Medecin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_medecin { get; set; }

        public int UtilisateurID { get; set; }

        [ForeignKey("UtilisateurID")]
        public Utilisateur? Utilisateur { get; set; }

        public string Specialisation { get; set; }

        // Foreign key to Departement
        public int DepartementID { get; set; }

        [ForeignKey("DepartementID")]
        public Departement? Departement { get; set; }

        // Navigation properties
        public ICollection<RendezVous> RendezVous { get; set; }
        public ICollection<Secretary> Secretaries { get; set; }

    }
}