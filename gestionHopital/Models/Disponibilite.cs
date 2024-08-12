using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestionHopital.Models
{
    public class Disponibilite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_disponibilte { get; set; }

       
        public int  Id_Medecin { get; set; }
        [ForeignKey("Id_Medecin")]
        public Medecin? Medecin { get; set; }

        [Required]
        public string JourDeLaSemaine { get; set; }

        [Required]
        public string HeureDebut { get; set; }

        [Required]
        public string HeureFin { get; set; }
    }
}
