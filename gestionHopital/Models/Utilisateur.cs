using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionHopital.Models
{
    public class Utilisateur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_utilisateur { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string MotDePasse { get; set; }

        [Phone]
        public string? Telephone { get; set; }


    }
}
