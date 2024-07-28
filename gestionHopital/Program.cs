using gestionHopital.Data;
using gestionHopital.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("MyConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("La chaine de connexion est vide ou nulle veuillez verifier la configuration de votre base de donnees");
}

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<Utilisateur, IdentityRole<int>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", options =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Secret"]);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173") 
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Use CORS policy
app.UseCors("AllowReactApp");

builder.Services.AddAuthorization();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
