use PatitosSA;
GO

create PROCEDURE spListarUsuariosYPersonas
    @Activo BIT = NULL -- Opcional: Filtrar por estado activo/inactivo
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        U.UsuarioID,
        P.PersonaID,
        P.NombreCompleto,
        P.Cedula,
        P.Correo,
        P.Telefono,
        P.Direccion,
        R.Nombre AS Rol,
        U.Activo
    FROM Usuarios U
    JOIN Personas P ON U.PersonaID = P.PersonaID
    JOIN Roles R ON U.RolID = R.RolID
    WHERE (@Activo IS NULL OR U.Activo = @Activo);
END;
GO


create PROCEDURE spCrearPersonaYUsuario
    @NombreCompleto NVARCHAR(100),
    @Cedula NVARCHAR(20),
    @Correo NVARCHAR(100),
    @Telefono NVARCHAR(15) = NULL,
    @Direccion TEXT = NULL,
    @RolID INT,
    @Contrasena NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PersonaID INT;

    -- Insertar Persona
    INSERT INTO Personas (NombreCompleto, Cedula, Correo, Telefono, Direccion, Activo)
    VALUES (@NombreCompleto, @Cedula, @Correo, @Telefono, @Direccion, 1);

    SET @PersonaID = SCOPE_IDENTITY(); -- Obtener el ID de la Persona recién creada

    -- Insertar Usuario relacionado
    INSERT INTO Usuarios (PersonaID, RolID, Contrasena, Activo)
    VALUES (@PersonaID, @RolID, @Contrasena, 1);

    SELECT @PersonaID AS PersonaID, SCOPE_IDENTITY() AS UsuarioID; -- Devolver ambos IDs
END;
GO


CREATE PROCEDURE spModificarPersonaYUsuario
    @PersonaID INT,
    @UsuarioID INT,
    @NombreCompleto NVARCHAR(100),
    @Cedula NVARCHAR(20),
    @Correo NVARCHAR(100),
    @Telefono NVARCHAR(15) = NULL,
    @Direccion TEXT = NULL,
    @RolID INT,
    @Contrasena NVARCHAR(255),
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;

    -- Actualizar Persona
    UPDATE Personas
    SET 
        NombreCompleto = @NombreCompleto,
        Cedula = @Cedula,
        Correo = @Correo,
        Telefono = @Telefono,
        Direccion = @Direccion
    WHERE PersonaID = @PersonaID;

    -- Actualizar Usuario
    UPDATE Usuarios
    SET 
        RolID = @RolID,
        Contrasena = @Contrasena,
        Activo = @Activo
    WHERE UsuarioID = @UsuarioID;

    SELECT 'Persona y Usuario actualizados correctamente' AS Mensaje;
END;
GO
CREATE PROCEDURE spEliminarPersonaYUsuario
    @PersonaID INT,
    @UsuarioID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Eliminar lógicamente la Persona
    UPDATE Personas
    SET Activo = 0
    WHERE PersonaID = @PersonaID;

    -- Eliminar lógicamente el Usuario
    UPDATE Usuarios
    SET Activo = 0
    WHERE UsuarioID = @UsuarioID;

    SELECT 'Persona y Usuario eliminados lógicamente' AS Mensaje;
END;
GO
