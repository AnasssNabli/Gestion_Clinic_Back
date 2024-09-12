using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestionHopital.Models
{
    public class Medicament
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_medicament { get; set; } 
        public string Nom { get; set; } 
        public string Instructions { get; set; }
        public ICollection<VisiteMedicament> VisiteMedicaments { get; set; }
    }
}
