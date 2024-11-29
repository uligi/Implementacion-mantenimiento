-- Listar Proyectos
use PatitosSA;
GO

CREATE PROCEDURE spListarProyectos
AS
BEGIN
    SELECT ProyectoID, Nombre, Descripcion, Estado, FechaInicio, FechaFin, Activo
    FROM Proyectos
    WHERE Activo = 1;
END;
GO

-- Crear Proyecto
CREATE PROCEDURE spCrearProyecto
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(500),
    @Estado NVARCHAR(50),
    @FechaInicio DATETIME,
    @FechaFin DATETIME
AS
BEGIN
    INSERT INTO Proyectos (Nombre, Descripcion, Estado, FechaInicio, FechaFin, Activo)
    VALUES (@Nombre, @Descripcion, @Estado, @FechaInicio, @FechaFin, 1);
    SELECT SCOPE_IDENTITY();
END;
GO

-- Modificar Proyecto
CREATE PROCEDURE spModificarProyecto
    @ProyectoID INT,
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(500),
    @Estado NVARCHAR(50),
    @FechaInicio DATETIME,
    @FechaFin DATETIME
AS
BEGIN
    UPDATE Proyectos
    SET Nombre = @Nombre, Descripcion = @Descripcion, Estado = @Estado,
        FechaInicio = @FechaInicio, FechaFin = @FechaFin
    WHERE ProyectoID = @ProyectoID AND Activo = 1;
END;
GO

-- Eliminar Proyecto
CREATE PROCEDURE spEliminarProyecto
    @ProyectoID INT
AS
BEGIN
    UPDATE Proyectos
    SET Activo = 0
    WHERE ProyectoID = @ProyectoID;
END;
GO
ALTER TABLE Proyectos ADD CONSTRAINT CK_Proyectos_Estado CHECK (Estado IN ('Iniciado', 'En Proceso', 'Finalizado'));
go