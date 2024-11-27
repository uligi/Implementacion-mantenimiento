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
            string clave = CN_Recursos.GenerarClave();
            obj.Contrasena = CN_Recursos.ConvertirSha256(clave); // Hash password
            int resultado = objCapaDato.Registrar(obj, out Mensaje);

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

        public bool Editar(Usuarios obj, out string Mensaje)
        {
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

        public bool CambiarClave(int UsuarioID, string nuevaClave, out string Mensaje)
        {
            return objCapaDato.CambiarClave(UsuarioID, nuevaClave, out Mensaje);
        }

        public bool DesactivarUsuario(int id, out string Mensaje)
        {
            return objCapaDato.DesactivarUsuario(id, out Mensaje);
        }

        public bool RestablecerContrasena(int UsuarioID, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevaClave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDato.RestablecerContrasena(UsuarioID, CN_Recursos.ConvertirSha256(nuevaClave), out Mensaje);

            if (resultado)
            {
                string asunto = "Contraseña Reestablecida";
                string mensaje_correo = "<h3>Su cuenta fue reestablecida correctamente</h3><br><p>Su contraseña para acceder ahora es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaClave);

                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                Mensaje = "No se pudo reestablecer la contrasena";
                return false;

            }


        }

    }
}
