using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestionHopital.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_Message { get; set; } 
        public int ExpéditeurID { get; set; } 
        public int DestinataireID { get; set; } 
        public string Contenu { get; set; } 
        public DateTime DateEnvoi { get; set; } 

      
        public Patient? ExpediteurPatient { get; set; }
        public Medecin? ExpediteurMédecin { get; set; } 
        public Patient? DestinatairePatient { get; set; }
        public Medecin? DestinataireMédecin { get; set; } 
    }
}
