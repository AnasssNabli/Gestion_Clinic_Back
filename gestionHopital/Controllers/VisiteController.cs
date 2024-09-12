using gestionHopital.Data;
using gestionHopital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Humanizer;  // Add this for number-to-words conversion
using System.Globalization;  // Add this for culture info

[Route("api/[controller]")]
[ApiController]
public class VisiteController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public VisiteController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    // POST: api/Visite
    [HttpPost]
    public async Task<ActionResult> CreateVisite([FromBody] VisiteDto model)
    {
        // Create the Visite object
        var visite = new Visite
        {
            MedecinID = model.Id_Medecin,
            PatientID = model.Id_patient,
            montant = model.Montant,
            Date = model.Date,
            Notes = model.Notes,
            VisiteMedicaments = model.Medicaments.Select(m => new VisiteMedicament
            {
                Medicament = new Medicament
                {
                    Nom = m.Nom,
                    Instructions = m.Instructions
                }
            }).ToList()
        };

        _context.Visites.Add(visite);
        await _context.SaveChangesAsync();

        return Ok("Visite created successfully");
    }

    // GET: api/Visite
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VisiteDtoo>>> GetVisites()
    {
        var userId = GetUserIdFromToken();
        if (userId == null)
        {
            return Unauthorized();
        }

        var visitesQuery = _context.Visites
            .Include(v => v.Patient)
                .ThenInclude(p => p.Utilisateur)
            .Include(v => v.Medecin)
                .ThenInclude(m => m.Utilisateur)
            .Include(v => v.VisiteMedicaments)
                .ThenInclude(vm => vm.Medicament)
            .AsQueryable();

        var patientId = await GetPatientIdForUser(userId.Value);
        if (patientId != null)
        {
            visitesQuery = visitesQuery.Where(v => v.PatientID == patientId.Value);
        }
        else
        {
            var medecinId = await GetMedecinIdForUser(userId.Value);
            if (medecinId != null)
            {
                visitesQuery = visitesQuery.Where(v => v.MedecinID == medecinId.Value);
            }
        }

        var visites = await visitesQuery.ToListAsync();

        var visiteDtos = visites.Select(v => new VisiteDtoo
        {
            Id_Visite = v.IdVisite,
            Id_Medecin = v.MedecinID,
            Id_patient = v.PatientID,
            PatientName = v.Patient != null && v.Patient.Utilisateur != null
                ? v.Patient.Utilisateur.Nom + " " + v.Patient.Utilisateur.Prenom
                : "Unknown",
            MedecinName = v.Medecin != null && v.Medecin.Utilisateur != null
                ? v.Medecin.Utilisateur.Nom + " " + v.Medecin.Utilisateur.Prenom
                : "Unknown",
            Montant = v.montant,
            Date = v.Date,
            Notes = v.Notes,
            Medicaments = v.VisiteMedicaments.Select(vm => new MedicamentDto
            {
                Nom = vm.Medicament.Nom,
                Instructions = vm.Medicament.Instructions
            }).ToList(),
            MontantEnLettre = NumberToWords(v.montant) // Convert montant to words in French
        }).ToList();

        return Ok(visiteDtos);
    }

    // Helper method to convert a number to French words
    private string NumberToWords(int number)
    {
        return number.ToWords(new CultureInfo("fr")); // Use French culture for word conversion
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
        catch
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

// DTOs

public class VisiteDto
{
    public int Id_Visite { get; set; }
    public int Id_Medecin { get; set; }
    public int Id_patient { get; set; }
    public int Montant { get; set; }
    public string Date { get; set; }
    public string Notes { get; set; }
    public List<MedicamentDto> Medicaments { get; set; }
}

public class VisiteDtoo
{
    public int Id_Visite { get; set; }
    public int Id_Medecin { get; set; }
    public int Id_patient { get; set; }
    public int Montant { get; set; }
    public string Date { get; set; }
    public string Notes { get; set; }
    public string MedecinName { get; set; }
    public string PatientName { get; set; }
    public List<MedicamentDto> Medicaments { get; set; }
    public string MontantEnLettre { get; set; }  // Montant in words
}

public class MedicamentDto
{
    public string Nom { get; set; }
    public string Instructions { get; set; }
}
