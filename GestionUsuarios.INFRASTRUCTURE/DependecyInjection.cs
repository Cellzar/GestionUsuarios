﻿using GestionUsuarios.APPLICATION.Common.Interfaces;
using GestionUsuarios.APPLICATION.Services;
using GestionUsuarios.DOMAIN.Entities;
using GestionUsuarios.INFRASTRUCTURE.Context;
using GestionUsuarios.INFRASTRUCTURE.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;


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

        services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        return services;
    }
}
