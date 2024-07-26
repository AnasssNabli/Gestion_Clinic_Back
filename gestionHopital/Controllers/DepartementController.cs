using gestionHopital.Data;
using gestionHopital.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gestionHopital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartementController : ControllerBase
    {
        private readonly ApplicationDbContext _context; 
        public DepartementController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult getDepartement() {
            return Ok(_context.Departements.ToList());
        }

        [HttpGet("{id:int}")]
        public IActionResult getDepartement(int id)
        {
            var departement = _context.Departements.Find(id); 
            return departement is null ? NotFound() : Ok(departement);
        }


        [HttpPost]
        public IActionResult AddDepartement(Departement departement)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var depa = new Departement();
            depa.Nom = departement.Nom; 
            depa.Description = departement.Description;
            depa.ResponsableDepartement = departement.ResponsableDepartement; 

            _context.Departements.Add(depa);
            _context.SaveChanges();
            return Ok(depa);   
        }

        
        [HttpPut("{id:int}")]
        public IActionResult UpdateDepartement(int id, [FromBody] Departement updatedDepartement)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (updatedDepartement is null) return BadRequest("UpdatedDepartement object is null");

            var departement = _context.Departements.Include(d => d.Medecin).FirstOrDefault(d => d.Id_dep == id); 

            if (departement is null) return BadRequest(); 

            departement.Nom = updatedDepartement.Nom;

            departement.Description = updatedDepartement.Description;
            departement.ResponsableDepartement = updatedDepartement.ResponsableDepartement;

            if (departement.ResponsableDepartement != updatedDepartement.ResponsableDepartement)
            {
                var medecinResponsable = _context.Medecins.Find(updatedDepartement.ResponsableDepartement);
                if (medecinResponsable is null)
                {
                    return BadRequest("not found");
                }

                departement.Medecin = medecinResponsable;
            }
            _context.SaveChanges();
            return Ok(departement);
        }

        [HttpDelete("{id:int}")]
        public IActionResult deleteDepartement(int id)
        {
            var departement = _context.Departements.Find(id);
            if (departement is null) return NotFound();
            _context.Departements.Remove(departement);
            _context.SaveChanges();
            return Ok();
        }

    }
}
