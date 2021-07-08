﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersistenceData;

namespace PersistenceData.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210708153742_Inicio")]
    partial class Inicio
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Cita", b =>
                {
                    b.Property<Guid>("CitaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExpedienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FechaCita")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ServicioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CitaId");

                    b.HasIndex("ExpedienteId");

                    b.HasIndex("ServicioId");

                    b.ToTable("Citas");
                });

            modelBuilder.Entity("Models.Diagnostico", b =>
                {
                    b.Property<Guid>("DiagnosticoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EnfermedadId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExpedienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.HasKey("DiagnosticoId");

                    b.HasIndex("EnfermedadId");

                    b.HasIndex("ExpedienteId")
                        .IsUnique();

                    b.ToTable("Diagnosticos");
                });

            modelBuilder.Entity("Models.Documento", b =>
                {
                    b.Property<Guid>("DocumentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("ContenidoDocumento")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ExtensionDocumento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreDocumento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ObjetoReferencia")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DocumentoId");

                    b.ToTable("Documentos");
                });

            modelBuilder.Entity("Models.Enfermedad", b =>
                {
                    b.Property<Guid>("EnfermedadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DescripcionEnfermedad")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombreEnfermedad")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("EnfermedadId");

                    b.ToTable("Enfermedades");
                });

            modelBuilder.Entity("Models.Expediente", b =>
                {
                    b.Property<Guid>("ExpedienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodidoExpediente")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<Guid>("DiagnosticoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PacienteId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ExpedienteId");

                    b.HasIndex("PacienteId");

                    b.ToTable("Expedientes");
                });

            modelBuilder.Entity("Models.Paciente", b =>
                {
                    b.Property<Guid>("PacienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApellidoPaciente")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("EdadPaciente")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoDuiPaciente")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("NombrePaciente")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PacienteTieneExpediente")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("PacienteId");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("Models.Pagina", b =>
                {
                    b.Property<Guid>("PaginaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Accion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Controlador")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NombrePagina")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PaginaId");

                    b.ToTable("Paginas");
                });

            modelBuilder.Entity("Models.PaginaTipoUsuario", b =>
                {
                    b.Property<Guid>("PaginaTipoUsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PaginaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TipoUsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PaginaTipoUsuarioId");

                    b.HasIndex("PaginaId");

                    b.HasIndex("TipoUsuarioId");

                    b.ToTable("PaginaTipoUsuarios");
                });

            modelBuilder.Entity("Models.Servicio", b =>
                {
                    b.Property<Guid>("ServicioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DescripcionServicio")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombreServicio")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ServicioId");

                    b.ToTable("Servicios");
                });

            modelBuilder.Entity("Models.TipoUsuario", b =>
                {
                    b.Property<Guid>("TipoUsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DescripcionTipoUsuario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombreTipoUsuario")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipoUsuarioId");

                    b.ToTable("TipoUsuarios");
                });

            modelBuilder.Entity("Models.Usuario", b =>
                {
                    b.Property<Guid>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Contra")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Direccion")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("TipoUsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UsuarioId");

                    b.HasIndex("TipoUsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Models.Cita", b =>
                {
                    b.HasOne("Models.Expediente", "Expediente")
                        .WithMany()
                        .HasForeignKey("ExpedienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Servicio", "Servicio")
                        .WithMany()
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expediente");

                    b.Navigation("Servicio");
                });

            modelBuilder.Entity("Models.Diagnostico", b =>
                {
                    b.HasOne("Models.Enfermedad", "Enfermedad")
                        .WithMany()
                        .HasForeignKey("EnfermedadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Expediente", "Expediente")
                        .WithOne("Diagnostico")
                        .HasForeignKey("Models.Diagnostico", "ExpedienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enfermedad");

                    b.Navigation("Expediente");
                });

            modelBuilder.Entity("Models.Expediente", b =>
                {
                    b.HasOne("Models.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Models.PaginaTipoUsuario", b =>
                {
                    b.HasOne("Models.Pagina", "Pagina")
                        .WithMany()
                        .HasForeignKey("PaginaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.TipoUsuario", "TipoUsuario")
                        .WithMany()
                        .HasForeignKey("TipoUsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pagina");

                    b.Navigation("TipoUsuario");
                });

            modelBuilder.Entity("Models.Usuario", b =>
                {
                    b.HasOne("Models.TipoUsuario", "TipoUsuario")
                        .WithMany()
                        .HasForeignKey("TipoUsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoUsuario");
                });

            modelBuilder.Entity("Models.Expediente", b =>
                {
                    b.Navigation("Diagnostico");
                });
#pragma warning restore 612, 618
        }
    }
}