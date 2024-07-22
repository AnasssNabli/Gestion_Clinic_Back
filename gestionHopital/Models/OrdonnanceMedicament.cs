using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestionHopital.Models
{
    public class OrdonnanceMedicament
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{ get; set; }
        
        public int OrdonnanceID { get; set; }
        [ForeignKey("OrdonnanceID")]
        public Ordonnance? Ordonnance { get; set; }

        public int MedicamentID { get; set; }
        [ForeignKey("MedicamentID")]
        public Medicament? Medicament { get; set; } 
        public int Quantite { get; set; }
    }
}
