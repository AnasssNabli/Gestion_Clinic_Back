using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionHopital.Models
{
    public class Departement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_dep{ get; set;  }

        public string Nom;

        public string Description;

        
        public int ResponsableDepartement { get; set; }
        [ForeignKey("ResponsableDepartement")]
        public Medecin? Medecin { get; set; }


    }
}
