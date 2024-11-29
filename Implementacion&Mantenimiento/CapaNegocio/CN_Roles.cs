using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Roles
    {
        private CD_Roles objCapaDatos = new CD_Roles();

        public List<Roles> Listar(int? rolID = null)
        {
            return objCapaDatos.Listar(rolID);
        }

        public int Crear(Roles obj, out string mensaje)
        {
            if (string.IsNullOrWhiteSpace(obj.Nombre))
            {
                mensaje = "El nombre del rol no puede estar vacío.";
                return 0;
            }

            return objCapaDatos.Crear(obj, out mensaje);
        }

        public bool Modificar(Roles obj, out string mensaje)
        {
            if (obj.RolID <= 0)
            {
                mensaje = "El ID del rol no es válido.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(obj.Nombre))
            {
                mensaje = "El nombre del rol no puede estar vacío.";
                return false;
            }

            return objCapaDatos.Modificar(obj, out mensaje);
        }

        public bool Eliminar(int rolID, out string mensaje)
        {
            if (rolID <= 0)
            {
                mensaje = "El ID del rol no es válido.";
                return false;
            }

            return objCapaDatos.Eliminar(rolID, out mensaje);
        }
    }
}
