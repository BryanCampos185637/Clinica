IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210507140406_Initialize')
BEGIN
    CREATE TABLE [Enfermedades] (
        [EnfermedadId] bigint NOT NULL IDENTITY,
        [NombreEnfermedad] nvarchar(200) NOT NULL,
        [DescripcionEnfermedad] nvarchar(200) NOT NULL,
        [FechaCreacion] datetime2 NOT NULL,
        CONSTRAINT [PK_Enfermedades] PRIMARY KEY ([EnfermedadId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210507140406_Initialize')
BEGIN
    CREATE TABLE [Pacientes] (
        [PacienteId] uniqueidentifier NOT NULL,
        [NombrePaciente] nvarchar(200) NOT NULL,
        [ApellidoPaciente] nvarchar(200) NOT NULL,
        [EdadPaciente] int NOT NULL,
        [NoDuiPaciente] nvarchar(10) NOT NULL,
        [FechaCreacion] datetime2 NOT NULL,
        CONSTRAINT [PK_Pacientes] PRIMARY KEY ([PacienteId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210507140406_Initialize')
BEGIN
    CREATE TABLE [Paginas] (
        [PaginaId] int NOT NULL IDENTITY,
        [NombrePagina] nvarchar(50) NOT NULL,
        [Accion] nvarchar(50) NOT NULL,
        [Controlador] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Paginas] PRIMARY KEY ([PaginaId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210507140406_Initialize')
BEGIN
    CREATE TABLE [Servicios] (
        [ServicioId] int NOT NULL IDENTITY,
        [NombreServicio] nvarchar(200) NOT NULL,
        [DescripcionServicio] nvarchar(200) NOT NULL,
        [FechaCreacion] datetime2 NOT NULL,
        CONSTRAINT [PK_Servicios] PRIMARY KEY ([ServicioId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210507140406_Initialize')
BEGIN
    CREATE TABLE [TipoUsuarios] (
        [TipoUsuarioId] int NOT NULL IDENTITY,
        [NombreTipoUsuario] nvarchar(max) NULL,
        [DescripcionTipoUsuario] nvarchar(max) NULL,
        [FechaCreacion] datetime2 NOT NULL,
        CONSTRAINT [PK_TipoUsuarios] PRIMARY KEY ([TipoUsuarioId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210507140406_Initialize')
BEGIN
    CREATE TABLE [Citas] (
        [CitaId] uniqueidentifier NOT NULL,
        [PacienteId] uniqueidentifier NOT NULL,
        [ServicioId] int NOT NULL,
        [FechaCita] datetime2 NOT NULL,
        [FechaCreacion] datetime2 NOT NULL,
        CONSTRAINT [PK_Citas] PRIMARY KEY ([CitaId]),
        CONSTRAINT [FK_Citas_Pacientes_PacienteId] FOREIGN KEY ([PacienteId]) REFERENCES [Pacientes] ([PacienteId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Citas_Servicios_ServicioId] FOREIGN KEY ([ServicioId]) REFERENCES [Servicios] ([ServicioId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210507140406_Initialize')
BEGIN
    CREATE TABLE [paginaTipoUsuarios] (
        [PaginaTipoUsuarioId] int NOT NULL IDENTITY,
        [PaginaId] int NOT NULL,
        [TipoUsuarioId] int NOT NULL,
        CONSTRAINT [PK_paginaTipoUsuarios] PRIMARY KEY ([PaginaTipoUsuarioId]),
        CONSTRAINT [FK_paginaTipoUsuarios_Paginas_PaginaId] FOREIGN KEY ([PaginaId]) REFERENCES [Paginas] ([PaginaId]) ON DELETE CASCADE,
        CONSTRAINT [FK_paginaTipoUsuarios_TipoUsuarios_TipoUsuarioId] FOREIGN KEY ([TipoUsuarioId]) REFERENCES [TipoUsuarios] ([TipoUsuarioId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210507140406_Initialize')
BEGIN
    CREATE TABLE [Usuarios] (
        [UsuarioId] int NOT NULL IDENTITY,
        [NombreUsuario] nvarchar(50) NOT NULL,
        [Contra] nvarchar(50) NOT NULL,
        [TipoUsuarioId] int NOT NULL,
        [FechaCreacion] datetime2 NOT NULL,
        CONSTRAINT [PK_Usuarios] PRIMARY KEY ([UsuarioId]),
        CONSTRAINT [FK_Usuarios_TipoUsuarios_TipoUsuarioId] FOREIGN KEY ([TipoUsuarioId]) REFERENCES [TipoUsuarios] ([TipoUsuarioId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210507140406_Initialize')
BEGIN
    CREATE INDEX [IX_Citas_PacienteId] ON [Citas] ([PacienteId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210507140406_Initialize')
BEGIN
    CREATE INDEX [IX_Citas_ServicioId] ON [Citas] ([ServicioId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210507140406_Initialize')
BEGIN
    CREATE INDEX [IX_paginaTipoUsuarios_PaginaId] ON [paginaTipoUsuarios] ([PaginaId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210507140406_Initialize')
BEGIN
    CREATE INDEX [IX_paginaTipoUsuarios_TipoUsuarioId] ON [paginaTipoUsuarios] ([TipoUsuarioId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210507140406_Initialize')
BEGIN
    CREATE INDEX [IX_Usuarios_TipoUsuarioId] ON [Usuarios] ([TipoUsuarioId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210507140406_Initialize')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210507140406_Initialize', N'5.0.5');
END;
GO

COMMIT;
GO

