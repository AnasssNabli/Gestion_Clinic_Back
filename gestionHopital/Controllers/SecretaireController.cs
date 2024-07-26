using gestionHopital.Data;
using gestionHopital.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gestionHopital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretaireController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SecretaireController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult getSecretaires() { 
            return Ok(_context.Secretaries.ToList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult getSecretaire(int id)
        {
            var secretaire = _context.Secretaries.Find(id);
            return secretaire is null ? NotFound() : Ok(secretaire); 
         
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult deleteSecretaire(int id)
        {
            var secretaire = _context.Secretaries.Find(id);
            if (secretaire is null) return NotFound();
            _context.Remove(secretaire); 
            _context.SaveChanges();
            return Ok();
        }


        [HttpPut("{id:int}")]
        public IActionResult UpdateSecretary(int id,  Secretary updatedSecretary)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            
            var secretary = _context.Secretaries.Include(s => s.Utilisateur) .Include(s => s.Supérieur) .FirstOrDefault(s => s.SecrétaireID == id);

            if (secretary is  null) return NotFound();
          
            secretary.Superieurid_medecin = updatedSecretary.Superieurid_medecin;

          
            if (secretary.Utilisateur is not null)
            {
                secretary.Utilisateur.Nom = updatedSecretary.Utilisateur.Nom;
                secretary.Utilisateur.Prenom = updatedSecretary.Utilisateur.Prenom;
                secretary.Utilisateur.Cin = updatedSecretary.Utilisateur.Cin; 
                secretary.Utilisateur.Email = updatedSecretary.Utilisateur.Email;
                secretary.Utilisateur.Telephone = updatedSecretary.Utilisateur.Telephone ;
                secretary.Utilisateur.DateNaissance = updatedSecretary.Utilisateur.DateNaissance;
            }

            if (updatedSecretary.Superieurid_medecin.HasValue)
            {
                var superiorMedecin = _context.Medecins.Find(updatedSecretary.Superieurid_medecin.Value);
                if (superiorMedecin is null)
                {
                    return BadRequest("Responsable not found");
                }
                secretary.Supérieur = superiorMedecin;
            }
            _context.SaveChanges();
            return Ok(secretary);
        }


    }
}
