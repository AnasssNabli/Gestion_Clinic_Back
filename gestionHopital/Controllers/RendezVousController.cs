using gestionHopital.Data;
using gestionHopital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class RendezVousController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public RendezVousController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    // POST: api/RendezVous
    [HttpPost]
    public async Task<ActionResult> PostRendezVous([FromBody] RendezVousDto model)
    {
        var patientId = GetPatientIdFromToken();
        if (patientId == null)
        {
            return Unauthorized();
        }

        var rendezVous = new RendezVous
        {
            Id_patient = patientId.Value,
            Id_Medecin = model.Id_Medecin,
            Date = model.Date,
            Heure = model.Heure,
            Raison = model.Raison,
            Statut = model.Statut
        };

        _context.RendezVous.Add(rendezVous);
        await _context.SaveChangesAsync();

        return Ok("RendezVous created successfully");
    }

    // GET: api/RendezVous
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RendezVousDto>>> GetRendezVous()
    {
        var userId = GetUserIdFromToken();
        if (userId == null)
            return Unauthorized();

        var rendezVousQuery = _context.RendezVous
            .Include(r => r.Patient)
                .ThenInclude(p => p.Utilisateur)
            .Include(r => r.Medecin)
                .ThenInclude(m => m.Utilisateur)
            .AsQueryable();

        var patientId = await GetPatientIdForUser(userId.Value);
        if (patientId != null)
        {
            rendezVousQuery = rendezVousQuery.Where(r => r.Id_patient == patientId.Value);
        }
        else
        {
            var medecinId = await GetMedecinIdForUser(userId.Value);
            if (medecinId != null)
            {
                rendezVousQuery = rendezVousQuery.Where(r => r.Id_Medecin == medecinId.Value);
            }
            else
            {
                var secretary = await _context.Secretaries.FirstOrDefaultAsync(s => s.UtilisateurID == userId.Value);
                if (secretary != null)
                {
                    var superiorMedecinId = secretary.Superieurid_medecin;
                    rendezVousQuery = rendezVousQuery.Where(r => r.Id_Medecin == superiorMedecinId);
                }
            }
        }

        var rendezVousList = await rendezVousQuery.ToListAsync();

        var rendezVousDtos = rendezVousList.Select(r => new RendezVousDto
        {
            Id_RendezVous = r.Id_RendezVous,
            Id_patient = r.Id_patient,
            PatientName = r.Patient != null ? r.Patient.Utilisateur != null ? r.Patient.Utilisateur.Nom + " " + r.Patient.Utilisateur.Prenom : "Unknown" : "Unknown",
            Id_Medecin = r.Id_Medecin,
            MedecinName = r.Medecin != null ? r.Medecin.Utilisateur != null ? r.Medecin.Utilisateur.Nom + " " + r.Medecin.Utilisateur.Prenom : "Unknown" : "Unknown",
            Date = r.Date,
            Heure = r.Heure,
            Raison = r.Raison,
            Statut = r.Statut
        }).ToList();

        return Ok(rendezVousDtos);
    }

    // DELETE: api/RendezVous/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRendezVous(int id)
    {
        var rendezVous = await _context.RendezVous.FindAsync(id);
        if (rendezVous == null)
            return NotFound();

        _context.RendezVous.Remove(rendezVous);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PUT: api/RendezVous/UpdateStatut/5
    [HttpPut("UpdateStatut/{id}")]
    public async Task<IActionResult> UpdateRendezVousStatut(int id, [FromBody] string statut)
    {
        var rendezVous = await _context.RendezVous.FindAsync(id);
        if (rendezVous == null)
            return NotFound();

        rendezVous.Statut = statut;
        _context.Entry(rendezVous).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Helper method to get the patient ID from the JWT token
    private int? GetPatientIdFromToken()
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
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var patientId))
            {
                return patientId;
            }
        }
        catch
        {
            return null;
        }

        return null;
    }

    // Helper method to get the user ID from the JWT token
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

    // Helper method to get the Patient ID for a given user ID
    private async Task<int?> GetPatientIdForUser(int userId)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UtilisateurID == userId);
        return patient?.Id_patient;
    }

    // Helper method to get the Medecin ID for a given user ID
    private async Task<int?> GetMedecinIdForUser(int userId)
    {
        var medecin = await _context.Medecins.FirstOrDefaultAsync(m => m.UtilisateurID == userId);
        return medecin?.Id_medecin;
    }
}

// DTO class
public class RendezVousDto
{
    public int Id_RendezVous { get; set; }
    public int Id_patient { get; set; }
    public string PatientName { get; set; }
    public int Id_Medecin { get; set; }
    public string MedecinName { get; set; }
    public string Date { get; set; }
    public string Heure { get; set; }
    public string Raison { get; set; }
    public string Statut { get; set; }
}
