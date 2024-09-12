using gestionHopital.Data;
using gestionHopital.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gestionHopital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetPatients()
        {
            var patients = _context.Patients.Include(p => p.Utilisateur).ToList();
            return Ok(patients);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetPatient(int id)
        {
            var patient = _context.Patients.Include(p => p.Utilisateur).FirstOrDefault(p => p.Id_patient == id);
            return patient is null ? NotFound() : Ok(patient);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            // Rechercher le patient
            var patient = await _context.Patients
                                        .Include(p => p.RendezVous) // Inclure les rendez-vous associés
                                        .FirstOrDefaultAsync(p => p.Id_patient == id);
            if (patient == null)
                return NotFound();

            // Supprimer les rendez-vous associés
            var rendezVous = _context.RendezVous.Where(r => r.Id_patient == id).ToList();
            if (rendezVous.Any())
            {
                _context.RendezVous.RemoveRange(rendezVous);
            }

            // Supprimer les visites associées et les VisiteMedicaments
            var visites = _context.Visites.Where(v => v.PatientID == id).ToList();
            foreach (var visite in visites)
            {
                var visiteMedicaments = _context.VisiteMedicaments.Where(vm => vm.VisiteId == visite.IdVisite).ToList();
                if (visiteMedicaments.Any())
                {
                    _context.VisiteMedicaments.RemoveRange(visiteMedicaments);
                }
                _context.Visites.Remove(visite);
            }

            // Supprimer le patient
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return Ok($"Patient avec ID {id} et ses rendez-vous et visites associés ont été supprimés.");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] PatientDto updatedPatientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patient = await _context.Patients
                .Include(p => p.Utilisateur)
                .FirstOrDefaultAsync(p => p.Id_patient == id);

            if (patient is null)
                return NotFound();

            // Mise à jour des champs
            if (updatedPatientDto.DateNaissance.HasValue)
                patient.Utilisateur.DateNaissance = updatedPatientDto.DateNaissance.Value;

            if (Enum.TryParse<Genre>(updatedPatientDto.Genre, out var genre))
            {
                patient.Genre = genre;
            }
            else
            {
                ModelState.AddModelError("Genre", "Invalid genre value.");
                return BadRequest(ModelState);
            }

            patient.Utilisateur.Email = updatedPatientDto.Email;
            patient.Utilisateur.Nom = updatedPatientDto.Nom;
            patient.Utilisateur.Prenom = updatedPatientDto.Prenom;
            patient.Utilisateur.Telephone = updatedPatientDto.Telephone;
            patient.Adresse = updatedPatientDto.Adresse;
            patient.Historiquemedical = updatedPatientDto.HistoriqueMedical;

            await _context.SaveChangesAsync();
            return Ok("Patient updated successfully");
        }
    }

    public class PatientDto
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Cin { get; set; }
        public string Telephone { get; set; }
        public DateOnly? DateNaissance { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }
        public string HistoriqueMedical { get; set; }

        [Required]
        public string Genre { get; set; }
        public string? Password { get; set; }
    }
}
