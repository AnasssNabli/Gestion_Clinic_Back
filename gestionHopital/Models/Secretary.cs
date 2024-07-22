using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionHopital.Models
{
    public class Secretary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SecrétaireID { get; set; }

        [ForeignKey("Utilisateur")]
        public int UtilisateurID { get; set; }

        [ForeignKey("Supérieur")]
        public int? Superieurid_medecin { get; set; } // Foreign key to Medecin

        // Navigation properties
        public virtual Utilisateur Utilisateur { get; set; }
        public virtual Medecin Supérieur { get; set; }
    }
}
