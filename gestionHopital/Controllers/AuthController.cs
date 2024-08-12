using gestionHopital.Data;
using gestionHopital.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace gestionHopital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Utilisateur> _userManager;
        private readonly SignInManager<Utilisateur> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthController(UserManager<Utilisateur> userManager, SignInManager<Utilisateur> signInManager, IConfiguration configuration, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new Utilisateur
            {
                UserName = model.Email,
                Email = model.Email,
                Nom = model.Nom,
                Prenom = model.Prenom,
                Cin = model.Cin,
                Telephone = model.Telephone,
                DateNaissance = DateOnly.Parse(model.DateNaissance)
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            switch (model.Type.ToLower())
            {
                case "admin":
                    var admin = new Admin
                    {
                        UtilisateurID = user.Id,
                        Utilisateur = user
                    };
                    _context.Admins.Add(admin);
                    break;
                case "medecin":
                    var medecin = new Medecin
                    {
                        UtilisateurID = user.Id,
                        Utilisateur = user,
                        Specialisation = model.Specialisation,
                        DepartementID = model.DepartementID.Value // Assuming DepartementID is required for medecin
                    };
                    _context.Medecins.Add(medecin);
                    break;
                case "secretary":
                    var secretary = new Secretary
                    {
                        UtilisateurID = user.Id,
                        Utilisateur = user,
                        Superieurid_medecin = model.Superieurid_medecin
                    };
                    _context.Secretaries.Add(secretary);
                    break;
                case "patient":
                    var patient = new Patient
                    {
                        UtilisateurID = user.Id,
                        Utilisateur = user,
                        Genre = Enum.Parse<Genre>(model.Genre),
                        Adresse = model.Adresse,
                        Historiquemedical = model.Historiquemedical
                    };
                    _context.Patients.Add(patient);
                    break;
                default:
                    return BadRequest("Invalid user type.");
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded)
                return BadRequest("Invalid login attempt.");

            var user = await _userManager.FindByEmailAsync(model.Email);
            var token = GenerateJwtToken(user);

            string userType = await GetUserType(user.Id);

            return Ok(new { token, type = userType });
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] TokenModel model)
        {
            if (string.IsNullOrEmpty(model.Token))
                return BadRequest("Token is required");

            // Logic to invalidate the token can be implemented here
            // Since JWTs are stateless, this could be a placeholder for actual token invalidation logic

            return Ok(new { message = "Logged out successfully" });
        }

        private async Task<string> GetUserType(int userId)
        {
            if (await _context.Admins.AnyAsync(a => a.UtilisateurID == userId))
                return "admin";
            if (await _context.Medecins.AnyAsync(m => m.UtilisateurID == userId))
                return "medecin";
            if (await _context.Secretaries.AnyAsync(s => s.UtilisateurID == userId))
                return "secretary";
            if (await _context.Patients.AnyAsync(p => p.UtilisateurID == userId))
                return "patient";

            return "unknown";
        }

        private string GenerateJwtToken(Utilisateur user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = creds,
                Issuer = _configuration["JwtConfig:Issuer"],
                Audience = _configuration["JwtConfig:Audience"] // Ensure audience is included here
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


    }

    public class RegisterModel
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        public string Cin { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? Telephone { get; set; }
        public string? DateNaissance { get; set; } // Date in string format
        public string? Specialisation { get; set; }
        public int? Superieurid_medecin { get; set; }
        public string? Genre { get; set; }
        public string? Adresse { get; set; }
        public string? Historiquemedical { get; set; }

        public int? DepartementID { get; set; } // Added this line for DepartementID
    }

    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class TokenModel
    {
        [Required]
        public string Token { get; set; }
    }
}