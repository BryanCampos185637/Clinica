using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersistenceData.Migrations
{
    public partial class inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    DocumentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtensionDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContenidoDocumento = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    NombreDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObjetoReferencia = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.DocumentoId);
                });

            migrationBuilder.CreateTable(
                name: "Enfermedades",
                columns: table => new
                {
                    EnfermedadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreEnfermedad = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescripcionEnfermedad = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enfermedades", x => x.EnfermedadId);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombrePaciente = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ApellidoPaciente = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EdadPaciente = table.Column<int>(type: "int", nullable: false),
                    NoDuiPaciente = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PacienteTieneExpediente = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.PacienteId);
                });

            migrationBuilder.CreateTable(
                name: "Paginas",
                columns: table => new
                {
                    PaginaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombrePagina = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Controlador = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paginas", x => x.PaginaId);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    ServicioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreServicio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescripcionServicio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.ServicioId);
                });

            migrationBuilder.CreateTable(
                name: "TipoUsuarios",
                columns: table => new
                {
                    TipoUsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreTipoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescripcionTipoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuarios", x => x.TipoUsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Expedientes",
                columns: table => new
                {
                    ExpedienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodidoExpediente = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiagnosticoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expedientes", x => x.ExpedienteId);
                    table.ForeignKey(
                        name: "FK_Expedientes_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaginaTipoUsuarios",
                columns: table => new
                {
                    PaginaTipoUsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaginaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoUsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaginaTipoUsuarios", x => x.PaginaTipoUsuarioId);
                    table.ForeignKey(
                        name: "FK_PaginaTipoUsuarios_Paginas_PaginaId",
                        column: x => x.PaginaId,
                        principalTable: "Paginas",
                        principalColumn: "PaginaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaginaTipoUsuarios_TipoUsuarios_TipoUsuarioId",
                        column: x => x.TipoUsuarioId,
                        principalTable: "TipoUsuarios",
                        principalColumn: "TipoUsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NombreUsuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contra = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoUsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuarios_TipoUsuarios_TipoUsuarioId",
                        column: x => x.TipoUsuarioId,
                        principalTable: "TipoUsuarios",
                        principalColumn: "TipoUsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    CitaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpedienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.CitaId);
                    table.ForeignKey(
                        name: "FK_Citas_Expedientes_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalTable: "Expedientes",
                        principalColumn: "ExpedienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Citas_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "ServicioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnosticos",
                columns: table => new
                {
                    DiagnosticoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnfermedadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpedienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosticos", x => x.DiagnosticoId);
                    table.ForeignKey(
                        name: "FK_Diagnosticos_Enfermedades_EnfermedadId",
                        column: x => x.EnfermedadId,
                        principalTable: "Enfermedades",
                        principalColumn: "EnfermedadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diagnosticos_Expedientes_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalTable: "Expedientes",
                        principalColumn: "ExpedienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citas_ExpedienteId",
                table: "Citas",
                column: "ExpedienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_ServicioId",
                table: "Citas",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosticos_EnfermedadId",
                table: "Diagnosticos",
                column: "EnfermedadId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosticos_ExpedienteId",
                table: "Diagnosticos",
                column: "ExpedienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_PacienteId",
                table: "Expedientes",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PaginaTipoUsuarios_PaginaId",
                table: "PaginaTipoUsuarios",
                column: "PaginaId");

            migrationBuilder.CreateIndex(
                name: "IX_PaginaTipoUsuarios_TipoUsuarioId",
                table: "PaginaTipoUsuarios",
                column: "TipoUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TipoUsuarioId",
                table: "Usuarios",
                column: "TipoUsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Diagnosticos");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "PaginaTipoUsuarios");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Enfermedades");

            migrationBuilder.DropTable(
                name: "Expedientes");

            migrationBuilder.DropTable(
                name: "Paginas");

            migrationBuilder.DropTable(
                name: "TipoUsuarios");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
