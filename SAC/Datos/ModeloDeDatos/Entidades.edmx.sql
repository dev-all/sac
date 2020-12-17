
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/12/2019 08:36:10
-- Generated from EDMX file: F:\Agenda\Agenda\Datos\ModeloDeDatos\Entidades.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Agenda];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AccionesPorRoles_Acciones]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccionPorRol] DROP CONSTRAINT [FK_AccionesPorRoles_Acciones];
GO
IF OBJECT_ID(N'[dbo].[FK_AccionesPorRoles_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccionPorRol] DROP CONSTRAINT [FK_AccionesPorRoles_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_Contacto_Evento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contacto] DROP CONSTRAINT [FK_Contacto_Evento];
GO
IF OBJECT_ID(N'[dbo].[FK_Contacto_Persona]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contacto] DROP CONSTRAINT [FK_Contacto_Persona];
GO
IF OBJECT_ID(N'[dbo].[FK_Evento_Usuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Evento] DROP CONSTRAINT [FK_Evento_Usuario];
GO
IF OBJECT_ID(N'[dbo].[FK_Localidad_Provincia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Localidad] DROP CONSTRAINT [FK_Localidad_Provincia];
GO
IF OBJECT_ID(N'[dbo].[FK_Notificacion_Persona]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notificacion] DROP CONSTRAINT [FK_Notificacion_Persona];
GO
IF OBJECT_ID(N'[dbo].[FK_Personas_EstadoCivil]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persona] DROP CONSTRAINT [FK_Personas_EstadoCivil];
GO
IF OBJECT_ID(N'[dbo].[FK_Personas_Localidades]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persona] DROP CONSTRAINT [FK_Personas_Localidades];
GO
IF OBJECT_ID(N'[dbo].[FK_Personas_Localidades1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persona] DROP CONSTRAINT [FK_Personas_Localidades1];
GO
IF OBJECT_ID(N'[dbo].[FK_Personas_Provincias]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persona] DROP CONSTRAINT [FK_Personas_Provincias];
GO
IF OBJECT_ID(N'[dbo].[FK_Personas_Provincias1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persona] DROP CONSTRAINT [FK_Personas_Provincias1];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonaUsuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK_PersonaUsuario];
GO
IF OBJECT_ID(N'[dbo].[FK_Roles_HomePorRol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rol] DROP CONSTRAINT [FK_Roles_HomePorRol];
GO
IF OBJECT_ID(N'[dbo].[FK_Usuarios_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK_Usuarios_Roles];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Accion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accion];
GO
IF OBJECT_ID(N'[dbo].[AccionPorRol]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccionPorRol];
GO
IF OBJECT_ID(N'[dbo].[Contacto]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contacto];
GO
IF OBJECT_ID(N'[dbo].[EstadoCivil]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EstadoCivil];
GO
IF OBJECT_ID(N'[dbo].[Evento]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Evento];
GO
IF OBJECT_ID(N'[dbo].[homePorRol]', 'U') IS NOT NULL
    DROP TABLE [dbo].[homePorRol];
GO
IF OBJECT_ID(N'[dbo].[Localidad]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Localidad];
GO
IF OBJECT_ID(N'[dbo].[Notificacion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notificacion];
GO
IF OBJECT_ID(N'[dbo].[Persona]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persona];
GO
IF OBJECT_ID(N'[dbo].[Provincia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Provincia];
GO
IF OBJECT_ID(N'[dbo].[Rol]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rol];
GO
IF OBJECT_ID(N'[dbo].[Usuario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuario];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accion'
CREATE TABLE [dbo].[Accion] (
    [idAccion] int IDENTITY(1,1) NOT NULL,
    [controlador] varchar(150)  NOT NULL,
    [nombre] varchar(150)  NOT NULL,
    [idSistema] int  NOT NULL
);
GO

-- Creating table 'AccionPorRol'
CREATE TABLE [dbo].[AccionPorRol] (
    [idRolPorAccion] int IDENTITY(1,1) NOT NULL,
    [idRol] int  NOT NULL,
    [idAccion] int  NOT NULL
);
GO

-- Creating table 'EstadoCivil'
CREATE TABLE [dbo].[EstadoCivil] (
    [id] int  NOT NULL,
    [descripcion] nvarchar(50)  NULL,
    [activo] bit  NOT NULL
);
GO

-- Creating table 'homePorRol'
CREATE TABLE [dbo].[homePorRol] (
    [idHome] int IDENTITY(1,1) NOT NULL,
    [metodo] varchar(50)  NULL,
    [controller] varchar(50)  NULL
);
GO

-- Creating table 'Localidad'
CREATE TABLE [dbo].[Localidad] (
    [id] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(50)  NOT NULL,
    [idProvincia] int  NOT NULL,
    [departamento] varchar(50)  NULL,
    [activo] bit  NOT NULL
);
GO

-- Creating table 'Notificacion'
CREATE TABLE [dbo].[Notificacion] (
    [id] int IDENTITY(1,1) NOT NULL,
    [idPersona] int  NOT NULL,
    [envioEmail] bit  NULL,
    [obs] nvarchar(250)  NULL,
    [activo] bit  NOT NULL,
    [fechaCreacion] datetime  NOT NULL
);
GO

-- Creating table 'Persona'
CREATE TABLE [dbo].[Persona] (
    [id] int IDENTITY(1,1) NOT NULL,
    [documento] char(10)  NOT NULL,
    [primerNombre] nvarchar(50)  NULL,
    [segundoNombre] nvarchar(50)  NULL,
    [primerApellido] nvarchar(50)  NULL,
    [segundoApellido] nvarchar(50)  NULL,
    [email] nvarchar(150)  NULL,
    [sexo] char(10)  NULL,
    [cuil] char(20)  NULL,
    [telefonoFijo] nvarchar(50)  NULL,
    [telefono] nvarchar(50)  NULL,
    [telefonoAlternativo] nvarchar(50)  NULL,
    [codigoPostal] nchar(10)  NULL,
    [domicilio] nvarchar(250)  NULL,
    [idEstadoCivil] int  NULL,
    [fechaNacimiento] datetime  NULL,
    [idPaisNacimiento] int  NULL,
    [idProvinciaNacimiento] int  NULL,
    [idLocalidadNacimiento] int  NULL,
    [idProvinciaDomicilio] int  NULL,
    [idLocalidadDomicilio] int  NULL,
    [fechaCreacion] datetime  NULL,
    [fechaModificacion] datetime  NULL,
    [codigoValidacion] nchar(10)  NULL,
    [activo] bit  NULL
);
GO

-- Creating table 'Provincia'
CREATE TABLE [dbo].[Provincia] (
    [id] int  NOT NULL,
    [nombre] nvarchar(50)  NULL,
    [activo] bit  NOT NULL
);
GO

-- Creating table 'Rol'
CREATE TABLE [dbo].[Rol] (
    [idRol] int IDENTITY(1,1) NOT NULL,
    [descripcion] varchar(250)  NOT NULL,
    [esAdministrador] bit  NOT NULL,
    [idHome] int  NULL
);
GO

-- Creating table 'Usuario'
CREATE TABLE [dbo].[Usuario] (
    [idUsuario] int IDENTITY(1,1) NOT NULL,
    [password] varchar(50)  NULL,
    [activo] bit  NOT NULL,
    [Creado] datetime  NULL,
    [Actualizado] datetime  NULL,
    [idPersona] int  NULL,
    [idRol] int  NULL,
    [Usuario1] int  NULL,
    [Persona_id] int  NULL
);
GO

-- Creating table 'Contacto'
CREATE TABLE [dbo].[Contacto] (
    [id] int IDENTITY(1,1) NOT NULL,
    [idEvento] int  NOT NULL,
    [idPersona] int  NOT NULL
);
GO

-- Creating table 'Evento'
CREATE TABLE [dbo].[Evento] (
    [id] int IDENTITY(1,1) NOT NULL,
    [tipo] varchar(350)  NULL,
    [fechaInicio] datetime  NULL,
    [horaInicio] time  NULL,
    [fechaFin] datetime  NULL,
    [horaFin] time  NULL,
    [asisten] varchar(500)  NULL,
    [recibido] varchar(500)  NULL,
    [enviado] varchar(500)  NULL,
    [ubicacion] nvarchar(250)  NULL,
    [obs] varchar(500)  NULL,
    [activo] bit  NOT NULL,
    [idUsuario] int  NOT NULL,
    [fechaModificacion] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [idAccion] in table 'Accion'
ALTER TABLE [dbo].[Accion]
ADD CONSTRAINT [PK_Accion]
    PRIMARY KEY CLUSTERED ([idAccion] ASC);
GO

-- Creating primary key on [idRolPorAccion] in table 'AccionPorRol'
ALTER TABLE [dbo].[AccionPorRol]
ADD CONSTRAINT [PK_AccionPorRol]
    PRIMARY KEY CLUSTERED ([idRolPorAccion] ASC);
GO

-- Creating primary key on [id] in table 'EstadoCivil'
ALTER TABLE [dbo].[EstadoCivil]
ADD CONSTRAINT [PK_EstadoCivil]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [idHome] in table 'homePorRol'
ALTER TABLE [dbo].[homePorRol]
ADD CONSTRAINT [PK_homePorRol]
    PRIMARY KEY CLUSTERED ([idHome] ASC);
GO

-- Creating primary key on [id] in table 'Localidad'
ALTER TABLE [dbo].[Localidad]
ADD CONSTRAINT [PK_Localidad]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Notificacion'
ALTER TABLE [dbo].[Notificacion]
ADD CONSTRAINT [PK_Notificacion]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Persona'
ALTER TABLE [dbo].[Persona]
ADD CONSTRAINT [PK_Persona]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Provincia'
ALTER TABLE [dbo].[Provincia]
ADD CONSTRAINT [PK_Provincia]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [idRol] in table 'Rol'
ALTER TABLE [dbo].[Rol]
ADD CONSTRAINT [PK_Rol]
    PRIMARY KEY CLUSTERED ([idRol] ASC);
GO

-- Creating primary key on [idUsuario] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [PK_Usuario]
    PRIMARY KEY CLUSTERED ([idUsuario] ASC);
GO

-- Creating primary key on [id] in table 'Contacto'
ALTER TABLE [dbo].[Contacto]
ADD CONSTRAINT [PK_Contacto]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Evento'
ALTER TABLE [dbo].[Evento]
ADD CONSTRAINT [PK_Evento]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [idAccion] in table 'AccionPorRol'
ALTER TABLE [dbo].[AccionPorRol]
ADD CONSTRAINT [FK_AccionesPorRoles_Acciones]
    FOREIGN KEY ([idAccion])
    REFERENCES [dbo].[Accion]
        ([idAccion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccionesPorRoles_Acciones'
CREATE INDEX [IX_FK_AccionesPorRoles_Acciones]
ON [dbo].[AccionPorRol]
    ([idAccion]);
GO

-- Creating foreign key on [idRol] in table 'AccionPorRol'
ALTER TABLE [dbo].[AccionPorRol]
ADD CONSTRAINT [FK_AccionesPorRoles_Roles]
    FOREIGN KEY ([idRol])
    REFERENCES [dbo].[Rol]
        ([idRol])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccionesPorRoles_Roles'
CREATE INDEX [IX_FK_AccionesPorRoles_Roles]
ON [dbo].[AccionPorRol]
    ([idRol]);
GO

-- Creating foreign key on [idEstadoCivil] in table 'Persona'
ALTER TABLE [dbo].[Persona]
ADD CONSTRAINT [FK_Personas_EstadoCivil]
    FOREIGN KEY ([idEstadoCivil])
    REFERENCES [dbo].[EstadoCivil]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Personas_EstadoCivil'
CREATE INDEX [IX_FK_Personas_EstadoCivil]
ON [dbo].[Persona]
    ([idEstadoCivil]);
GO

-- Creating foreign key on [idHome] in table 'Rol'
ALTER TABLE [dbo].[Rol]
ADD CONSTRAINT [FK_Roles_HomePorRol]
    FOREIGN KEY ([idHome])
    REFERENCES [dbo].[homePorRol]
        ([idHome])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Roles_HomePorRol'
CREATE INDEX [IX_FK_Roles_HomePorRol]
ON [dbo].[Rol]
    ([idHome]);
GO

-- Creating foreign key on [idProvincia] in table 'Localidad'
ALTER TABLE [dbo].[Localidad]
ADD CONSTRAINT [FK_Localidad_Provincia]
    FOREIGN KEY ([idProvincia])
    REFERENCES [dbo].[Provincia]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Localidad_Provincia'
CREATE INDEX [IX_FK_Localidad_Provincia]
ON [dbo].[Localidad]
    ([idProvincia]);
GO

-- Creating foreign key on [idLocalidadNacimiento] in table 'Persona'
ALTER TABLE [dbo].[Persona]
ADD CONSTRAINT [FK_Personas_Localidades]
    FOREIGN KEY ([idLocalidadNacimiento])
    REFERENCES [dbo].[Localidad]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Personas_Localidades'
CREATE INDEX [IX_FK_Personas_Localidades]
ON [dbo].[Persona]
    ([idLocalidadNacimiento]);
GO

-- Creating foreign key on [idLocalidadDomicilio] in table 'Persona'
ALTER TABLE [dbo].[Persona]
ADD CONSTRAINT [FK_Personas_Localidades1]
    FOREIGN KEY ([idLocalidadDomicilio])
    REFERENCES [dbo].[Localidad]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Personas_Localidades1'
CREATE INDEX [IX_FK_Personas_Localidades1]
ON [dbo].[Persona]
    ([idLocalidadDomicilio]);
GO

-- Creating foreign key on [idPersona] in table 'Notificacion'
ALTER TABLE [dbo].[Notificacion]
ADD CONSTRAINT [FK_Notificacion_Persona]
    FOREIGN KEY ([idPersona])
    REFERENCES [dbo].[Persona]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Notificacion_Persona'
CREATE INDEX [IX_FK_Notificacion_Persona]
ON [dbo].[Notificacion]
    ([idPersona]);
GO

-- Creating foreign key on [idProvinciaNacimiento] in table 'Persona'
ALTER TABLE [dbo].[Persona]
ADD CONSTRAINT [FK_Personas_Provincias]
    FOREIGN KEY ([idProvinciaNacimiento])
    REFERENCES [dbo].[Provincia]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Personas_Provincias'
CREATE INDEX [IX_FK_Personas_Provincias]
ON [dbo].[Persona]
    ([idProvinciaNacimiento]);
GO

-- Creating foreign key on [idProvinciaDomicilio] in table 'Persona'
ALTER TABLE [dbo].[Persona]
ADD CONSTRAINT [FK_Personas_Provincias1]
    FOREIGN KEY ([idProvinciaDomicilio])
    REFERENCES [dbo].[Provincia]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Personas_Provincias1'
CREATE INDEX [IX_FK_Personas_Provincias1]
ON [dbo].[Persona]
    ([idProvinciaDomicilio]);
GO

-- Creating foreign key on [Persona_id] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [FK_PersonaUsuario]
    FOREIGN KEY ([Persona_id])
    REFERENCES [dbo].[Persona]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonaUsuario'
CREATE INDEX [IX_FK_PersonaUsuario]
ON [dbo].[Usuario]
    ([Persona_id]);
GO

-- Creating foreign key on [idRol] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [FK_Usuarios_Roles]
    FOREIGN KEY ([idRol])
    REFERENCES [dbo].[Rol]
        ([idRol])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Usuarios_Roles'
CREATE INDEX [IX_FK_Usuarios_Roles]
ON [dbo].[Usuario]
    ([idRol]);
GO

-- Creating foreign key on [idPersona] in table 'Contacto'
ALTER TABLE [dbo].[Contacto]
ADD CONSTRAINT [FK_Contacto_Persona]
    FOREIGN KEY ([idPersona])
    REFERENCES [dbo].[Persona]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Contacto_Persona'
CREATE INDEX [IX_FK_Contacto_Persona]
ON [dbo].[Contacto]
    ([idPersona]);
GO

-- Creating foreign key on [idEvento] in table 'Contacto'
ALTER TABLE [dbo].[Contacto]
ADD CONSTRAINT [FK_Contacto_Evento]
    FOREIGN KEY ([idEvento])
    REFERENCES [dbo].[Evento]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Contacto_Evento'
CREATE INDEX [IX_FK_Contacto_Evento]
ON [dbo].[Contacto]
    ([idEvento]);
GO

-- Creating foreign key on [idUsuario] in table 'Evento'
ALTER TABLE [dbo].[Evento]
ADD CONSTRAINT [FK_Evento_Usuario]
    FOREIGN KEY ([idUsuario])
    REFERENCES [dbo].[Usuario]
        ([idUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Evento_Usuario'
CREATE INDEX [IX_FK_Evento_Usuario]
ON [dbo].[Evento]
    ([idUsuario]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------