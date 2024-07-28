using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionHopital.Models
{
    public class Departement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_dep { get; set; }

        public string Nom { get; set; }

        public string Description { get; set; }
    }
}
