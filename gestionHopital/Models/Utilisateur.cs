using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace gestionHopital.Models
{
    public class Utilisateur : IdentityUser<int>
    {
        public string Nom { get; set; }

        public string Prenom { get; set; }
        public string Cin { get; set; }

        [Phone]
        public string? Telephone { get; set; }
        public DateOnly? DateNaissance { get; set; }
    }
}
