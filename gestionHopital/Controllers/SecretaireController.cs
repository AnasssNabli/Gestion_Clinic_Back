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
        public async Task<IActionResult> getSecretaires()
        {
            var secretaires = await _context.Secretaries
                .Include(s => s.Utilisateur)
                .Include(s => s.Supérieur)
                 .ThenInclude(m => m.Utilisateur) 
                .ToListAsync();

            var result = secretaires.Select(s => new
            {
                SecretaireID = s.SecrétaireID,
                UtilisateurID = s.UtilisateurID,
                Utilisateur = s.Utilisateur == null ? null : new
                {
                    s.Utilisateur.Nom,
                    s.Utilisateur.Prenom,
                    s.Utilisateur.Cin,
                    s.Utilisateur.Telephone,
                    s.Utilisateur.DateNaissance,
                    s.Utilisateur.Email
                },
                Superieur = s.Supérieur == null ? null : new
                {
                    s.Supérieur.Id_medecin,
                    Specialisation = s.Supérieur.Specialisation,
                    Utilisateur = s.Supérieur.Utilisateur == null ? null : new
                    {
                        s.Supérieur.Utilisateur.Nom,
                        s.Supérieur.Utilisateur.Prenom
                    }
                }
            });

            return Ok(result);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSecretaire(int id)
        {
            var secretaire = await _context.Secretaries
                .Include(s => s.Utilisateur)
                .Include(s => s.Supérieur)
                    .ThenInclude(m => m.Utilisateur) 
                .FirstOrDefaultAsync(s => s.SecrétaireID == id);

            if (secretaire == null)
            {
                return NotFound();
            }
            var result = new
            {
                SecretaireID = secretaire.SecrétaireID,
                UtilisateurID = secretaire.UtilisateurID,
                Utilisateur = secretaire.Utilisateur == null ? null : new
                {
                    secretaire.Utilisateur.Nom,
                    secretaire.Utilisateur.Prenom,
                    secretaire.Utilisateur.Cin,
                    secretaire.Utilisateur.Telephone,
                    secretaire.Utilisateur.DateNaissance,
                    secretaire.Utilisateur.Email
                },
                Superieur = secretaire.Supérieur == null ? null : new
                {
                    secretaire.Supérieur.Id_medecin,
                    Specialisation = secretaire.Supérieur.Specialisation,
                    Utilisateur = secretaire.Supérieur.Utilisateur == null ? null : new
                    {
                        secretaire.Supérieur.Utilisateur.Nom,
                        secretaire.Supérieur.Utilisateur.Prenom
                    }
                }
            };

            return Ok(result);
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
