using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestionHopital.Models
{
    public class Visite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVisite { get; set; }

        [Required]
        public int PatientID { get; set; }
        public Patient Patient { get; set; }

        [Required]
        public int MedecinID { get; set; }
        public Medecin Medecin { get; set; }
        [Required]
        public int montant { get; set; }

        [Required]
        public string Date { get; set; }

        public string? Notes { get; set; }
        public ICollection<VisiteMedicament> VisiteMedicaments { get; set; }
    }
}
