using gestionHopital.Data;
using gestionHopital.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace gestionHopital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedecinController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedecinController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> getMedecins()
        {
            var medecins = await _context.Medecins
                                         .Include(m => m.Utilisateur)
                                         .ToListAsync();

            // Map to anonymous type to remove potential circular references
            var result = medecins.Select(m => new
            {
                Id_medecin = m.Id_medecin,
                UtilisateurID = m.UtilisateurID,
                Utilisateur = m.Utilisateur == null ? null : new
                {
                    m.Utilisateur.Nom,
                    m.Utilisateur.Prenom,
                    m.Utilisateur.Cin,
                    m.Utilisateur.Telephone,
                    m.Utilisateur.DateNaissance,
                    m.Utilisateur.Email
                },
                Specialisation = m.Specialisation
            });

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> getMedecin(int id)
        {
            var medecin = await _context.Medecins
                                        .Include(m => m.Utilisateur)
                                        .FirstOrDefaultAsync(m => m.Id_medecin == id);

            if (medecin == null)
            {
                return NotFound();
            }

            // Map to anonymous type to remove potential circular references
            var result = new
            {
                Id_medecin = medecin.Id_medecin,
                UtilisateurID = medecin.UtilisateurID,
                Utilisateur = medecin.Utilisateur == null ? null : new
                {
                    medecin.Utilisateur.Nom,
                    medecin.Utilisateur.Prenom,
                    medecin.Utilisateur.Cin,
                    medecin.Utilisateur.Telephone,
                    medecin.Utilisateur.DateNaissance,
                    medecin.Utilisateur.Email
                },
                Specialisation = medecin.Specialisation
            };

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMedecin(int id)
        {
            var medecin = await _context.Medecins.FindAsync(id);
            if (medecin == null)
            {
                return NotFound();
            }

            _context.Medecins.Remove(medecin);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMedecin(int id, [FromBody] Medecin updatedMedecin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (updatedMedecin == null)
            {
                return BadRequest("UpdatedMedecin object is null.");
            }

            var medecin = await _context.Medecins
                                        .Include(m => m.Utilisateur)
                                        .FirstOrDefaultAsync(m => m.Id_medecin == id);

            if (medecin == null)
            {
                return NotFound();
            }

            medecin.Specialisation = updatedMedecin.Specialisation;

            if (medecin.Utilisateur != null)
            {
                medecin.Utilisateur.Nom = updatedMedecin.Utilisateur.Nom;
                medecin.Utilisateur.Cin = updatedMedecin.Utilisateur.Cin;
                medecin.Utilisateur.Prenom = updatedMedecin.Utilisateur.Prenom;
                medecin.Utilisateur.Email = updatedMedecin.Utilisateur.Email;
                medecin.Utilisateur.Telephone = updatedMedecin.Utilisateur.Telephone;
                medecin.Utilisateur.DateNaissance = updatedMedecin.Utilisateur.DateNaissance;
            }

            await _context.SaveChangesAsync();

            return Ok(medecin);
        }
    }
}
