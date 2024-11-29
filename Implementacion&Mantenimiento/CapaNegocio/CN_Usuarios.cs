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

        public bool Eliminar(int personaID, int usuarioID, out string Mensaje)
        {
            // Pasar correctamente los dos IDs requeridos al método y el parámetro de salida
            return objCapaDato.Eliminar(personaID, usuarioID, out Mensaje);
        }


        public bool CambiarClave(int usuarioID, string nuevaClave, out string mensaje)
        {
            return objCapaDato.CambiarClave(usuarioID, nuevaClave, out mensaje);
        }


        public bool DesactivarUsuario(int id, out string Mensaje)
        {
            return objCapaDato.DesactivarUsuario(id, out Mensaje);
        }

        public bool RestablecerContrasena(int usuarioID, string correo, out string mensaje)
        {
            string nuevaClave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDato.RestablecerContrasena(usuarioID, CN_Recursos.ConvertirSha256(nuevaClave), out mensaje);

            if (resultado)
            {
                string asunto = "Contraseña Restablecida - Patitos S.A.";
                string mensajeCorreo = $"<p>Tu contraseña ha sido restablecida correctamente.</p><p>Nueva contraseña: <b>{nuevaClave}</b></p>";
                bool envioCorreo = CN_Recursos.EnviarCorreo(correo, asunto, mensajeCorreo);

                if (!envioCorreo)
                {
                    mensaje = "La contraseña fue restablecida, pero no se pudo enviar el correo.";
                }
            }

            return resultado;
        }



    }
}
