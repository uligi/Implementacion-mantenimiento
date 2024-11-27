using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using CapaEntidad;
using CapaNegocio;

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

        // Validar Credenciales de Login
        [HttpPost]
        public ActionResult Login(string correo, string clave)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(clave))
            {
                ViewBag.Error = "Correo y contraseña no pueden estar vacíos.";
                return View();
            }

            string hashedClave = CN_Recursos.ConvertirSha256(clave);
            Usuarios oUsuario = usuarioNegocio.Listar()
                .FirstOrDefault(u => u.oPersonas.Correo == correo && u.Contrasena == hashedClave);

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos.";
                return View();
            }
            else if (oUsuario.RestablecerContrasena)
            {
                TempData["UsuarioID"] = oUsuario.UsuarioID;
                return RedirectToAction("RestablecerContrasena");
            }

            FormsAuthentication.SetAuthCookie(oUsuario.oPersonas.Correo, false);
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
        public ActionResult Registrar(string nombreCompleto, string correo, string cedula)
        {
            Usuarios nuevoUsuario = new Usuarios
            {
                oPersonas = new Personas
                {
                    NombreCompleto = nombreCompleto,
                    Correo = correo,
                    Cedula = cedula
                },
                RolID = 2 // Rol de usuario básico
            };

            string mensaje;
            int resultado = usuarioNegocio.Registrar(nuevoUsuario, out mensaje);

            if (resultado > 0)
            {
                ViewBag.Success = "Usuario registrado exitosamente. Revisa tu correo para acceder.";
                return View();
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
            return View();
        }

        // Restablecer Contraseña
        [HttpPost]
        public ActionResult RestablecerContrasena(int usuarioID, string nuevaClave, string confirmarClave)
        {
            if (nuevaClave != confirmarClave)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                return View();
            }

            string mensaje;
            bool resultado = usuarioNegocio.CambiarClave(usuarioID, CN_Recursos.ConvertirSha256(nuevaClave), out mensaje);

            if (resultado)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }

        // Cerrar Sesión
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
