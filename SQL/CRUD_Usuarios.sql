use PatitosSA;
GO

CREATE PROCEDURE spGestionarUsuario
    @Operacion NVARCHAR(10), -- CREATE, READ, UPDATE, DELETE
    @UsuarioID INT = NULL, -- Obligatorio para UPDATE y DELETE
    @PersonaID INT = NULL, -- Obligatorio para CREATE
    @RolID INT = NULL, -- Obligatorio para CREATE y UPDATE
    @Contrasena NVARCHAR(255) = NULL, -- Obligatorio para CREATE y UPDATE
    @Activo BIT = NULL -- Obligatorio para UPDATE y DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF @Operacion = 'CREATE'
    BEGIN
        IF @PersonaID IS NULL OR @RolID IS NULL OR @Contrasena IS NULL
        BEGIN
            THROW 50000, 'Faltan parámetros obligatorios para la creación.', 1;
        END

        INSERT INTO Usuarios (PersonaID, RolID, Contrasena, Activo)
        VALUES (@PersonaID, @RolID, @Contrasena, 1);

        SELECT SCOPE_IDENTITY() AS UsuarioID; -- Devuelve el ID del nuevo usuario
    END

    ELSE IF @Operacion = 'READ'
    BEGIN
        IF @UsuarioID IS NULL
        BEGIN
            -- Leer todos los usuarios activos
            SELECT 
                U.UsuarioID,
                P.NombreCompleto,
                P.Correo,
                R.Nombre AS Rol,
                U.Activo
            FROM Usuarios U
            JOIN Personas P ON U.PersonaID = P.PersonaID
            JOIN Roles R ON U.RolID = R.RolID
            WHERE U.Activo = 1;
        END
        ELSE
        BEGIN
            -- Leer un usuario específico
            SELECT 
                U.UsuarioID,
                P.NombreCompleto,
                P.Correo,
                R.Nombre AS Rol,
                U.Activo
            FROM Usuarios U
            JOIN Personas P ON U.PersonaID = P.PersonaID
            JOIN Roles R ON U.RolID = R.RolID
            WHERE U.UsuarioID = @UsuarioID;
        END
    END

    ELSE IF @Operacion = 'UPDATE'
    BEGIN
        IF @UsuarioID IS NULL OR @RolID IS NULL OR @Contrasena IS NULL OR @Activo IS NULL
        BEGIN
            THROW 50000, 'Faltan parámetros obligatorios para la actualización.', 1;
        END

        UPDATE Usuarios
        SET 
            RolID = @RolID,
            Contrasena = @Contrasena,
            Activo = @Activo
        WHERE UsuarioID = @UsuarioID;

        SELECT 'Usuario actualizado correctamente' AS Mensaje;
    END

    ELSE IF @Operacion = 'DELETE'
    BEGIN
        IF @UsuarioID IS NULL OR @Activo IS NULL
        BEGIN
            THROW 50000, 'Faltan parámetros obligatorios para la eliminación lógica.', 1;
        END

        UPDATE Usuarios
        SET Activo = @Activo
        WHERE UsuarioID = @UsuarioID;

        SELECT 'Usuario eliminado lógicamente' AS Mensaje;
    END

    ELSE
    BEGIN
        THROW 50000, 'Operación no válida. Use CREATE, READ, UPDATE o DELETE.', 1;
    END
END;
