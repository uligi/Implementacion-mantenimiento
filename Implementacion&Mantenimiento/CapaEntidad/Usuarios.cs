using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Usuarios
    {
        public int UsuarioID { get; set; }

        public int PersonaID { get; set; }

        public int RolID { get; set; }

        public string Contrasena { get; set; }

        public bool RestablecerContrasena { get; set; }

        public bool Activo {  get; set; }

        public Personas oPersonas { get; set; }

        public Roles oRoles { get; set; }


    }
}
