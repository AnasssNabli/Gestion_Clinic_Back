using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionHopital.Models
{

    public class Facture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Facture { get; set; }
        public int PatientID { get; set; }
        [ForeignKey("PatientID")]
        public Patient? Patient { get; set; }

        [Precision(16,2)]
        public decimal Montant { get; set; }
        public bool Paiement { get; set; } = false; 
    }
}
