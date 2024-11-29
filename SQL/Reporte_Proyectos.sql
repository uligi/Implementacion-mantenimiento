use PatitosSA;
GO

CREATE PROCEDURE spReporteProyectos
AS
BEGIN
    SELECT 
        ProyectoID,
        Nombre,
        Descripcion,
        Estado,
        FORMAT(FechaInicio, 'dd/MM/yyyy') AS FechaInicio,
        FORMAT(FechaFin, 'dd/MM/yyyy') AS FechaFin,
        CASE 
            WHEN Activo = 1 THEN 'Activo'
            ELSE 'Inactivo'
        END AS EstadoActivo
    FROM 
        Proyectos
    ORDER BY 
        FechaInicio DESC;
END;
GO
