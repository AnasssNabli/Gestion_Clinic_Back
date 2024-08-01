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
        public async Task<IActionResult> GetSecretaires()
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSecretaire(int id)
        {
            var secretaire = await _context.Secretaries.FindAsync(id);
            if (secretaire == null)
                return NotFound();

            _context.Secretaries.Remove(secretaire);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSecretaire(int id, SecretaryDto updatedSecretaryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

          
            var secretary = await _context.Secretaries
                .Include(s => s.Utilisateur)
                .Include(s => s.Supérieur)
                .FirstOrDefaultAsync(s => s.SecrétaireID == id);

            if (secretary is null)
                return NotFound();

           
            if (updatedSecretaryDto.DateNaissance.HasValue)
            {
                secretary.Utilisateur.DateNaissance = updatedSecretaryDto.DateNaissance.Value;
            }
            else
            {
           
            }

            secretary.Utilisateur.Email = updatedSecretaryDto.Email;
            secretary.Utilisateur.Nom = updatedSecretaryDto.Nom;
            secretary.Utilisateur.Prenom = updatedSecretaryDto.Prenom;
            secretary.Utilisateur.Telephone = updatedSecretaryDto.Telephone;

            
            if (updatedSecretaryDto.Superieurid_medecin.HasValue)
            {
                var superiorMedecin = await _context.Medecins.FindAsync(updatedSecretaryDto.Superieurid_medecin.Value);
                if (superiorMedecin == null)
                {
                    return BadRequest("Responsable not found");
                }
                secretary.Supérieur = superiorMedecin;
            }
            else
            {
                secretary.Supérieur = null;
            }

         
            await _context.SaveChangesAsync();
            return Ok("Secretary updated successfully");
        }


    }

    public class SecretaryDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public DateOnly? DateNaissance { get; set; }
        public int? Superieurid_medecin { get; set; }
        
    }

}
