using gestionHopital.Data;
using gestionHopital.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        public IActionResult GetDepartements()
        {
            var departements = _context.Departements.ToList();
            return Ok(departements);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetDepartement(int id)
        {
            var departement = _context.Departements.Find(id);

            if (departement == null)
            {
                return NotFound();
            }

            return Ok(departement);
        }

        [HttpPost]
        public IActionResult AddDepartement(Departement departement)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Departements.Add(departement);
            _context.SaveChanges();
            return Ok("departement enregistrer avec success");
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateDepartement(int id, [FromBody] Departement updatedDepartement)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (updatedDepartement == null) return BadRequest("UpdatedDepartement object is null");

            var departement = _context.Departements.Find(id);

            if (departement == null) return NotFound();

            departement.Nom = updatedDepartement.Nom;
            departement.Description = updatedDepartement.Description;

            _context.SaveChanges();
            return Ok(departement);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteDepartement(int id)
        {
            var departement = _context.Departements.Find(id);
            if (departement == null) return NotFound();

            _context.Departements.Remove(departement);
            _context.SaveChanges();
            return Ok("dep deleted successfully");
        }
    }
}
