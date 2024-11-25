using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    internal class Proyectos
    {
        public int ProyectoID { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Estado { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin {  get; set; }

        public bool Activo { get; set; }


    }
}
