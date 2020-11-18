USE [master]
GO
/****** Object:  Database [SAC]    Script Date: 18/11/2020 16:05:21 ******/
CREATE DATABASE [SAC]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Agenda', FILENAME = N'C:\Users\Leonardo\Documents\SQL Server Management Studio\Projects\Agenda\Agenda.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Agenda_log', FILENAME = N'C:\Users\Leonardo\Documents\SQL Server Management Studio\Projects\Agenda\Agenda_log.ldf' , SIZE = 3136KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SAC] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SAC].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SAC] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SAC] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SAC] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SAC] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SAC] SET ARITHABORT OFF 
GO
ALTER DATABASE [SAC] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SAC] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SAC] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SAC] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SAC] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SAC] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SAC] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SAC] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SAC] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SAC] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SAC] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SAC] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SAC] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SAC] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SAC] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SAC] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SAC] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SAC] SET RECOVERY FULL 
GO
ALTER DATABASE [SAC] SET  MULTI_USER 
GO
ALTER DATABASE [SAC] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SAC] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SAC] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SAC] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SAC] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SAC', N'ON'
GO
ALTER DATABASE [SAC] SET QUERY_STORE = OFF
GO
USE [SAC]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [SAC]
GO
/****** Object:  Table [dbo].[Accion]    Script Date: 18/11/2020 16:05:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accion](
	[IdAccion] [int] IDENTITY(1,1) NOT NULL,
	[Controlador] [varchar](150) NOT NULL,
	[Nombre] [varchar](150) NOT NULL,
	[Descripcion] [varchar](250) NOT NULL,
	[IdModulo] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[fechaModificacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Accion] PRIMARY KEY CLUSTERED 
(
	[IdAccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccionPorRol]    Script Date: 18/11/2020 16:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccionPorRol](
	[idRolPorAccion] [int] IDENTITY(1,1) NOT NULL,
	[idRol] [int] NOT NULL,
	[idAccion] [int] NOT NULL,
 CONSTRAINT [PK_AccionPorRol] PRIMARY KEY CLUSTERED 
(
	[idRolPorAccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacto]    Script Date: 18/11/2020 16:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdEvento] [int] NOT NULL,
	[IdPersona] [int] NOT NULL,
 CONSTRAINT [PK_Contacto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Data]    Script Date: 18/11/2020 16:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data](
	[Code] [varchar](100) NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoCivil]    Script Date: 18/11/2020 16:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoCivil](
	[id] [int] NOT NULL,
	[descripcion] [nvarchar](50) NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_EstadoCivil] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evento]    Script Date: 18/11/2020 16:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Tipo] [varchar](350) NULL,
	[FechaInicio] [datetime] NULL,
	[HoraInicio] [time](7) NULL,
	[FechaFin] [datetime] NULL,
	[HoraFin] [time](7) NULL,
	[IdPrioridad] [int] NULL,
	[TodoElDia] [bit] NULL,
	[Asisten] [varchar](500) NULL,
	[Recibido] [varchar](500) NULL,
	[Enviado] [varchar](500) NULL,
	[Pasado] [varchar](500) NULL,
	[Ubicacion] [nvarchar](250) NULL,
	[Organizador] [nvarchar](250) NULL,
	[Obs] [varchar](500) NULL,
	[Activo] [bit] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[FechaModificacion] [datetime] NULL,
 CONSTRAINT [PK_Evento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[homePorRol]    Script Date: 18/11/2020 16:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[homePorRol](
	[IdHome] [int] IDENTITY(1,1) NOT NULL,
	[Metodo] [varchar](50) NULL,
	[Controller] [varchar](50) NULL,
 CONSTRAINT [PK_homePorRol] PRIMARY KEY CLUSTERED 
(
	[IdHome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Localidad]    Script Date: 18/11/2020 16:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Localidad](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[IdProvincia] [int] NOT NULL,
	[Departamento] [varchar](50) NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Localidad] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuSidebar]    Script Date: 18/11/2020 16:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuSidebar](
	[IdMenuSidebar] [int] IDENTITY(1,1) NOT NULL,
	[Icono] [nvarchar](50) NOT NULL,
	[Url] [nvarchar](50) NOT NULL,
	[Titulo] [nvarchar](50) NOT NULL,
	[IdParent] [int] NULL,
	[IdAccion] [int] NULL,
	[Orden] [int] NULL,
	[Activo] [bit] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
 CONSTRAINT [PK__MenuSide__0D85BF1A8151C278] PRIMARY KEY CLUSTERED 
(
	[IdMenuSidebar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewTable]    Script Date: 18/11/2020 16:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewTable](
	[Code] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notificacion]    Script Date: 18/11/2020 16:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notificacion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPersona] [int] NOT NULL,
	[EnvioEmail] [bit] NULL,
	[Obs] [nvarchar](250) NULL,
	[Activo] [bit] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Notificacion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persona]    Script Date: 18/11/2020 16:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persona](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Documento] [char](10) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Apellido] [nvarchar](50) NULL,
	[Email] [nvarchar](150) NULL,
	[Sexo] [char](10) NULL,
	[Cuil] [char](20) NULL,
	[TelefonoMovil] [nvarchar](50) NULL,
	[TelefonoFijo] [nvarchar](50) NULL,
	[TelefonoAlternativo] [nvarchar](50) NULL,
	[CodigoPostal] [nchar](10) NULL,
	[Domicilio] [nvarchar](250) NULL,
	[FechaNacimiento] [datetime] NULL,
	[IdProvinciaDomicilio] [int] NULL,
	[IdLocalidadDomicilio] [int] NULL,
	[FechaCreacion] [datetime] NULL,
	[FechaModificacion] [datetime] NULL,
	[CodigoValidacion] [nchar](10) NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prioridad]    Script Date: 18/11/2020 16:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prioridad](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Color] [nvarchar](50) NULL,
	[Activo] [bit] NOT NULL,
	[Nombre] [nvarchar](50) NULL,
 CONSTRAINT [PK_Prioridad] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 18/11/2020 16:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Proveedore] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Provincia]    Script Date: 18/11/2020 16:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provincia](
	[Id] [int] NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Provincia] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 18/11/2020 16:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](250) NOT NULL,
	[EsAdministrador] [bit] NOT NULL,
	[IdHome] [int] NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 18/11/2020 16:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Password] [varchar](50) NULL,
	[IdPersona] [int] NULL,
	[IdRol] [int] NULL,
	[Activo] [bit] NOT NULL,
	[Creado] [datetime] NULL,
	[Actualizado] [datetime] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accion] ON 

INSERT [dbo].[Accion] ([IdAccion], [Controlador], [Nombre], [Descripcion], [IdModulo], [Activo], [fechaModificacion]) VALUES (1, N'evento', N'index', N'Acceso a Eventos', 1, 1, CAST(N'2020-11-16T21:06:12.000' AS DateTime))
INSERT [dbo].[Accion] ([IdAccion], [Controlador], [Nombre], [Descripcion], [IdModulo], [Activo], [fechaModificacion]) VALUES (2, N'accion', N'index', N'Acceso a  Configuración de Acción', 1, 1, CAST(N'2019-07-24T21:24:32.000' AS DateTime))
INSERT [dbo].[Accion] ([IdAccion], [Controlador], [Nombre], [Descripcion], [IdModulo], [Activo], [fechaModificacion]) VALUES (3, N'calendario', N'index', N'Calendario', 1, 1, CAST(N'2020-11-13T16:13:49.000' AS DateTime))
INSERT [dbo].[Accion] ([IdAccion], [Controlador], [Nombre], [Descripcion], [IdModulo], [Activo], [fechaModificacion]) VALUES (4, N'rol', N'index', N'Roles', 1, 1, CAST(N'2019-07-24T12:09:58.000' AS DateTime))
INSERT [dbo].[Accion] ([IdAccion], [Controlador], [Nombre], [Descripcion], [IdModulo], [Activo], [fechaModificacion]) VALUES (6, N'configuracion', N'index', N'Configuracion de Seguridad', 1, 1, CAST(N'2019-02-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Accion] ([IdAccion], [Controlador], [Nombre], [Descripcion], [IdModulo], [Activo], [fechaModificacion]) VALUES (1008, N'Index', N'Calendario', N'Acceso a Calendario', 1, 1, CAST(N'2020-11-16T21:06:15.000' AS DateTime))
INSERT [dbo].[Accion] ([IdAccion], [Controlador], [Nombre], [Descripcion], [IdModulo], [Activo], [fechaModificacion]) VALUES (1009, N'evento', N'calendario', N'Eventos en Calendario', 1, 1, CAST(N'2020-11-16T21:06:07.000' AS DateTime))
INSERT [dbo].[Accion] ([IdAccion], [Controlador], [Nombre], [Descripcion], [IdModulo], [Activo], [fechaModificacion]) VALUES (2005, N'evento', N'crear', N'Crear eventos de calendario', 1, 0, CAST(N'2020-11-16T21:06:10.000' AS DateTime))
INSERT [dbo].[Accion] ([IdAccion], [Controlador], [Nombre], [Descripcion], [IdModulo], [Activo], [fechaModificacion]) VALUES (2006, N'menusidebar', N'index', N'Gestión de Menú Inicio', 0, 1, CAST(N'2020-11-08T10:40:59.000' AS DateTime))
INSERT [dbo].[Accion] ([IdAccion], [Controlador], [Nombre], [Descripcion], [IdModulo], [Activo], [fechaModificacion]) VALUES (3006, N'proveedor', N'index', N'Gestión de Proveedor', 0, 1, CAST(N'2020-11-16T19:15:15.000' AS DateTime))
INSERT [dbo].[Accion] ([IdAccion], [Controlador], [Nombre], [Descripcion], [IdModulo], [Activo], [fechaModificacion]) VALUES (3007, N's/e', N's/e', N'Compras', 0, 1, CAST(N'2020-11-18T15:26:06.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Accion] OFF
SET IDENTITY_INSERT [dbo].[AccionPorRol] ON 

INSERT [dbo].[AccionPorRol] ([idRolPorAccion], [idRol], [idAccion]) VALUES (4, 1, 4)
INSERT [dbo].[AccionPorRol] ([idRolPorAccion], [idRol], [idAccion]) VALUES (1003, 1, 6)
INSERT [dbo].[AccionPorRol] ([idRolPorAccion], [idRol], [idAccion]) VALUES (4004, 2, 2006)
INSERT [dbo].[AccionPorRol] ([idRolPorAccion], [idRol], [idAccion]) VALUES (5003, 2, 2)
INSERT [dbo].[AccionPorRol] ([idRolPorAccion], [idRol], [idAccion]) VALUES (5009, 1, 2)
INSERT [dbo].[AccionPorRol] ([idRolPorAccion], [idRol], [idAccion]) VALUES (5010, 1, 2006)
INSERT [dbo].[AccionPorRol] ([idRolPorAccion], [idRol], [idAccion]) VALUES (5036, 1, 1)
INSERT [dbo].[AccionPorRol] ([idRolPorAccion], [idRol], [idAccion]) VALUES (5037, 1, 3006)
SET IDENTITY_INSERT [dbo].[AccionPorRol] OFF
SET IDENTITY_INSERT [dbo].[Evento] ON 

INSERT [dbo].[Evento] ([Id], [Tipo], [FechaInicio], [HoraInicio], [FechaFin], [HoraFin], [IdPrioridad], [TodoElDia], [Asisten], [Recibido], [Enviado], [Pasado], [Ubicacion], [Organizador], [Obs], [Activo], [IdUsuario], [FechaModificacion]) VALUES (7, NULL, CAST(N'2019-07-11T14:20:00.000' AS DateTime), CAST(N'14:20:00' AS Time), CAST(N'2019-07-11T14:20:00.000' AS DateTime), CAST(N'14:20:00' AS Time), NULL, NULL, N'Martines', N'Zapana', N'Bre', NULL, NULL, NULL, NULL, 0, 1, CAST(N'2019-07-11T14:21:09.000' AS DateTime))
INSERT [dbo].[Evento] ([Id], [Tipo], [FechaInicio], [HoraInicio], [FechaFin], [HoraFin], [IdPrioridad], [TodoElDia], [Asisten], [Recibido], [Enviado], [Pasado], [Ubicacion], [Organizador], [Obs], [Activo], [IdUsuario], [FechaModificacion]) VALUES (8, NULL, CAST(N'2019-07-11T14:21:00.000' AS DateTime), CAST(N'14:21:00' AS Time), CAST(N'2019-07-11T14:21:00.000' AS DateTime), CAST(N'14:21:00' AS Time), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, CAST(N'2019-07-11T14:22:36.000' AS DateTime))
INSERT [dbo].[Evento] ([Id], [Tipo], [FechaInicio], [HoraInicio], [FechaFin], [HoraFin], [IdPrioridad], [TodoElDia], [Asisten], [Recibido], [Enviado], [Pasado], [Ubicacion], [Organizador], [Obs], [Activo], [IdUsuario], [FechaModificacion]) VALUES (9, N'reunion', CAST(N'2019-07-12T11:30:00.000' AS DateTime), CAST(N'11:30:00' AS Time), CAST(N'2019-07-31T13:00:00.000' AS DateTime), CAST(N'13:00:00' AS Time), NULL, NULL, N'ricardo', N'jose', N'afirma', NULL, N'lugar ', NULL, N'concurrir con documentación correspondiente', 0, 1, CAST(N'2019-07-12T00:08:47.000' AS DateTime))
INSERT [dbo].[Evento] ([Id], [Tipo], [FechaInicio], [HoraInicio], [FechaFin], [HoraFin], [IdPrioridad], [TodoElDia], [Asisten], [Recibido], [Enviado], [Pasado], [Ubicacion], [Organizador], [Obs], [Activo], [IdUsuario], [FechaModificacion]) VALUES (10, N'Urgente', CAST(N'2019-07-11T20:33:00.000' AS DateTime), CAST(N'20:33:00' AS Time), CAST(N'2019-07-31T20:33:00.000' AS DateTime), CAST(N'20:33:00' AS Time), NULL, NULL, N'tipo de evento ', N'por personal de unidad ', N'a su ddomicilio', NULL, N'AA1480', NULL, NULL, 0, 1, CAST(N'2019-07-11T20:34:52.000' AS DateTime))
INSERT [dbo].[Evento] ([Id], [Tipo], [FechaInicio], [HoraInicio], [FechaFin], [HoraFin], [IdPrioridad], [TodoElDia], [Asisten], [Recibido], [Enviado], [Pasado], [Ubicacion], [Organizador], [Obs], [Activo], [IdUsuario], [FechaModificacion]) VALUES (11, NULL, CAST(N'2019-07-11T20:39:00.000' AS DateTime), CAST(N'20:39:00' AS Time), CAST(N'2019-07-11T20:39:00.000' AS DateTime), CAST(N'20:39:00' AS Time), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, CAST(N'2019-07-11T20:42:54.000' AS DateTime))
INSERT [dbo].[Evento] ([Id], [Tipo], [FechaInicio], [HoraInicio], [FechaFin], [HoraFin], [IdPrioridad], [TodoElDia], [Asisten], [Recibido], [Enviado], [Pasado], [Ubicacion], [Organizador], [Obs], [Activo], [IdUsuario], [FechaModificacion]) VALUES (12, NULL, CAST(N'2019-07-11T20:43:00.000' AS DateTime), CAST(N'20:43:00' AS Time), CAST(N'2019-07-11T20:43:00.000' AS DateTime), CAST(N'20:43:00' AS Time), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, CAST(N'2019-07-11T20:43:28.000' AS DateTime))
INSERT [dbo].[Evento] ([Id], [Tipo], [FechaInicio], [HoraInicio], [FechaFin], [HoraFin], [IdPrioridad], [TodoElDia], [Asisten], [Recibido], [Enviado], [Pasado], [Ubicacion], [Organizador], [Obs], [Activo], [IdUsuario], [FechaModificacion]) VALUES (13, N'tipo uno', CAST(N'2019-07-11T09:00:00.000' AS DateTime), CAST(N'09:00:00' AS Time), CAST(N'2019-07-31T11:00:00.000' AS DateTime), CAST(N'11:00:00' AS Time), NULL, NULL, N'evento uno ', N'recibio uno', N'envio uno', NULL, N'ubicacion uno', NULL, N'obs uno', 1, 1, CAST(N'2019-07-11T23:50:38.000' AS DateTime))
INSERT [dbo].[Evento] ([Id], [Tipo], [FechaInicio], [HoraInicio], [FechaFin], [HoraFin], [IdPrioridad], [TodoElDia], [Asisten], [Recibido], [Enviado], [Pasado], [Ubicacion], [Organizador], [Obs], [Activo], [IdUsuario], [FechaModificacion]) VALUES (14, N'reunion', CAST(N'2019-07-15T08:00:00.000' AS DateTime), CAST(N'08:00:00' AS Time), CAST(N'2019-07-29T00:00:00.000' AS DateTime), CAST(N'00:00:00' AS Time), NULL, NULL, N'evento 2', N'recibio 2', N'envio 2', NULL, N'ubicacion 2', NULL, N'2', 0, 1, CAST(N'2019-07-12T00:08:06.000' AS DateTime))
INSERT [dbo].[Evento] ([Id], [Tipo], [FechaInicio], [HoraInicio], [FechaFin], [HoraFin], [IdPrioridad], [TodoElDia], [Asisten], [Recibido], [Enviado], [Pasado], [Ubicacion], [Organizador], [Obs], [Activo], [IdUsuario], [FechaModificacion]) VALUES (15, N'reunion', CAST(N'2019-07-01T08:00:00.000' AS DateTime), CAST(N'08:00:00' AS Time), CAST(N'2019-07-21T15:00:00.000' AS DateTime), CAST(N'15:00:00' AS Time), NULL, NULL, N'todos', N'recibio uno', N'envio uno', NULL, N'ubicacion uno', NULL, NULL, 1, 1, CAST(N'2019-07-14T13:18:44.000' AS DateTime))
INSERT [dbo].[Evento] ([Id], [Tipo], [FechaInicio], [HoraInicio], [FechaFin], [HoraFin], [IdPrioridad], [TodoElDia], [Asisten], [Recibido], [Enviado], [Pasado], [Ubicacion], [Organizador], [Obs], [Activo], [IdUsuario], [FechaModificacion]) VALUES (16, N'tipo uno', CAST(N'2019-07-14T18:12:00.000' AS DateTime), CAST(N'18:12:00' AS Time), CAST(N'2019-07-14T18:12:00.000' AS DateTime), CAST(N'18:12:00' AS Time), NULL, NULL, N'Pepe', N'recibio uno', N'envio 2', NULL, N'ubicacion 2', NULL, NULL, 1, 1, CAST(N'2019-07-14T18:12:17.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Evento] OFF
SET IDENTITY_INSERT [dbo].[homePorRol] ON 

INSERT [dbo].[homePorRol] ([IdHome], [Metodo], [Controller]) VALUES (1, N'Index', N'Evento')
SET IDENTITY_INSERT [dbo].[homePorRol] OFF
SET IDENTITY_INSERT [dbo].[MenuSidebar] ON 

INSERT [dbo].[MenuSidebar] ([IdMenuSidebar], [Icono], [Url], [Titulo], [IdParent], [IdAccion], [Orden], [Activo], [FechaModificacion]) VALUES (5, N'fa-cogs', N'CreateMenusidebar', N'Configuración', NULL, 6, NULL, 1, CAST(N'2019-07-24T11:37:42.000' AS DateTime))
INSERT [dbo].[MenuSidebar] ([IdMenuSidebar], [Icono], [Url], [Titulo], [IdParent], [IdAccion], [Orden], [Activo], [FechaModificacion]) VALUES (2004, N'fa fa-bars', N'CreateMenusidebar', N'Menú Inicio', 5, 2006, NULL, 1, CAST(N'2020-10-25T14:08:58.000' AS DateTime))
INSERT [dbo].[MenuSidebar] ([IdMenuSidebar], [Icono], [Url], [Titulo], [IdParent], [IdAccion], [Orden], [Activo], [FechaModificacion]) VALUES (3005, N'fa-building', N'CreateMenusidebar', N'Mayorista', 5, 3, NULL, 0, CAST(N'2020-11-08T21:13:46.000' AS DateTime))
INSERT [dbo].[MenuSidebar] ([IdMenuSidebar], [Icono], [Url], [Titulo], [IdParent], [IdAccion], [Orden], [Activo], [FechaModificacion]) VALUES (3006, N'fa fa-square', N'CreateMenusidebar', N'Roles', 5, 4, NULL, 1, CAST(N'2020-11-04T18:43:00.000' AS DateTime))
INSERT [dbo].[MenuSidebar] ([IdMenuSidebar], [Icono], [Url], [Titulo], [IdParent], [IdAccion], [Orden], [Activo], [FechaModificacion]) VALUES (4006, N'fa fa-circle', N'CreateMenusidebar', N'Acciones', 5, 2, NULL, 1, CAST(N'2020-11-04T18:43:00.000' AS DateTime))
INSERT [dbo].[MenuSidebar] ([IdMenuSidebar], [Icono], [Url], [Titulo], [IdParent], [IdAccion], [Orden], [Activo], [FechaModificacion]) VALUES (5008, N'fa-cubes', N'CreateMenusidebar', N'Proveedor', NULL, 3006, NULL, 1, CAST(N'2020-11-16T19:18:05.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[MenuSidebar] OFF
SET IDENTITY_INSERT [dbo].[Persona] ON 

INSERT [dbo].[Persona] ([Id], [Documento], [Nombre], [Apellido], [Email], [Sexo], [Cuil], [TelefonoMovil], [TelefonoFijo], [TelefonoAlternativo], [CodigoPostal], [Domicilio], [FechaNacimiento], [IdProvinciaDomicilio], [IdLocalidadDomicilio], [FechaCreacion], [FechaModificacion], [CodigoValidacion], [Activo]) VALUES (1, N'1234      ', N'Administrador', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Persona] OFF
SET IDENTITY_INSERT [dbo].[Prioridad] ON 

INSERT [dbo].[Prioridad] ([Id], [Color], [Activo], [Nombre]) VALUES (1, N'bg-danger', 1, NULL)
INSERT [dbo].[Prioridad] ([Id], [Color], [Activo], [Nombre]) VALUES (2, N'bg-success', 1, NULL)
INSERT [dbo].[Prioridad] ([Id], [Color], [Activo], [Nombre]) VALUES (3, N'bg-warning', 1, NULL)
INSERT [dbo].[Prioridad] ([Id], [Color], [Activo], [Nombre]) VALUES (4, N'bg-info', 1, NULL)
SET IDENTITY_INSERT [dbo].[Prioridad] OFF
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([IdRol], [Descripcion], [EsAdministrador], [IdHome]) VALUES (1, N'Administrador', 1, 1)
INSERT [dbo].[Rol] ([IdRol], [Descripcion], [EsAdministrador], [IdHome]) VALUES (2, N'Ventas x Mayor', 0, NULL)
SET IDENTITY_INSERT [dbo].[Rol] OFF
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([IdUsuario], [Password], [IdPersona], [IdRol], [Activo], [Creado], [Actualizado]) VALUES (1, N'202cb962ac59075b964b07152d234b70', 1, 1, 1, CAST(N'2019-11-07T00:00:00.000' AS DateTime), CAST(N'2019-11-07T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Usuario] OFF
/****** Object:  Index [IX_FK_AccionesPorRoles_Acciones]    Script Date: 18/11/2020 16:05:23 ******/
CREATE NONCLUSTERED INDEX [IX_FK_AccionesPorRoles_Acciones] ON [dbo].[AccionPorRol]
(
	[idAccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_AccionesPorRoles_Roles]    Script Date: 18/11/2020 16:05:23 ******/
CREATE NONCLUSTERED INDEX [IX_FK_AccionesPorRoles_Roles] ON [dbo].[AccionPorRol]
(
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Contacto_Evento]    Script Date: 18/11/2020 16:05:23 ******/
CREATE NONCLUSTERED INDEX [IX_FK_Contacto_Evento] ON [dbo].[Contacto]
(
	[IdEvento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Contacto_Persona]    Script Date: 18/11/2020 16:05:23 ******/
CREATE NONCLUSTERED INDEX [IX_FK_Contacto_Persona] ON [dbo].[Contacto]
(
	[IdPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Evento_Usuario]    Script Date: 18/11/2020 16:05:23 ******/
CREATE NONCLUSTERED INDEX [IX_FK_Evento_Usuario] ON [dbo].[Evento]
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Localidad_Provincia]    Script Date: 18/11/2020 16:05:23 ******/
CREATE NONCLUSTERED INDEX [IX_FK_Localidad_Provincia] ON [dbo].[Localidad]
(
	[IdProvincia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Notificacion_Persona]    Script Date: 18/11/2020 16:05:23 ******/
CREATE NONCLUSTERED INDEX [IX_FK_Notificacion_Persona] ON [dbo].[Notificacion]
(
	[IdPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Personas_Localidades1]    Script Date: 18/11/2020 16:05:23 ******/
CREATE NONCLUSTERED INDEX [IX_FK_Personas_Localidades1] ON [dbo].[Persona]
(
	[IdLocalidadDomicilio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Personas_Provincias1]    Script Date: 18/11/2020 16:05:23 ******/
CREATE NONCLUSTERED INDEX [IX_FK_Personas_Provincias1] ON [dbo].[Persona]
(
	[IdProvinciaDomicilio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Roles_HomePorRol]    Script Date: 18/11/2020 16:05:23 ******/
CREATE NONCLUSTERED INDEX [IX_FK_Roles_HomePorRol] ON [dbo].[Rol]
(
	[IdHome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Usuarios_Roles]    Script Date: 18/11/2020 16:05:23 ******/
CREATE NONCLUSTERED INDEX [IX_FK_Usuarios_Roles] ON [dbo].[Usuario]
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accion] ADD  CONSTRAINT [DF_Accion_activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Accion] ADD  CONSTRAINT [DF_Accion_fechaModificacion]  DEFAULT (getdate()) FOR [fechaModificacion]
GO
ALTER TABLE [dbo].[Evento] ADD  CONSTRAINT [DF_Evento_todoElDia]  DEFAULT ((1)) FOR [TodoElDia]
GO
ALTER TABLE [dbo].[Evento] ADD  CONSTRAINT [DF_Evento_activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Evento] ADD  CONSTRAINT [DF_Evento_fechaModificacion]  DEFAULT (getdate()) FOR [FechaModificacion]
GO
ALTER TABLE [dbo].[MenuSidebar] ADD  CONSTRAINT [DF__MenuSideb__Icono__05D8E0BE]  DEFAULT ('fa-bars') FOR [Icono]
GO
ALTER TABLE [dbo].[MenuSidebar] ADD  CONSTRAINT [DF__MenuSidebar__Url__06CD04F7]  DEFAULT ('algo') FOR [Url]
GO
ALTER TABLE [dbo].[MenuSidebar] ADD  CONSTRAINT [DF__MenuSideb__activ__07C12930]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[MenuSidebar] ADD  CONSTRAINT [DF__MenuSideb__Fecha__08B54D69]  DEFAULT (getdate()) FOR [FechaModificacion]
GO
ALTER TABLE [dbo].[AccionPorRol]  WITH CHECK ADD  CONSTRAINT [FK_AccionesPorRoles_Acciones] FOREIGN KEY([idAccion])
REFERENCES [dbo].[Accion] ([IdAccion])
GO
ALTER TABLE [dbo].[AccionPorRol] CHECK CONSTRAINT [FK_AccionesPorRoles_Acciones]
GO
ALTER TABLE [dbo].[AccionPorRol]  WITH CHECK ADD  CONSTRAINT [FK_AccionesPorRoles_Roles] FOREIGN KEY([idRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[AccionPorRol] CHECK CONSTRAINT [FK_AccionesPorRoles_Roles]
GO
ALTER TABLE [dbo].[Contacto]  WITH CHECK ADD  CONSTRAINT [FK_Contacto_Evento] FOREIGN KEY([IdEvento])
REFERENCES [dbo].[Evento] ([Id])
GO
ALTER TABLE [dbo].[Contacto] CHECK CONSTRAINT [FK_Contacto_Evento]
GO
ALTER TABLE [dbo].[Contacto]  WITH CHECK ADD  CONSTRAINT [FK_Contacto_Persona] FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Persona] ([Id])
GO
ALTER TABLE [dbo].[Contacto] CHECK CONSTRAINT [FK_Contacto_Persona]
GO
ALTER TABLE [dbo].[Evento]  WITH CHECK ADD  CONSTRAINT [FK_Evento_Prioridad] FOREIGN KEY([IdPrioridad])
REFERENCES [dbo].[Prioridad] ([Id])
GO
ALTER TABLE [dbo].[Evento] CHECK CONSTRAINT [FK_Evento_Prioridad]
GO
ALTER TABLE [dbo].[Evento]  WITH CHECK ADD  CONSTRAINT [FK_Evento_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Evento] CHECK CONSTRAINT [FK_Evento_Usuario]
GO
ALTER TABLE [dbo].[Localidad]  WITH CHECK ADD  CONSTRAINT [FK_Localidad_Provincia] FOREIGN KEY([IdProvincia])
REFERENCES [dbo].[Provincia] ([Id])
GO
ALTER TABLE [dbo].[Localidad] CHECK CONSTRAINT [FK_Localidad_Provincia]
GO
ALTER TABLE [dbo].[MenuSidebar]  WITH CHECK ADD  CONSTRAINT [FK_MenuSidebar_Accion] FOREIGN KEY([IdAccion])
REFERENCES [dbo].[Accion] ([IdAccion])
GO
ALTER TABLE [dbo].[MenuSidebar] CHECK CONSTRAINT [FK_MenuSidebar_Accion]
GO
ALTER TABLE [dbo].[MenuSidebar]  WITH CHECK ADD  CONSTRAINT [FK_MenuSidebar_MenuSidebar] FOREIGN KEY([IdParent])
REFERENCES [dbo].[MenuSidebar] ([IdMenuSidebar])
GO
ALTER TABLE [dbo].[MenuSidebar] CHECK CONSTRAINT [FK_MenuSidebar_MenuSidebar]
GO
ALTER TABLE [dbo].[Notificacion]  WITH CHECK ADD  CONSTRAINT [FK_Notificacion_Persona] FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Persona] ([Id])
GO
ALTER TABLE [dbo].[Notificacion] CHECK CONSTRAINT [FK_Notificacion_Persona]
GO
ALTER TABLE [dbo].[Persona]  WITH CHECK ADD  CONSTRAINT [FK_Personas_Localidades1] FOREIGN KEY([IdLocalidadDomicilio])
REFERENCES [dbo].[Localidad] ([Id])
GO
ALTER TABLE [dbo].[Persona] CHECK CONSTRAINT [FK_Personas_Localidades1]
GO
ALTER TABLE [dbo].[Persona]  WITH CHECK ADD  CONSTRAINT [FK_Personas_Provincias1] FOREIGN KEY([IdProvinciaDomicilio])
REFERENCES [dbo].[Provincia] ([Id])
GO
ALTER TABLE [dbo].[Persona] CHECK CONSTRAINT [FK_Personas_Provincias1]
GO
ALTER TABLE [dbo].[Rol]  WITH CHECK ADD  CONSTRAINT [FK_Roles_HomePorRol] FOREIGN KEY([IdHome])
REFERENCES [dbo].[homePorRol] ([IdHome])
GO
ALTER TABLE [dbo].[Rol] CHECK CONSTRAINT [FK_Roles_HomePorRol]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Persona] FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Persona] ([Id])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Persona]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Roles] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuarios_Roles]
GO
USE [master]
GO
ALTER DATABASE [SAC] SET  READ_WRITE 
GO
