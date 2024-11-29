use PatitosSA;
GO

Create PROCEDURE spReporteTareas
AS
BEGIN
    SELECT 
        T.TareaID,
        T.Descripcion AS TareaDescripcion,
        T.Estado AS TareaEstado,
        T.FechaInicio AS TareaFechaInicio,
        T.FechaFin AS TareaFechaFin,
        CASE WHEN T.Activo = 1 THEN 'Activo' ELSE 'Inactivo' END AS EstadoActivo,
        P.Nombre AS ProyectoNombre,
        Per.NombreCompleto AS UsuarioAsignado
    FROM 
        Tareas T
    INNER JOIN 
        Proyectos P ON T.ProyectoID = P.ProyectoID
    INNER JOIN 
        Usuarios U ON T.AsignadoA = U.UsuarioID
    INNER JOIN 
        Personas Per ON U.PersonaID = Per.PersonaID
    ORDER BY 
        T.TareaID;
END;
GO

