using gestionHopital.Data;
using gestionHopital.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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
        public IActionResult DeletePatient(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient is null) return NotFound();

            _context.Patients.Remove(patient);
            _context.SaveChanges();
            return Ok();
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

            // Update fields
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
