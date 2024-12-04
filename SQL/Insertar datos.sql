use PatitosSA;
GO  


-- Limpia la tabla para evitar conflictos en caso de registros previos
DELETE FROM Roles;
GO
-- Insertar registros utilizando el procedimiento almacenado
EXEC spCrearRol @Nombre = 'Administrador', @Descripcion = 'Rol con acceso completo a todas las funcionalidades del sistema';
GO

EXEC spCrearRol @Nombre = 'Usuario', @Descripcion = 'Rol con acceso limitado a funcionalidades básicas del sistema';
GO

EXEC spCrearRol @Nombre = 'Moderador', @Descripcion = 'Rol encargado de gestionar y moderar contenido del sistema';
GO

EXEC spCrearRol @Nombre = 'Invitado', @Descripcion = 'Rol con acceso temporal y limitado para visualización de datos';
GO

DECLARE @HashedPassword NVARCHAR(255);
SET @HashedPassword = LOWER(CONVERT(NVARCHAR(255), HASHBYTES('SHA2_256', 'contrasena123'), 2));
EXEC spCrearPersonaYUsuario  
    @NombreCompleto = 'Juan Pérez',
    @Cedula = '12345678',
    @Correo = 'juan.perez@example.com',
    @Telefono = '5551234567',
    @Direccion = 'Calle Principal 123',
    @RolID = 1,
    @Contrasena = @HashedPassword;
GO

DECLARE @HashedPassword NVARCHAR(255);
SET @HashedPassword = LOWER(CONVERT(NVARCHAR(255), HASHBYTES('SHA2_256', 'claveSegura456'), 2));
EXEC spCrearPersonaYUsuario 
    @NombreCompleto = 'María López',
    @Cedula = '87654321',
    @Correo = 'maria.lopez@example.com',
    @Telefono = '5557654321',
    @Direccion = 'Avenida Secundaria 456',
    @RolID = 2,
    @Contrasena = @HashedPassword;
GO

DECLARE @HashedPassword NVARCHAR(255);
SET @HashedPassword = LOWER(CONVERT(NVARCHAR(255), HASHBYTES('SHA2_256', 'password789'), 2));
EXEC spCrearPersonaYUsuario 
    @NombreCompleto = 'Carlos Sánchez',
    @Cedula = '11223344',
    @Correo = 'carlos.sanchez@example.com',
    @Telefono = '5551122334',
    @Direccion = 'Zona Norte 789',
    @RolID = 3,
    @Contrasena = @HashedPassword;
GO

DECLARE @HashedPassword NVARCHAR(255);
SET @HashedPassword = LOWER(CONVERT(NVARCHAR(255), HASHBYTES('SHA2_256', 'adminPass321'), 2));
EXEC spCrearPersonaYUsuario 
    @NombreCompleto = 'Ana Morales',
    @Cedula = '44332211',
    @Correo = 'ana.morales@example.com',
    @Telefono = '5554433221',
    @Direccion = 'Sector Este 321',
    @RolID = 4,
    @Contrasena = @HashedPassword;
GO


-- Limpieza de la tabla para evitar conflictos (solo para pruebas)
DELETE FROM Proyectos;
GO


-- Insertar registros utilizando el procedimiento almacenado
EXEC spCrearProyecto 
    @Nombre = 'Sistema de Gestión de Inventarios',
    @Descripcion = 'Desarrollo de un sistema para gestionar inventarios en tiempo real.',
    @Estado = 'Iniciado',
    @FechaInicio = '2024-01-15',
    @FechaFin = '2024-07-30';
GO


EXEC spCrearProyecto 
    @Nombre = 'Actualización de Infraestructura de Red',
    @Descripcion = 'Mejora de la infraestructura de red para soportar mayor tráfico.',
    @Estado = 'Finalizado',
    @FechaInicio = '2023-10-01',
    @FechaFin = '2024-03-01';
GO


EXEC spCrearProyecto 
    @Nombre = 'Implementación de CRM',
    @Descripcion = 'Despliegue de un sistema CRM para la gestión de clientes.',
    @Estado = 'En Progreso',
    @FechaInicio = '2024-02-01',
    @FechaFin = '2024-12-31';
GO


EXEC spCrearProyecto 
    @Nombre = 'Optimización del Proceso de Nómina',
    @Descripcion = 'Automatización de cálculos y generación de reportes de nómina.',
    @Estado = 'Iniciado',
    @FechaInicio = '2023-11-15',
    @FechaFin = '2024-05-15';
GO


EXEC spCrearProyecto 
    @Nombre = 'Aplicación Móvil para Comercio Electrónico',
    @Descripcion = 'Diseño y desarrollo de una app para e-commerce.',
    @Estado = 'En Progreso',
    @FechaInicio = '2024-03-01',
    @FechaFin = '2024-09-01';
GO


EXEC spCrearProyecto 
    @Nombre = 'Migración de ERP a la Nube',
    @Descripcion = 'Migración completa de los datos y funcionalidades del ERP a la nube.',
    @Estado = 'Finalizado',
    @FechaInicio = '2023-04-01',
    @FechaFin = '2023-12-15';
GO


-- Limpieza de la tabla para evitar conflictos (solo para pruebas)
DELETE FROM Tareas;
GO

-- Insertar tareas utilizando el procedimiento almacenado
EXEC spCrearTarea 
    @ProyectoID = 1, -- Referencia a un Proyecto existente
    @Descripcion = 'Diseño inicial del sistema.',
    @Estado = 'En progreso',
    @AsignadoA = 1, -- Referencia a un Usuario existente
    @FechaInicio = '2024-01-01',
    @FechaFin = '2024-01-15';
GO

EXEC spCrearTarea 
    @ProyectoID = 1,
    @Descripcion = 'Revisión de requerimientos con el cliente.',
    @Estado = 'Pendiente',
    @AsignadoA = 3,
    @FechaInicio = '2024-01-10',
    @FechaFin = '2024-01-20';
GO

EXEC spCrearTarea 
    @ProyectoID = 2,
    @Descripcion = 'Pruebas de conectividad en la red.',
    @Estado = 'En progreso',
    @AsignadoA = 4,
    @FechaInicio = '2024-01-05',
    @FechaFin = '2024-01-25';
GO

EXEC spCrearTarea 
    @ProyectoID = 2,
    @Descripcion = 'Configuración inicial del hardware.',
    @Estado = 'Pendiente',
    @AsignadoA = 1,
    @FechaInicio = '2024-01-15',
    @FechaFin = '2024-01-30';
GO

EXEC spCrearTarea 
    @ProyectoID = 3,
    @Descripcion = 'Integración del sistema CRM con la base de datos.',
    @Estado = 'En progreso',
    @AsignadoA = 5,
    @FechaInicio = '2024-02-01',
    @FechaFin = '2024-02-20';
GO

EXEC spCrearTarea 
    @ProyectoID = 3,
    @Descripcion = 'Creación de vistas para el módulo CRM.',
    @Estado = 'Pendiente',
    @AsignadoA = 2,
    @FechaInicio = '2024-02-05',
    @FechaFin = '2024-02-25';
GO

EXEC spCrearTarea 
    @ProyectoID = 4,
    @Descripcion = 'Automatización de cálculos de nómina.',
    @Estado = 'Completada',
    @AsignadoA = 3,
    @FechaInicio = '2023-12-15',
    @FechaFin = '2024-01-05';
GO

EXEC spCrearTarea 
    @ProyectoID = 4,
    @Descripcion = 'Pruebas del módulo de nómina.',
    @Estado = 'En progreso',
    @AsignadoA = 5,
    @FechaInicio = '2024-01-06',
    @FechaFin = '2024-01-20';
GO

EXEC spCrearTarea 
    @ProyectoID = 5,
    @Descripcion = 'Desarrollo de la interfaz móvil para comercio electrónico.',
    @Estado = 'En progreso',
    @AsignadoA = 2,
    @FechaInicio = '2024-03-01',
    @FechaFin = '2024-03-31';
GO

EXEC spCrearTarea 
    @ProyectoID = 5,
    @Descripcion = 'Pruebas funcionales de la aplicación móvil.',
    @Estado = 'Pendiente',
    @AsignadoA = 3,
    @FechaInicio = '2024-04-01',
    @FechaFin = '2024-04-15';
GO
