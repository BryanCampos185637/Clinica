USE [DBClinica]
GO
SET IDENTITY_INSERT [dbo].[Paginas] ON 

INSERT [dbo].[Paginas] ([PaginaId], [NombrePagina], [Accion], [Controlador]) VALUES (1, N'SERVICIO', N'INDEX', N'SERVICIO')
INSERT [dbo].[Paginas] ([PaginaId], [NombrePagina], [Accion], [Controlador]) VALUES (2, N'ENFERMEDAD', N'INDEX', N'ENFERMEDAD')
INSERT [dbo].[Paginas] ([PaginaId], [NombrePagina], [Accion], [Controlador]) VALUES (3, N'PACIENTE', N'INDEX', N'PACIENTE')
INSERT [dbo].[Paginas] ([PaginaId], [NombrePagina], [Accion], [Controlador]) VALUES (4, N'EXPEDIENTE', N'INDEX', N'EXPEDIENTE')
INSERT [dbo].[Paginas] ([PaginaId], [NombrePagina], [Accion], [Controlador]) VALUES (5, N'CREAR CITA', N'CREARCITA', N'CITA')
INSERT [dbo].[Paginas] ([PaginaId], [NombrePagina], [Accion], [Controlador]) VALUES (6, N'CITAS CREADAS', N'INDEX ', N'CITA')
INSERT [dbo].[Paginas] ([PaginaId], [NombrePagina], [Accion], [Controlador]) VALUES (7, N'PAGINAS', N'INDEX', N'PAGINA')
INSERT [dbo].[Paginas] ([PaginaId], [NombrePagina], [Accion], [Controlador]) VALUES (8, N'ROLES', N'INDEX', N'ROL')
INSERT [dbo].[Paginas] ([PaginaId], [NombrePagina], [Accion], [Controlador]) VALUES (9, N'USUARIO', N'INDEX', N'USUARIO')
SET IDENTITY_INSERT [dbo].[Paginas] OFF
SET IDENTITY_INSERT [dbo].[TipoUsuarios] ON 

INSERT [dbo].[TipoUsuarios] ([TipoUsuarioId], [NombreTipoUsuario], [DescripcionTipoUsuario], [FechaCreacion]) VALUES (1, N'ADMINISTRADOR', N'TIENE TODOS LOS PERMISOS', CAST(N'2021-06-03T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[TipoUsuarios] ([TipoUsuarioId], [NombreTipoUsuario], [DescripcionTipoUsuario], [FechaCreacion]) VALUES (6, N'DOCTOR', N'TIENE ACCESO A EXPEDIENTE PACIENTE Y CREAR CITAS', CAST(N'2021-06-04T13:04:23.4895057' AS DateTime2))
SET IDENTITY_INSERT [dbo].[TipoUsuarios] OFF
SET IDENTITY_INSERT [dbo].[PaginaTipoUsuarios] ON 

INSERT [dbo].[PaginaTipoUsuarios] ([PaginaTipoUsuarioId], [PaginaId], [TipoUsuarioId]) VALUES (133, 7, 6)
INSERT [dbo].[PaginaTipoUsuarios] ([PaginaTipoUsuarioId], [PaginaId], [TipoUsuarioId]) VALUES (134, 8, 6)
INSERT [dbo].[PaginaTipoUsuarios] ([PaginaTipoUsuarioId], [PaginaId], [TipoUsuarioId]) VALUES (143, 1, 1)
INSERT [dbo].[PaginaTipoUsuarios] ([PaginaTipoUsuarioId], [PaginaId], [TipoUsuarioId]) VALUES (144, 2, 1)
INSERT [dbo].[PaginaTipoUsuarios] ([PaginaTipoUsuarioId], [PaginaId], [TipoUsuarioId]) VALUES (145, 3, 1)
INSERT [dbo].[PaginaTipoUsuarios] ([PaginaTipoUsuarioId], [PaginaId], [TipoUsuarioId]) VALUES (146, 4, 1)
INSERT [dbo].[PaginaTipoUsuarios] ([PaginaTipoUsuarioId], [PaginaId], [TipoUsuarioId]) VALUES (147, 5, 1)
INSERT [dbo].[PaginaTipoUsuarios] ([PaginaTipoUsuarioId], [PaginaId], [TipoUsuarioId]) VALUES (148, 6, 1)
INSERT [dbo].[PaginaTipoUsuarios] ([PaginaTipoUsuarioId], [PaginaId], [TipoUsuarioId]) VALUES (149, 7, 1)
INSERT [dbo].[PaginaTipoUsuarios] ([PaginaTipoUsuarioId], [PaginaId], [TipoUsuarioId]) VALUES (150, 8, 1)
INSERT [dbo].[PaginaTipoUsuarios] ([PaginaTipoUsuarioId], [PaginaId], [TipoUsuarioId]) VALUES (151, 9, 1)
SET IDENTITY_INSERT [dbo].[PaginaTipoUsuarios] OFF
