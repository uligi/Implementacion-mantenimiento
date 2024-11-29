using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Proyectos
    {
        private CD_Proyectos objCapaDatos = new CD_Proyectos();

        public List<Proyectos> Listar(int? proyectoID = null)
        {
            return objCapaDatos.Listar(proyectoID);
        }

        public int Crear(Proyectos obj, out string mensaje)
        {
            if (string.IsNullOrWhiteSpace(obj.Nombre))
            {
                mensaje = "El nombre del proyecto no puede estar vacío.";
                return 0;
            }

            return objCapaDatos.Crear(obj, out mensaje);
        }

        public bool Modificar(Proyectos obj, out string mensaje)
        {
            if (obj.ProyectoID <= 0)
            {
                mensaje = "El ID del proyecto no es válido.";
                return false;
            }

            return objCapaDatos.Modificar(obj, out mensaje);
        }

        public bool Eliminar(int proyectoID, out string mensaje)
        {
            if (proyectoID <= 0)
            {
                mensaje = "El ID del proyecto no es válido.";
                return false;
            }

            return objCapaDatos.Eliminar(proyectoID, out mensaje);
        }


        public List<Proyectos> ObtenerReporteProyectos()
        {
            return objCapaDatos.ReporteProyectos();
        }
    }
}
