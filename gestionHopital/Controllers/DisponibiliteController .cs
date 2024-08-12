using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gestionHopital.Data;
using gestionHopital.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace gestionHopital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DisponibiliteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public DisponibiliteController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Disponibilite
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disponibilite>>> GetDisponibilites()
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
                return Unauthorized();

            var medecinId = await GetMedecinIdForUser(userId.Value);
            if (medecinId == null)
                return NotFound("Medecin not found");

            return await _context.Disponibilites
                .Where(d => d.Id_Medecin == medecinId.Value)
                .ToListAsync();
        }

        // POST: api/Disponibilite
        // POST: api/Disponibilite
        [HttpPost]
        public async Task<ActionResult> PostDisponibilite(DisponibiliteDto disponibiliteDto)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
                return Unauthorized();

            var medecinId = await GetMedecinIdForUser(userId.Value);
            if (medecinId == null)
                return NotFound("Medecin not found");

            var disponibilite = new Disponibilite
            {
                Id_Medecin = medecinId.Value,
                JourDeLaSemaine = disponibiliteDto.JourDeLaSemaine,
                HeureDebut = disponibiliteDto.HeureDebut,
                HeureFin = disponibiliteDto.HeureFin
            };

            _context.Disponibilites.Add(disponibilite);
            await _context.SaveChangesAsync();

            // Return a success message
            return Ok("Disponibilite created successfully");
        }


        // PUT: api/Disponibilite/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisponibilite(int id, DisponibiliteDto disponibiliteDto)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
                return Unauthorized();

            var medecinId = await GetMedecinIdForUser(userId.Value);
            if (medecinId == null)
                return NotFound("Medecin not found");

            var disponibilite = await _context.Disponibilites.FindAsync(id);
            if (disponibilite == null)
                return NotFound();

            if (disponibilite.Id_Medecin != medecinId.Value)
                return Forbid();

            disponibilite.JourDeLaSemaine = disponibiliteDto.JourDeLaSemaine;
            disponibilite.HeureDebut = disponibiliteDto.HeureDebut;
            disponibilite.HeureFin = disponibiliteDto.HeureFin;

            _context.Entry(disponibilite).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private int? GetUserIdFromToken()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"]);
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["JwtConfig:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return null;
                }

                return int.Parse(userIdClaim.Value);
            }
            catch (Exception)
            {
                return null;
            }
        }


        private async Task<int?> GetMedecinIdForUser(int userId)
        {
            var secretary = await _context.Secretaries.FirstOrDefaultAsync(s => s.UtilisateurID == userId);
            if (secretary != null)
                return secretary.Superieurid_medecin;

            var medecin = await _context.Medecins.FirstOrDefaultAsync(m => m.UtilisateurID == userId);
            return medecin?.Id_medecin;
        }
    }

    public class DisponibiliteDto
    {
        public string JourDeLaSemaine { get; set; }
        public string HeureDebut { get; set; }
        public string HeureFin { get; set; }
    }
}
