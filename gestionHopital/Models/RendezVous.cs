using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestionHopital.Models
{
    public class RendezVous
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_RendezVous { get; set; }

        public int Id_patient { get; set; }
        [ForeignKey("Id_patient")]
        public Patient? Patient { get; set; }

        public int Id_Medecin { get; set; }
        [ForeignKey("Id_Medecin")]
        public Medecin? Medecin { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Heure { get; set; }

        public string Raison { get; set; }

        [Required]
        
        public string Statut { get; set; }
    }
}
