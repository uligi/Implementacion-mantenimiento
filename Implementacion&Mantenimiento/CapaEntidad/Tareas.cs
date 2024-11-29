using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Tareas
    {

        public int TareaID { get; set; }

        public string ProyectoID { get; set; }

        public string Descripcion { get; set; }

        public string Estado { get; set; }

        public int AsignadoA { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin {  get; set; }

        public bool Activo {  get; set; }

        public Proyectos oProyectos { get; set; }

        public Usuarios oUsuarios { get; set; }

    }
}
