using gestionHopital.Data;
using gestionHopital.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return Ok(_context.Patients.Include(p => p.Utilisateur).ToList());
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
        public IActionResult UpdatePatient(int id,Patient Patient)
        {
            return Ok(Patient);

            if (Patient is null)
            {
                return BadRequest("The updatedPatient field is required");
            }

            var patient = _context.Patients.Include(p => p.Utilisateur).FirstOrDefault(p => p.Id_patient == id);
            if (patient is null) return NotFound();

           
            patient.Genre = Patient.Genre;
            patient.Adresse = Patient.Adresse;
            patient.Historiquemedical = Patient.Historiquemedical;

           
            if (patient.Utilisateur is not null && Patient.Utilisateur is not null)
            {
                patient.Utilisateur.Nom = Patient.Utilisateur.Nom ?? patient.Utilisateur.Nom;
                patient.Utilisateur.Prenom = Patient.Utilisateur.Prenom ?? patient.Utilisateur.Prenom;
                patient.Utilisateur.Email = Patient.Utilisateur.Email ?? patient.Utilisateur.Email;
                patient.Utilisateur.Telephone = Patient.Utilisateur.Telephone ?? patient.Utilisateur.Telephone;
                patient.Utilisateur.DateNaissance = Patient.Utilisateur.DateNaissance ?? patient.Utilisateur.DateNaissance;
            }

            _context.SaveChanges();
            return Ok(patient);
        }
    }
}
