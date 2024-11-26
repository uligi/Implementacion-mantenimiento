using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuarios> Listar()
        {

            return objCapaDato.Listar();
        }

        public int Registrar(Usuarios obj, out string Mensaje)
        {
            if (obj.oPersonas == null || string.IsNullOrWhiteSpace(obj.oPersonas.NombreCompleto))
            {
                Mensaje = "El nombre completo de la persona es obligatorio.";
                return 0;
            }

            if (string.IsNullOrWhiteSpace(obj.oPersonas.Cedula))
            {
                Mensaje = "La cédula es obligatoria.";
                return 0;
            }

            return objCapaDato.Registrar(obj, out Mensaje);
        }

    }
}
