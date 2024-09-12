using gestionHopital.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class VisiteMedicament
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int VisiteId { get; set; }
    [ForeignKey("VisiteId")]
    public Visite Visite { get; set; }

    public int MedicamentID { get; set; }
    [ForeignKey("MedicamentID")]
    public Medicament Medicament { get; set; }

}
