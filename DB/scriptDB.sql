
--PRIMERO SE DEBE CREAR LA BASE DE DATOS
CREATE DATABASE oriontek

--LUEGO DE HABER CREADO LA BASE DE DATOS SE EJECUTA TODO LO SIGUIENTE

USE oriontek
GO
/****** Object:  Table [dbo].[clientes]    Script Date: 3/21/2021 7:52:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clientes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](65) NOT NULL,
	[apellido] [varchar](65) NOT NULL,
	[telefono] [varchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[direcciones]    Script Date: 3/21/2021 7:52:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[direcciones](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_cliente] [int] NULL,
	[id_pais] [int] NULL,
	[line1] [text] NULL,
	[line2] [text] NULL,
	[sector] [varchar](135) NULL,
	[ciudad] [varchar](95) NULL,
	[zipcode] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[logs]    Script Date: 3/21/2021 7:52:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[logs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[id_objeto] [int] NULL,
	[tipo] [varchar](50) NULL,
	[fecha] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[paises]    Script Date: 3/21/2021 7:52:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[paises](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pais] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[paises] ON 

INSERT [dbo].[paises] ([id], [pais]) VALUES (1, N'Austria')
INSERT [dbo].[paises] ([id], [pais]) VALUES (2, N'Bélgica')
INSERT [dbo].[paises] ([id], [pais]) VALUES (3, N'Bulgaria')
INSERT [dbo].[paises] ([id], [pais]) VALUES (4, N'Chipre')
INSERT [dbo].[paises] ([id], [pais]) VALUES (5, N'Dinamarca')
INSERT [dbo].[paises] ([id], [pais]) VALUES (6, N'España')
INSERT [dbo].[paises] ([id], [pais]) VALUES (7, N'Finlandia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (8, N'Francia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (9, N'Grecia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (10, N'Hungría')
INSERT [dbo].[paises] ([id], [pais]) VALUES (11, N'Irlanda')
INSERT [dbo].[paises] ([id], [pais]) VALUES (12, N'Italia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (13, N'Luxemburgo')
INSERT [dbo].[paises] ([id], [pais]) VALUES (14, N'Malta')
INSERT [dbo].[paises] ([id], [pais]) VALUES (15, N'Países Bajos')
INSERT [dbo].[paises] ([id], [pais]) VALUES (16, N'Polonia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (17, N'Portugal')
INSERT [dbo].[paises] ([id], [pais]) VALUES (18, N'Alemania')
INSERT [dbo].[paises] ([id], [pais]) VALUES (19, N'Rumanía')
INSERT [dbo].[paises] ([id], [pais]) VALUES (20, N'Suecia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (21, N'Letonia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (22, N'Estonia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (23, N'Lituania')
INSERT [dbo].[paises] ([id], [pais]) VALUES (24, N'República Checa')
INSERT [dbo].[paises] ([id], [pais]) VALUES (25, N'República Eslovaca')
INSERT [dbo].[paises] ([id], [pais]) VALUES (26, N'Croacia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (27, N'Eslovenia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (28, N'Albania')
INSERT [dbo].[paises] ([id], [pais]) VALUES (29, N'Islandia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (30, N'Liechtenstein')
INSERT [dbo].[paises] ([id], [pais]) VALUES (31, N'Mónaco')
INSERT [dbo].[paises] ([id], [pais]) VALUES (32, N'Noruega')
INSERT [dbo].[paises] ([id], [pais]) VALUES (33, N'Andorra')
INSERT [dbo].[paises] ([id], [pais]) VALUES (34, N'Reino Unido')
INSERT [dbo].[paises] ([id], [pais]) VALUES (35, N'San Marino')
INSERT [dbo].[paises] ([id], [pais]) VALUES (36, N'Santa Sede')
INSERT [dbo].[paises] ([id], [pais]) VALUES (37, N'Suiza')
INSERT [dbo].[paises] ([id], [pais]) VALUES (38, N'Ucrania')
INSERT [dbo].[paises] ([id], [pais]) VALUES (39, N'Moldavia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (40, N'Belarús')
INSERT [dbo].[paises] ([id], [pais]) VALUES (41, N'Georgia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (42, N'Bosnia y Herzegovina')
INSERT [dbo].[paises] ([id], [pais]) VALUES (43, N'Armenia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (44, N'Rusia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (45, N'Macedonia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (46, N'Serbia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (47, N'Montenegro')
INSERT [dbo].[paises] ([id], [pais]) VALUES (48, N'Otros países de Europa')
INSERT [dbo].[paises] ([id], [pais]) VALUES (49, N'Burkina Faso')
INSERT [dbo].[paises] ([id], [pais]) VALUES (50, N'Angola')
INSERT [dbo].[paises] ([id], [pais]) VALUES (51, N'Argelia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (52, N'Benin')
INSERT [dbo].[paises] ([id], [pais]) VALUES (53, N'Botswana')
INSERT [dbo].[paises] ([id], [pais]) VALUES (54, N'Burundi')
INSERT [dbo].[paises] ([id], [pais]) VALUES (55, N'Cabo Verde')
INSERT [dbo].[paises] ([id], [pais]) VALUES (56, N'Camerún')
INSERT [dbo].[paises] ([id], [pais]) VALUES (57, N'Comores')
INSERT [dbo].[paises] ([id], [pais]) VALUES (58, N'Congo')
INSERT [dbo].[paises] ([id], [pais]) VALUES (59, N'Costa de Marfil')
INSERT [dbo].[paises] ([id], [pais]) VALUES (60, N'Djibouti')
INSERT [dbo].[paises] ([id], [pais]) VALUES (61, N'Egipto')
INSERT [dbo].[paises] ([id], [pais]) VALUES (62, N'Etiopía')
INSERT [dbo].[paises] ([id], [pais]) VALUES (63, N'Gabón')
INSERT [dbo].[paises] ([id], [pais]) VALUES (64, N'Gambia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (65, N'Ghana')
INSERT [dbo].[paises] ([id], [pais]) VALUES (66, N'Guinea')
INSERT [dbo].[paises] ([id], [pais]) VALUES (67, N'Guinea-Bissau')
INSERT [dbo].[paises] ([id], [pais]) VALUES (68, N'Guinea Ecuatorial')
INSERT [dbo].[paises] ([id], [pais]) VALUES (69, N'Kenia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (70, N'Lesotho')
INSERT [dbo].[paises] ([id], [pais]) VALUES (71, N'Liberia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (72, N'Libia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (73, N'Madagascar')
INSERT [dbo].[paises] ([id], [pais]) VALUES (74, N'Malawi')
INSERT [dbo].[paises] ([id], [pais]) VALUES (75, N'Mali')
INSERT [dbo].[paises] ([id], [pais]) VALUES (76, N'Marruecos')
INSERT [dbo].[paises] ([id], [pais]) VALUES (77, N'Mauricio')
INSERT [dbo].[paises] ([id], [pais]) VALUES (78, N'Mauritania')
INSERT [dbo].[paises] ([id], [pais]) VALUES (79, N'Mozambique')
INSERT [dbo].[paises] ([id], [pais]) VALUES (80, N'Namibia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (81, N'Níger')
INSERT [dbo].[paises] ([id], [pais]) VALUES (82, N'Nigeria')
INSERT [dbo].[paises] ([id], [pais]) VALUES (83, N'República Centroafricana')
INSERT [dbo].[paises] ([id], [pais]) VALUES (84, N'Sudáfrica')
INSERT [dbo].[paises] ([id], [pais]) VALUES (85, N'Ruanda')
INSERT [dbo].[paises] ([id], [pais]) VALUES (86, N'Santo Tomé y Príncipe')
INSERT [dbo].[paises] ([id], [pais]) VALUES (87, N'Senegal')
INSERT [dbo].[paises] ([id], [pais]) VALUES (88, N'Seychelles')
INSERT [dbo].[paises] ([id], [pais]) VALUES (89, N'Sierra Leona')
INSERT [dbo].[paises] ([id], [pais]) VALUES (90, N'Somalia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (91, N'Sudán')
INSERT [dbo].[paises] ([id], [pais]) VALUES (92, N'Swazilandia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (93, N'Tanzania')
INSERT [dbo].[paises] ([id], [pais]) VALUES (94, N'Chad')
INSERT [dbo].[paises] ([id], [pais]) VALUES (95, N'Togo')
INSERT [dbo].[paises] ([id], [pais]) VALUES (96, N'Túnez')
INSERT [dbo].[paises] ([id], [pais]) VALUES (97, N'Uganda')
INSERT [dbo].[paises] ([id], [pais]) VALUES (98, N'República Democrática del Congo')
INSERT [dbo].[paises] ([id], [pais]) VALUES (99, N'Zambia')
GO
INSERT [dbo].[paises] ([id], [pais]) VALUES (100, N'Zimbabwe')
INSERT [dbo].[paises] ([id], [pais]) VALUES (101, N'Eritrea')
INSERT [dbo].[paises] ([id], [pais]) VALUES (102, N'Sudán del Sur')
INSERT [dbo].[paises] ([id], [pais]) VALUES (103, N'Otros países de África')
INSERT [dbo].[paises] ([id], [pais]) VALUES (104, N'Canadá')
INSERT [dbo].[paises] ([id], [pais]) VALUES (105, N'Estados Unidos de América')
INSERT [dbo].[paises] ([id], [pais]) VALUES (106, N'México')
INSERT [dbo].[paises] ([id], [pais]) VALUES (107, N'Antigua y Barbuda')
INSERT [dbo].[paises] ([id], [pais]) VALUES (108, N'Bahamas')
INSERT [dbo].[paises] ([id], [pais]) VALUES (109, N'Barbados')
INSERT [dbo].[paises] ([id], [pais]) VALUES (110, N'Belice')
INSERT [dbo].[paises] ([id], [pais]) VALUES (111, N'Costa Rica')
INSERT [dbo].[paises] ([id], [pais]) VALUES (112, N'Cuba')
INSERT [dbo].[paises] ([id], [pais]) VALUES (113, N'Dominica')
INSERT [dbo].[paises] ([id], [pais]) VALUES (114, N'El Salvador')
INSERT [dbo].[paises] ([id], [pais]) VALUES (115, N'Granada')
INSERT [dbo].[paises] ([id], [pais]) VALUES (116, N'Guatemala')
INSERT [dbo].[paises] ([id], [pais]) VALUES (117, N'Haití')
INSERT [dbo].[paises] ([id], [pais]) VALUES (118, N'Honduras')
INSERT [dbo].[paises] ([id], [pais]) VALUES (119, N'Jamaica')
INSERT [dbo].[paises] ([id], [pais]) VALUES (120, N'Nicaragua')
INSERT [dbo].[paises] ([id], [pais]) VALUES (121, N'Panamá')
INSERT [dbo].[paises] ([id], [pais]) VALUES (122, N'San Vicente y las Granadinas')
INSERT [dbo].[paises] ([id], [pais]) VALUES (123, N'República Dominicana')
INSERT [dbo].[paises] ([id], [pais]) VALUES (124, N'Trinidad y Tobago')
INSERT [dbo].[paises] ([id], [pais]) VALUES (125, N'Santa Lucía')
INSERT [dbo].[paises] ([id], [pais]) VALUES (126, N'San Cristóbal y Nieves')
INSERT [dbo].[paises] ([id], [pais]) VALUES (127, N'Argentina')
INSERT [dbo].[paises] ([id], [pais]) VALUES (128, N'Bolivia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (129, N'Brasil')
INSERT [dbo].[paises] ([id], [pais]) VALUES (130, N'Colombia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (131, N'Chile')
INSERT [dbo].[paises] ([id], [pais]) VALUES (132, N'Ecuador')
INSERT [dbo].[paises] ([id], [pais]) VALUES (133, N'Guyana')
INSERT [dbo].[paises] ([id], [pais]) VALUES (134, N'Paraguay')
INSERT [dbo].[paises] ([id], [pais]) VALUES (135, N'Perú')
INSERT [dbo].[paises] ([id], [pais]) VALUES (136, N'Surinam')
INSERT [dbo].[paises] ([id], [pais]) VALUES (137, N'Uruguay')
INSERT [dbo].[paises] ([id], [pais]) VALUES (138, N'Venezuela')
INSERT [dbo].[paises] ([id], [pais]) VALUES (139, N'Otros países de América')
INSERT [dbo].[paises] ([id], [pais]) VALUES (140, N'Afganistán')
INSERT [dbo].[paises] ([id], [pais]) VALUES (141, N'Arabia Saudí')
INSERT [dbo].[paises] ([id], [pais]) VALUES (142, N'Bahréin')
INSERT [dbo].[paises] ([id], [pais]) VALUES (143, N'Bangladesh')
INSERT [dbo].[paises] ([id], [pais]) VALUES (144, N'Myanmar')
INSERT [dbo].[paises] ([id], [pais]) VALUES (145, N'China')
INSERT [dbo].[paises] ([id], [pais]) VALUES (146, N'Emiratos Árabes Unidos')
INSERT [dbo].[paises] ([id], [pais]) VALUES (147, N'Filipinas')
INSERT [dbo].[paises] ([id], [pais]) VALUES (148, N'India')
INSERT [dbo].[paises] ([id], [pais]) VALUES (149, N'Indonesia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (150, N'Iraq')
INSERT [dbo].[paises] ([id], [pais]) VALUES (151, N'Irán')
INSERT [dbo].[paises] ([id], [pais]) VALUES (152, N'Israel')
INSERT [dbo].[paises] ([id], [pais]) VALUES (153, N'Japón')
INSERT [dbo].[paises] ([id], [pais]) VALUES (154, N'Jordania')
INSERT [dbo].[paises] ([id], [pais]) VALUES (155, N'Camboya')
INSERT [dbo].[paises] ([id], [pais]) VALUES (156, N'Kuwait')
INSERT [dbo].[paises] ([id], [pais]) VALUES (157, N'Laos')
INSERT [dbo].[paises] ([id], [pais]) VALUES (158, N'Líbano')
INSERT [dbo].[paises] ([id], [pais]) VALUES (159, N'Malasia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (160, N'Maldivas')
INSERT [dbo].[paises] ([id], [pais]) VALUES (161, N'Mongolia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (162, N'Nepal')
INSERT [dbo].[paises] ([id], [pais]) VALUES (163, N'Omán')
INSERT [dbo].[paises] ([id], [pais]) VALUES (164, N'Pakistán')
INSERT [dbo].[paises] ([id], [pais]) VALUES (165, N'Qatar')
INSERT [dbo].[paises] ([id], [pais]) VALUES (166, N'Corea')
INSERT [dbo].[paises] ([id], [pais]) VALUES (167, N'Corea del Norte')
INSERT [dbo].[paises] ([id], [pais]) VALUES (168, N'Singapur')
INSERT [dbo].[paises] ([id], [pais]) VALUES (169, N'Siria')
INSERT [dbo].[paises] ([id], [pais]) VALUES (170, N'Sri Lanka')
INSERT [dbo].[paises] ([id], [pais]) VALUES (171, N'Tailandia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (172, N'Turquía')
INSERT [dbo].[paises] ([id], [pais]) VALUES (173, N'Vietnam')
INSERT [dbo].[paises] ([id], [pais]) VALUES (174, N'Brunei')
INSERT [dbo].[paises] ([id], [pais]) VALUES (175, N'Islas Marshall')
INSERT [dbo].[paises] ([id], [pais]) VALUES (176, N'Yemen')
INSERT [dbo].[paises] ([id], [pais]) VALUES (177, N'Azerbaiyán')
INSERT [dbo].[paises] ([id], [pais]) VALUES (178, N'Kazajstán')
INSERT [dbo].[paises] ([id], [pais]) VALUES (179, N'Kirguistán')
INSERT [dbo].[paises] ([id], [pais]) VALUES (180, N'Tayikistán')
INSERT [dbo].[paises] ([id], [pais]) VALUES (181, N'Turkmenistán')
INSERT [dbo].[paises] ([id], [pais]) VALUES (182, N'Uzbekistán')
INSERT [dbo].[paises] ([id], [pais]) VALUES (183, N'Bhután')
INSERT [dbo].[paises] ([id], [pais]) VALUES (184, N'Palestina. Estado Observador, no miembro de Naciones Unidas')
INSERT [dbo].[paises] ([id], [pais]) VALUES (185, N'Otros países de Asia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (186, N'Australia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (187, N'Fiji')
INSERT [dbo].[paises] ([id], [pais]) VALUES (188, N'Nueva Zelanda')
INSERT [dbo].[paises] ([id], [pais]) VALUES (189, N'Papúa Nueva Guinea')
INSERT [dbo].[paises] ([id], [pais]) VALUES (190, N'Islas Salomón')
INSERT [dbo].[paises] ([id], [pais]) VALUES (191, N'Samoa')
INSERT [dbo].[paises] ([id], [pais]) VALUES (192, N'Tonga')
INSERT [dbo].[paises] ([id], [pais]) VALUES (193, N'Vanuatu')
INSERT [dbo].[paises] ([id], [pais]) VALUES (194, N'Micronesia')
INSERT [dbo].[paises] ([id], [pais]) VALUES (195, N'Tuvalu')
INSERT [dbo].[paises] ([id], [pais]) VALUES (196, N'Islas Cook')
INSERT [dbo].[paises] ([id], [pais]) VALUES (197, N'Kiribati')
INSERT [dbo].[paises] ([id], [pais]) VALUES (198, N'Nauru')
INSERT [dbo].[paises] ([id], [pais]) VALUES (199, N'Palaos')
GO
INSERT [dbo].[paises] ([id], [pais]) VALUES (200, N'Timor Oriental')
INSERT [dbo].[paises] ([id], [pais]) VALUES (201, N'Otros países de Oceanía')
SET IDENTITY_INSERT [dbo].[paises] OFF
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 3/21/2021 7:52:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](95) NULL,
	[apellido] [varchar](95) NULL,
	[email] [varchar](120) NULL,
	[pass] [varchar](120) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[direcciones]  WITH CHECK ADD  CONSTRAINT [fk_cliente] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[clientes] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[direcciones] CHECK CONSTRAINT [fk_cliente]
GO
ALTER TABLE [dbo].[direcciones]  WITH CHECK ADD  CONSTRAINT [fk_pais] FOREIGN KEY([id_pais])
REFERENCES [dbo].[paises] ([id])
GO
ALTER TABLE [dbo].[direcciones] CHECK CONSTRAINT [fk_pais]
GO
ALTER TABLE [dbo].[logs]  WITH CHECK ADD  CONSTRAINT [fk_cliente_logs] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[clientes] ([id])
GO
ALTER TABLE [dbo].[logs] CHECK CONSTRAINT [fk_cliente_logs]
GO
ALTER TABLE [dbo].[logs]  WITH CHECK ADD  CONSTRAINT [fk_usuario_logs] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id])
GO
ALTER TABLE [dbo].[logs] CHECK CONSTRAINT [fk_usuario_logs]
GO

--USUARIO POR DEFECTO PARA ACCEDER AL SISTEMA

INSERT INTO usuarios (nombre, apellido, email, pass) VALUES('john','doe', 'johndoe@algo.com',123456)