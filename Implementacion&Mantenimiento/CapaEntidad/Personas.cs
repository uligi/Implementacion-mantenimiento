﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Personas
    {
        public int PersonaID { get; set; }

        public string NombreCompleto { get; set; }

        public string Cedula { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }    

        public string Direccion { get; set; }

        public bool Activo { get; set; }
    }
}
