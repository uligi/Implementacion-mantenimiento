use PatitosSA;
GO

CREATE PROCEDURE spListarRoles
    @RolID INT = NULL -- Opcional: Si se especifica, filtra por RolID
AS
BEGIN
    SET NOCOUNT ON;

    IF @RolID IS NULL
    BEGIN
        -- Listar todos los roles
        SELECT 
            RolID,
            Nombre,
            Descripcion
        FROM Roles;
    END
    ELSE
    BEGIN
        -- Listar un rol específico
        SELECT 
            RolID,
            Nombre,
            Descripcion
        FROM Roles
        WHERE RolID = @RolID;
    END
END;
GO

CREATE PROCEDURE spCrearRol
    @Nombre NVARCHAR(50),
    @Descripcion TEXT = NULL -- Opcional
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el nombre del rol ya existe
    IF EXISTS (SELECT 1 FROM Roles WHERE Nombre = @Nombre)
    BEGIN
        THROW 50000, 'El rol ya existe.', 1;
    END

    -- Insertar el nuevo rol
    INSERT INTO Roles (Nombre, Descripcion)
    VALUES (@Nombre, @Descripcion);

    SELECT SCOPE_IDENTITY() AS RolID; -- Devolver el ID del nuevo rol
END;
GO

CREATE PROCEDURE spModificarRol
    @RolID INT,
    @Nombre NVARCHAR(50),
    @Descripcion TEXT = NULL -- Opcional
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el RolID existe
    IF NOT EXISTS (SELECT 1 FROM Roles WHERE RolID = @RolID)
    BEGIN
        THROW 50000, 'El rol especificado no existe.', 1;
    END

    -- Actualizar el rol
    UPDATE Roles
    SET 
        Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE RolID = @RolID;

    SELECT 'Rol actualizado correctamente' AS Mensaje;
END;
GO

CREATE PROCEDURE spEliminarRol
    @RolID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el RolID existe
    IF NOT EXISTS (SELECT 1 FROM Roles WHERE RolID = @RolID)
    BEGIN
        THROW 50000, 'El rol especificado no existe.', 1;
    END

    -- Verificar si el rol está en uso por algún usuario
    IF EXISTS (SELECT 1 FROM Usuarios WHERE RolID = @RolID)
    BEGIN
        THROW 50000, 'No se puede eliminar el rol porque está siendo utilizado por usuarios.', 1;
    END

    -- Eliminar el rol
    DELETE FROM Roles
    WHERE RolID = @RolID;

    SELECT 'Rol eliminado correctamente' AS Mensaje;
END;
GO
