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
            string clave = CN_Recursos.GenerarClave(); // Generar contraseña aleatoria
            obj.Contrasena = CN_Recursos.ConvertirSha256(clave); // Encriptar contraseña

            int resultado = objCapaDato.Registrar(obj, out Mensaje);

            if (resultado > 0)
            {
                // Enviar correo con la contraseña
                string asunto = "Registro exitoso - Patitos S.A.";
                string mensajeCorreo = $"Bienvenido a Patitos S.A.\nTu contraseña temporal es: {clave}. Por favor cámbiala al iniciar sesión.";
                CN_Recursos.EnviarCorreo(obj.oPersonas.Correo, asunto, mensajeCorreo);
            }

            return resultado;
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
