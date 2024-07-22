using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionHopital.Models
{
    public enum Genre
    {
        M,
        F
    }

    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_patient { get; set; }

        public int UtilisateurID { get; set; }

        [ForeignKey("UtilisateurID")]
        public Utilisateur? Utilisateur { get; set; }

        [Required]
        [EnumDataType(typeof(Genre))]
        public Genre Genre { get; set; }

        public string Adresse { get; set; }

        public string Historiquemedical { get; set; }
    }
}
