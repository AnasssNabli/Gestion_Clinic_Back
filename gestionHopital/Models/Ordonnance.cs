using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestionHopital.Models
{
    public class Ordonnance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Ordonnance { get; set; }

        public int VisiteID { get; set; }
        public Visite? Visite { get; set; }
    }
}

