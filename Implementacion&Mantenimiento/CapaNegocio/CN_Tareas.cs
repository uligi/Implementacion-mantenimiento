using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Tareas
    {
        private CD_Tareas objCapaDatos = new CD_Tareas();

        public List<Tareas> Listar(int? tareaID = null)
        {
            return objCapaDatos.Listar(tareaID);
        }

        public int Crear(Tareas obj, out string mensaje)
        {
            if (string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                mensaje = "La descripción de la tarea no puede estar vacía.";
                return 0;
            }

            return objCapaDatos.Crear(obj, out mensaje);
        }

        public bool Modificar(Tareas obj, out string mensaje)
        {
            if (obj.TareaID <= 0)
            {
                mensaje = "El ID de la tarea no es válido.";
                return false;
            }

            return objCapaDatos.Modificar(obj, out mensaje);
        }

        public bool Eliminar(int tareaID, out string mensaje)
        {
            if (tareaID <= 0)
            {
                mensaje = "El ID de la tarea no es válido.";
                return false;
            }

            return objCapaDatos.Eliminar(tareaID, out mensaje);
        }

        private CD_Tareas cd_tareas = new CD_Tareas();

        public List<Tareas> ReporteTareas()
        {
            return cd_tareas.ReporteTareas();
        }

    }
}
