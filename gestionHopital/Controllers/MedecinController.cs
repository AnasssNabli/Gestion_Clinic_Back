using gestionHopital.Data;
using gestionHopital.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
                                         .Include(m => m.Departement)
                                         .ToListAsync();

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
                Specialisation = m.Specialisation,
                DepartementID = m.DepartementID,
                Departement = m.Departement == null ? null : new
                {
                    m.Departement.Nom
                }
            });

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> getMedecin(int id)
        {
            var medecin = await _context.Medecins
                                        .Include(m => m.Utilisateur)
                                        .Include(m => m.Departement)
                                        .FirstOrDefaultAsync(m => m.Id_medecin == id);

            if (medecin == null)
            {
                return NotFound();
            }

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
                Specialisation = medecin.Specialisation,
                DepartementID = medecin.DepartementID,
                Departement = medecin.Departement == null ? null : new
                {
                    medecin.Departement.Nom
                }
            };

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMedecin(int id)
        {
            // Rechercher les rendez-vous associés au médecin
            var rendezVous = _context.RendezVous.Where(r => r.Id_Medecin == id).ToList();
            if (rendezVous.Any())
            {
                // Supprimer les médicaments associés aux visites
                var visiteIds = rendezVous.SelectMany(r => _context.Visites.Where(v => v.MedecinID == id).Select(v => v.IdVisite)).ToList();
                if (visiteIds.Any())
                {
                    var visiteMedicaments = _context.VisiteMedicaments.Where(vm => visiteIds.Contains(vm.VisiteId)).ToList();
                    if (visiteMedicaments.Any())
                    {
                        _context.VisiteMedicaments.RemoveRange(visiteMedicaments);
                    }
                }

                // Supprimer les visites associées
                var visites = _context.Visites.Where(v => v.MedecinID == id).ToList();
                if (visites.Any())
                {
                    _context.Visites.RemoveRange(visites);
                }

                // Supprimer les rendez-vous associés
                _context.RendezVous.RemoveRange(rendezVous);
            }

            // Supprimer les secrétaires associés
            var secretaries = _context.Secretaries.Where(s => s.Superieurid_medecin == id).ToList();
            if (secretaries.Any())
            {
                _context.Secretaries.RemoveRange(secretaries);
            }

            // Supprimer le médecin
            var medecin = await _context.Medecins.FindAsync(id);
            if (medecin != null)
            {
                _context.Medecins.Remove(medecin);
            }

            await _context.SaveChangesAsync();

            return Ok($"Médecin avec ID {id} supprimé ainsi que ses rendez-vous, visites, médicaments, et secrétaires associés.");
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMedecin(int id, [FromBody] MedecinDto updatedMedecinDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (updatedMedecinDto == null)
            {
                return BadRequest("UpdatedMedecinDto object is null.");
            }

            var medecin = await _context.Medecins
                                        .Include(m => m.Utilisateur)
                                        .Include(m => m.Departement)
                                        .FirstOrDefaultAsync(m => m.Id_medecin == id);

            if (medecin == null)
            {
                return NotFound();
            }

            // Mise à jour des informations
            medecin.Specialisation = updatedMedecinDto.Specialisation;
            medecin.DepartementID = updatedMedecinDto.DepartementID;
            medecin.Utilisateur.Nom = updatedMedecinDto.Nom;
            medecin.Utilisateur.Prenom = updatedMedecinDto.Prenom;
            medecin.Utilisateur.Email = updatedMedecinDto.Email;
            medecin.Utilisateur.Telephone = updatedMedecinDto.Telephone;
            medecin.Utilisateur.Cin = updatedMedecinDto.Cin;
            medecin.Utilisateur.DateNaissance = updatedMedecinDto.DateNaissance;

            await _context.SaveChangesAsync();

            return Ok("Medecin updated successfully");
        }

        public class MedecinDto
        {
            public string Specialisation { get; set; }
            public int DepartementID { get; set; }
            public string Nom { get; set; }
            public string Prenom { get; set; }
            public string Email { get; set; }
            public string Telephone { get; set; }
            public string Cin { get; set; }
            public DateOnly DateNaissance { get; set; }
        }
    }
}