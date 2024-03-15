using System;
using System.Collections.Generic;
using GestionUsuarios.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionUsuarios.INFRASTRUCTURE.Context;

public partial class BdContext : DbContext
{
    public BdContext()
    {
    }

    public BdContext(DbContextOptions<BdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Identificador).HasName("PK__Personas__F2374EB18E96A6C2");

            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombresApellidosConcatenados)
                .HasMaxLength(101)
                .IsUnicode(false)
                .HasComputedColumnSql("(concat([Nombres],' ',[Apellidos]))", false);
            entity.Property(e => e.NumeroIdentificacion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NumeroIdentificacionConcatenado)
                .HasMaxLength(41)
                .IsUnicode(false)
                .HasComputedColumnSql("(concat([TipoIdentificacion],'-',[NumeroIdentificacion]))", false);
            entity.Property(e => e.TipoIdentificacion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoDocu__3214EC27562ECE44");

            entity.ToTable("TipoDocumento");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Identificador).HasName("PK__Usuario__F2374EB1C61F89A0");

            entity.ToTable("Usuario");

            entity.Property(e => e.Identificador).ValueGeneratedNever();
            entity.Property(e => e.Pass)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
