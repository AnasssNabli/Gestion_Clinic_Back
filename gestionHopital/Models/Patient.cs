using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestionHopital.Models


{

    public enum Genre
    {
        H,
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
        public DateOnly DateDeNaissance { get; set; }

        [Required]
        [EnumDataType(typeof(Genre))]
        public Genre Genre { get; set; }

        public string Adresse { get; set; }

        [Phone]
        public string Telephone { get; set; }

        public string Historiqueeédical { get; set; }
    }
}
