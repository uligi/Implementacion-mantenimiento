Create database PatitosSA;
Go
use PatitosSA;
GO

-- Tabla Roles
CREATE TABLE Roles (
    RolID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL UNIQUE,
    Descripcion TEXT
);

-- Tabla Personas
CREATE TABLE Personas (
    PersonaID INT IDENTITY(1,1) PRIMARY KEY,
    NombreCompleto VARCHAR(100) NOT NULL,
    Cedula VARCHAR(20) NOT NULL UNIQUE,
    Correo VARCHAR(100) NOT NULL UNIQUE,
    Telefono VARCHAR(15),
    Direccion TEXT,
    Activo BIT DEFAULT 1 -- 1: Activo, 0: Inactivo
);

-- Tabla Usuarios
CREATE TABLE Usuarios (
    UsuarioID INT IDENTITY(1,1) PRIMARY KEY,
    PersonaID INT NOT NULL,
    RolID INT NOT NULL,
    Contrasena VARCHAR(255) NOT NULL,
	RestablecerContrasena BIT DEFAULT 1,
    Activo BIT DEFAULT 1, -- 1: Activo, 0: Inactivo
    CONSTRAINT FK_Usuarios_Personas FOREIGN KEY (PersonaID) REFERENCES Personas(PersonaID),
    CONSTRAINT FK_Usuarios_Roles FOREIGN KEY (RolID) REFERENCES Roles(RolID)
);

-- Tabla Proyectos
CREATE TABLE Proyectos (
    ProyectoID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion TEXT NOT NULL,
    Estado VARCHAR(20) CHECK (Estado IN ('En desarrollo', 'Mantenimiento', 'Completado')) NOT NULL,
    FechaInicio DATE NOT NULL,
    FechaFin DATE,
    Activo BIT DEFAULT 1 -- 1: Activo, 0: Inactivo
);

-- Tabla Tareas
CREATE TABLE Tareas (
    TareaID INT IDENTITY(1,1) PRIMARY KEY,
    ProyectoID INT NOT NULL,
    Descripcion TEXT NOT NULL,
    Estado VARCHAR(20) CHECK (Estado IN ('Pendiente', 'En progreso', 'Finalizada')) NOT NULL,
    AsignadoA INT NOT NULL,
    FechaInicio DATE NOT NULL,
    FechaFin DATE,
    Activo BIT DEFAULT 1, -- 1: Activo, 0: Inactivo
    CONSTRAINT FK_Tareas_Proyectos FOREIGN KEY (ProyectoID) REFERENCES Proyectos(ProyectoID),
    CONSTRAINT FK_Tareas_Usuarios FOREIGN KEY (AsignadoA) REFERENCES Usuarios(UsuarioID)
);
GO