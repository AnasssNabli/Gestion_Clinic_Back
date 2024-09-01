using gestionHopital.Data;
using gestionHopital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

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
    // POST: api/Disponibiliteaddd

    [HttpPost("Disponibiliteaddd")]
    public async Task<ActionResult> DisponibiliteAddd([FromBody] List<DisponibiliteDtoo> disponibiliteDtos)

    {
        if (disponibiliteDtos == null || disponibiliteDtos.Count == 0)
            return BadRequest("No disponibilite data provided");

        var userId = disponibiliteDtos.First().id_user; // Extracting id_user from the first entry in the list

        // Find the Medecin associated with the provided user ID
        var medecin = await _context.Medecins.FirstOrDefaultAsync(m => m.UtilisateurID == userId);
        if (medecin == null)
            return NotFound("Medecin not found for the provided user ID");

        var disponibilites = disponibiliteDtos.Select(dto => new Disponibilite
        {
            Id_Medecin = medecin.Id_medecin,
            JourDeLaSemaine = dto.jourDeLaSemaine,
            HeureDebut = dto.HeureDebut,
            HeureFin = dto.HeureFin
        }).ToList();

        _context.Disponibilites.AddRange(disponibilites);
        await _context.SaveChangesAsync();

        return Ok("Disponibilites added successfully");
    }
    // GET: api/Disponibilite
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DisponibiliteDto>>> GetDisponibilites()
    {
        var userId = GetUserIdFromToken();
        if (userId == null)
            return Unauthorized();

        var medecinId = await GetMedecinIdForUser(userId.Value);
        if (medecinId == null)
            return NotFound("Medecin not found");

        var disponibilites = await _context.Disponibilites
            .Include(d => d.Medecin)
            .ThenInclude(m => m.Utilisateur)
            .Where(d => d.Id_Medecin == medecinId.Value)
            .Select(d => new DisponibiliteDto
            {
                Id_Disponibilite = d.Id_disponibilte,
                Id_Medecin = d.Id_Medecin,
                Medecin = new MedecinDto
                {
                    Nom = d.Medecin.Utilisateur.Nom,
                    Prenom = d.Medecin.Utilisateur.Prenom
                },
                JourDeLaSemaine = d.JourDeLaSemaine,
                HeureDebut = d.HeureDebut,
                HeureFin = d.HeureFin
            })
            .ToListAsync();

        return Ok(disponibilites);
    }

    // POST: api/Disponibilite
    [HttpPost]
    public async Task<ActionResult> PostDisponibilite([FromBody] JsonElement request)
    {
        var userId = GetUserIdFromToken();
        if (userId == null)
            return Unauthorized();

        var medecinId = await GetMedecinIdForUser(userId.Value);
        if (medecinId == null)
            return NotFound("Medecin not found");

        List<DisponibiliteDto> disponibiliteDtos;

        if (request.ValueKind == JsonValueKind.Array)
        {
            // Handle list of DisponibiliteDto
            disponibiliteDtos = JsonConvert.DeserializeObject<List<DisponibiliteDto>>(request.GetRawText());
        }
        else
        {
            // Handle single DisponibiliteDto
            disponibiliteDtos = new List<DisponibiliteDto> { JsonConvert.DeserializeObject<DisponibiliteDto>(request.GetRawText()) };
        }

        var disponibilites = new List<Disponibilite>();

        foreach (var disponibiliteDto in disponibiliteDtos)
        {
            var disponibilite = new Disponibilite
            {
                Id_Medecin = medecinId.Value,
                JourDeLaSemaine = disponibiliteDto.JourDeLaSemaine,
                HeureDebut = disponibiliteDto.HeureDebut,
                HeureFin = disponibiliteDto.HeureFin
            };

            disponibilites.Add(disponibilite);
        }

        _context.Disponibilites.AddRange(disponibilites);
        await _context.SaveChangesAsync();

        // Return a success message
        return Ok("Disponibilites created successfully");
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

    // DELETE: api/Disponibilite
    [HttpDelete]
    public async Task<IActionResult> DeleteDisponibilitesByDay([FromQuery] string jourDeLaSemaine)
    {
        if (string.IsNullOrWhiteSpace(jourDeLaSemaine))
        {
            return BadRequest("jourDeLaSemaine is required");
        }

        var userId = GetUserIdFromToken();
        if (userId == null)
            return Unauthorized();

        var medecinId = await GetMedecinIdForUser(userId.Value);
        if (medecinId == null)
            return NotFound("Medecin not found");

        // Convert jourDeLaSemaine to lowercase for case-insensitive comparison
        var jourDeLaSemaineLower = jourDeLaSemaine.ToLower();

        // Use .ToLower() for case-insensitive comparison in the query
        var disponibilitesToDelete = await _context.Disponibilites
            .Where(d => d.Id_Medecin == medecinId.Value &&
                        d.JourDeLaSemaine.ToLower() == jourDeLaSemaineLower)
            .ToListAsync();

        if (disponibilitesToDelete.Count == 0)
            return NoContent();

        _context.Disponibilites.RemoveRange(disponibilitesToDelete);
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
    public int Id_Disponibilite { get; set; }
    public int Id_Medecin { get; set; }
    public MedecinDto Medecin { get; set; }
    public string JourDeLaSemaine { get; set; }
    public string HeureDebut { get; set; }
    public string HeureFin { get; set; }
}
public class DisponibiliteDtoo
{
    
    public int id_user { get; set; }
    public string jourDeLaSemaine { get; set; }
    public string HeureDebut { get; set; }
    public string HeureFin { get; set; }
}
public class MedecinDto
{
    public string Nom { get; set; }
    public string Prenom { get; set; } // Added this line
}
