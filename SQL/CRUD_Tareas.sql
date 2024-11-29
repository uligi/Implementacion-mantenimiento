
use PatitosSA;
GO

CREATE PROCEDURE spListarTareas
AS
BEGIN
    SELECT T.TareaID, T.ProyectoID, T.Descripcion, T.Estado, T.AsignadoA, 
           T.FechaInicio, T.FechaFin, T.Activo,
           P.Nombre AS NombreProyecto, pp.NombreCompleto AS NombreUsuario
    FROM Tareas T
    JOIN Proyectos P ON T.ProyectoID = P.ProyectoID
    JOIN Usuarios U ON T.AsignadoA = U.UsuarioID
	join Personas PP on pp.PersonaID = U.PersonaID
END;
Go

CREATE PROCEDURE spCrearTarea
    @ProyectoID NVARCHAR(50),
    @Descripcion NVARCHAR(500),
    @Estado NVARCHAR(50),
    @AsignadoA INT,
    @FechaInicio DATETIME,
    @FechaFin DATETIME
AS
BEGIN
    INSERT INTO Tareas (ProyectoID, Descripcion, Estado, AsignadoA, FechaInicio, FechaFin, Activo)
    VALUES (@ProyectoID, @Descripcion, @Estado, @AsignadoA, @FechaInicio, @FechaFin, 1);
    SELECT SCOPE_IDENTITY();
END;
Go

CREATE PROCEDURE spModificarTarea
    @TareaID INT,
    @Descripcion NVARCHAR(500),
    @Estado NVARCHAR(50),
    @AsignadoA INT,
    @FechaInicio DATETIME,
    @FechaFin DATETIME
AS
BEGIN
    UPDATE Tareas
    SET Descripcion = @Descripcion, Estado = @Estado, AsignadoA = @AsignadoA, 
        FechaInicio = @FechaInicio, FechaFin = @FechaFin
    WHERE TareaID = @TareaID;
END;
Go

CREATE PROCEDURE spEliminarTarea
    @TareaID INT
AS
BEGIN
    DELETE FROM Tareas WHERE TareaID = @TareaID;
END;
go