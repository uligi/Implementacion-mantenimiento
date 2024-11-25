using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Roles
    {
        private CD_Roles objCapaDatos = new CD_Roles();

        // Método para listar roles
        public List<Roles> Listar(int? rolID = null)
        {
            return objCapaDatos.Listar(rolID);
        }

        // Método para crear un rol
        public int Crear(Roles obj, out string Mensaje)
        {
            if (string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del rol no puede estar vacío.";
                return 0;
            }

            return objCapaDatos.Crear(obj, out Mensaje);
        }

        // Método para modificar un rol
        public bool Modificar(Roles obj, out string Mensaje)
        {
            if (obj.RolID <= 0)
            {
                Mensaje = "El ID del rol no es válido.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del rol no puede estar vacío.";
                return false;
            }

            return objCapaDatos.Modificar(obj, out Mensaje);
        }

        // Método para eliminar un rol
        public bool Eliminar(int rolID, out string Mensaje)
        {
            if (rolID <= 0)
            {
                Mensaje = "El ID del rol no es válido.";
                return false;
            }

            return objCapaDatos.Eliminar(rolID, out Mensaje);
        }
    }
}
