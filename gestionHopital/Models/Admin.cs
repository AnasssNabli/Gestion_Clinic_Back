using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionHopital.Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_admin { get; set; }

        public int UtilisateurID { get; set; }

        [ForeignKey("UtilisateurID")]
        public Utilisateur? Utilisateur { get; set; }
    }
}
