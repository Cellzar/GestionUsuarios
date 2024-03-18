using GestionUsuarios.APPLICATION.Common.Interfaces;
using GestionUsuarios.APPLICATION.Services;
using GestionUsuarios.DOMAIN.Entities;
using GestionUsuarios.INFRASTRUCTURE.Context;
using GestionUsuarios.INFRASTRUCTURE.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using GestionUsuarios.DOMAIN.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace GestionUsuarios.INFRASTRUCTURE;

public static class DependecyInjection
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration Configuration)
    {
        string connectionString = Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<BdContext>(options => options.UseSqlServer(connectionString));

    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        List<string> listOrigin = new List<string>();
        var originsSection = configuration.GetSection("WithOrigins");
        foreach (var origin in originsSection.GetChildren())
        {
            listOrigin.Add(origin.Value);
        }

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(listOrigin.ToArray()));
        });

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped<PersonaService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IPersonaService, PersonaService>();
        return services;
    }

    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuration from AppSettings
        services.Configure<JWT>(configuration.GetSection("JWT"));

        // Adding Authentication - JWT
        services.AddAuthentication("Bearer")
        .AddJwtBearer(o =>
        {

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var signingCredencials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);
            o.RequireHttpsMetadata = false;

            
            o.RequireHttpsMetadata = false;
            o.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = signingKey
            };
        });
    }
}
