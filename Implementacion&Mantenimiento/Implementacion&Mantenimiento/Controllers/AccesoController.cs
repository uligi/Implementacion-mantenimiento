using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using CapaEntidad;
using CapaNegocio;
using System.Web.Security;

namespace Implementacion_Mantenimiento.Controllers
{
    public class AccesoController : Controller
    {
        private CN_Usuarios usuarioNegocio = new CN_Usuarios();

        // Página de Login
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string correo, string clave)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(clave))
            {
                ViewBag.Error = "Correo y contraseña no pueden estar vacíos.";
                return View(); // Retorna la misma vista con el mensaje de error
            }

            // Obtener la lista de usuarios desde la capa de negocio
            var usuarios = usuarioNegocio.Listar();

            // Validar credenciales
            string hashedClave = CN_Recursos.ConvertirSha256(clave);
            Usuarios oUsuario = usuarios.FirstOrDefault(u => u.oPersonas.Correo == correo && u.Contrasena == hashedClave);

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos.";
                return View(); // Retorna la misma vista con el mensaje de error
            }

            // Redirigir a cambiar clave si es necesario
            if (oUsuario.RestablecerContrasena)
            {
                TempData["UsuarioID"] = oUsuario.UsuarioID;
                return RedirectToAction("CambiarClave", "Acceso", new { usuarioID = oUsuario.UsuarioID });
            }

            // Autenticación exitosa
            FormsAuthentication.SetAuthCookie(oUsuario.oPersonas.Correo, false);
            Session["UsuarioID"] = oUsuario.UsuarioID;
            Session["NombreUsuario"] = oUsuario.oPersonas.NombreCompleto;
            Session["Rol"] = oUsuario.oRoles.Nombre;

            return RedirectToAction("Index", "Home");
        }




        // Página de Registro
        public ActionResult Registrar()
        {
            return View();
        }

        // Registrar Usuario
        [HttpPost]
        public ActionResult Registrar(string nombreCompleto, string correo, string cedula, string telefono, string direccion)
        {
            Usuarios nuevoUsuario = new Usuarios
            {
                oPersonas = new Personas
                {
                    NombreCompleto = nombreCompleto,
                    Correo = correo,
                    Cedula = cedula,
                    Telefono = string.IsNullOrEmpty(telefono) ? null : telefono,
                    Direccion = string.IsNullOrEmpty(direccion) ? null : direccion
                },
                RolID = 2 // Asignar automáticamente el Rol de usuario básico
            };

            string mensaje;
            int resultado = usuarioNegocio.Registrar(nuevoUsuario, out mensaje);

            if (resultado > 0)
            {
                TempData["SuccessMessage"] = "Registro exitoso. Revisa tu correo para obtener tu contraseña temporal.";
                return RedirectToAction("Login","Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }



        // Página de Restablecer Contraseña
        public ActionResult RestablecerContrasena()
        {
            ViewBag.UsuarioID = TempData["UsuarioID"];
            return View();
        }

        // Restablecer Contraseña

        [HttpPost]
        public ActionResult RestablecerContrasena(int usuarioID, string correo)
        {
            string mensaje;
            Usuarios usuario = usuarioNegocio.Listar().FirstOrDefault(u => u.UsuarioID == usuarioID);

            if (usuario == null)
            {
                ViewBag.Error = "Usuario no encontrado.";
                return View();
            }

            bool resultado = usuarioNegocio.RestablecerContrasena(usuarioID, correo, out mensaje);

            if (resultado)
            {
                TempData["SuccessMessage"] = "Se ha enviado una nueva contraseña a tu correo.";
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }



        public ActionResult CambiarClave(int usuarioID)
        {
            ViewBag.UsuarioID = usuarioID; // Pasar el ID al ViewBag para la vista
            return View();
        }



        [HttpPost]
        public ActionResult CambiarClave(int usuarioID, string nuevaClave, string confirmarClave)
        {
            if (string.IsNullOrEmpty(nuevaClave) || string.IsNullOrEmpty(confirmarClave))
            {
                ViewBag.Error = "Todos los campos son obligatorios.";
                ViewBag.UsuarioID = usuarioID;
                return View();
            }

            if (nuevaClave != confirmarClave)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                ViewBag.UsuarioID = usuarioID;
                return View();
            }

            string mensaje;
            bool resultado = usuarioNegocio.CambiarClave(usuarioID, CN_Recursos.ConvertirSha256(nuevaClave), out mensaje);

            if (resultado)
            {
                TempData["SuccessMessage"] = "Contraseña cambiada con éxito.";
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Error = mensaje;
                ViewBag.UsuarioID = usuarioID;
                return View();
            }
        }

        [HttpGet]
        public JsonResult ObtenerUsuarioID(string correo)
        {
            // Buscar usuario por correo
            Usuarios usuario = usuarioNegocio.Listar().FirstOrDefault(u => u.oPersonas.Correo == correo);

            if (usuario != null)
            {
                return Json(new { usuarioID = usuario.UsuarioID }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { usuarioID = 0 }, JsonRequestBehavior.AllowGet);
            }
        }



        // Cerrar Sesión
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Acceso");
        }
    }
}
