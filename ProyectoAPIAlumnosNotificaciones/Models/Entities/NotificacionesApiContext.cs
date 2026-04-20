using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ProyectoAPIAlumnosNotificaciones.Models.Entities;

public partial class NotificacionesApiContext : DbContext
{
    public NotificacionesApiContext()
    {
    }

    public NotificacionesApiContext(DbContextOptions<NotificacionesApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumnos> Alumnos { get; set; }

    public virtual DbSet<Avisosgenerales> Avisosgenerales { get; set; }

    public virtual DbSet<Avisospersonales> Avisospersonales { get; set; }

    public virtual DbSet<Grupos> Grupos { get; set; }

    public virtual DbSet<LecturasAvisos> LecturasAvisos { get; set; }

    public virtual DbSet<Maestros> Maestros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("user=root;password=root;server=localhost;database=NotificacionesApi;port=3308", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.37-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Alumnos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("alumnos");

            entity.HasIndex(e => e.IdGrupo, "idx_alumno_grupo");

            entity.Property(e => e.Contraseña).HasMaxLength(255);
            entity.Property(e => e.IdGrupo).HasColumnName("id_grupo");
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.Alumnos)
                .HasForeignKey(d => d.IdGrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("alumnos_ibfk_1");
        });

        modelBuilder.Entity<Avisosgenerales>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("avisosgenerales");

            entity.HasIndex(e => e.FechaCaducidad, "idx_avisos_escuela_fecha");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contenido)
                .HasColumnType("text")
                .HasColumnName("contenido");
            entity.Property(e => e.FechaCaducidad)
                .HasColumnType("datetime")
                .HasColumnName("fecha_caducidad");
            entity.Property(e => e.FechaEmision)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_emision");
            entity.Property(e => e.Titulo)
                .HasMaxLength(150)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<Avisospersonales>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("avisospersonales");

            entity.HasIndex(e => e.IdMaestro, "id_maestro");

            entity.HasIndex(e => new { e.IdAlumno, e.FechaEnvio }, "idx_avisos_alumno").IsDescending(false, true);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contenido)
                .HasColumnType("text")
                .HasColumnName("contenido");
            entity.Property(e => e.FechaEnvio)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_envio");
            entity.Property(e => e.IdAlumno)
                .HasComment("Quien recibe")
                .HasColumnName("id_alumno");
            entity.Property(e => e.IdMaestro)
                .HasComment("Quien envia")
                .HasColumnName("id_maestro");
            entity.Property(e => e.Titulo)
                .HasMaxLength(150)
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.Avisospersonales)
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("avisospersonales_ibfk_2");

            entity.HasOne(d => d.IdMaestroNavigation).WithMany(p => p.Avisospersonales)
                .HasForeignKey(d => d.IdMaestro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("avisospersonales_ibfk_1");
        });

        modelBuilder.Entity<Grupos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("grupos");

            entity.HasIndex(e => e.IdMaestro, "id_maestro").IsUnique();

            entity.Property(e => e.IdMaestro).HasColumnName("id_maestro");
            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasOne(d => d.IdMaestroNavigation).WithOne(p => p.Grupos)
                .HasForeignKey<Grupos>(d => d.IdMaestro)
                .HasConstraintName("grupos_ibfk_1");
        });

        modelBuilder.Entity<LecturasAvisos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("lecturas_avisos");

            entity.HasIndex(e => e.IdAlumno, "id_alumno");

            entity.HasIndex(e => new { e.IdAvisoPersonal, e.IdAlumno }, "uk_lectura_unica").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaLectura)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_lectura");
            entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");
            entity.Property(e => e.IdAvisoPersonal).HasColumnName("id_aviso_personal");

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.LecturasAvisos)
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("lecturas_avisos_ibfk_2");

            entity.HasOne(d => d.IdAvisoPersonalNavigation).WithMany(p => p.LecturasAvisos)
                .HasForeignKey(d => d.IdAvisoPersonal)
                .HasConstraintName("lecturas_avisos_ibfk_1");
        });

        modelBuilder.Entity<Maestros>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("maestros");

            entity.Property(e => e.Clave).HasMaxLength(50);
            entity.Property(e => e.Contraseña).HasMaxLength(255);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
