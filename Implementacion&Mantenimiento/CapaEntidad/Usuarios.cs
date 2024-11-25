using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    internal class Usuarios
    {
        public int UsuarioID { get; set; }

        public int PersonaID { get; set; }

        public int RolId { get; set; }

        public string Contrasena { get; set; }

        public bool RestablecerContrasena { get; set; }

        public bool Activo {  get; set; }

        public Personas oPersonas { get; set; }

        public Roles oRoles { get; set; }


    }
}
