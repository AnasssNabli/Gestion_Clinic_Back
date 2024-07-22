using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestionHopital.Models
{
    public enum StatutRendezVous
    {
        Planifie,
        Complete,
        Annule
    }

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
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Heure { get; set; }

        public string Raison { get; set; }

        [Required]
        [EnumDataType(typeof(StatutRendezVous))]
        public StatutRendezVous Statut { get; set; }
    }
}
