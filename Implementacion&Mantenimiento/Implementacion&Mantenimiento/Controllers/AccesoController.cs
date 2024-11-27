using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CapaNegocio;

namespace Implementacion_Mantenimiento.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }
        public ActionResult RestablecerContrasena()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(clave))
            {
                ViewBag.Error = "Correo y contraseña no pueden estar vacíos.";
                return View();
            }


            string hashedClave = CN_Recursos.ConvertirSha256(clave);


            Usuarios oUsuario = new CN_Usuario().Listar()
                .Where(u => u.Persona.Correo.DireccionCorreo == correo && u.Contrasena == hashedClave).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos";
                return View();
            }
            else
            {
                if (oUsuario.RestablecerContrasena)
                {
                    TempData["UsuarioID"] = oUsuario.UsuarioID;
                    return RedirectToAction("CambiarClave");
                }
                FormsAuthentication.SetAuthCookie(oUsuario.Personas.Correo, false);
                Session["NombreUsuario"] = oUsuario.Personas.Nombre;
                Session["Rol"] = oUsuario.Roles.Rol;
                Session["UsuarioID"] = oUsuario.UsuarioID;
                ViewBag.Error = null;

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(string UsuarioID, string claveActual, string nuevaClave, string confirmarClave)
        {

            Usuarios oUsuario = new Usuarios();
            oUsuario = new CN_Usuarios().Listar()
                .Where(u => u.UsuarioID == int.Parse(UsuarioID)).FirstOrDefault();
            if (oUsuario.Contrasena != CN_Recursos.ConvertirSha256(claveActual))
            {
                TempData["UsuarioID"] = UsuarioID;
                ViewData["vClave"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (nuevaClave != confirmarClave)
            {
                TempData["UsuarioID"] = UsuarioID;
                ViewData["vClave"] = claveActual;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            ViewData["vClave"] = "";
            nuevaClave = CN_Recursos.ConvertirSha256(nuevaClave);

            string mensaje = string.Empty;
            bool respuesta = new CN_Usuarios().CambiarClave(int.Parse(UsuarioID), nuevaClave, out mensaje);

            if (respuesta)
            {
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                TempData["UsuarioID"] = UsuarioID;
                ViewBag.Error = mensaje;
                return View();
            }

        }


        public ActionResult CerrarSesion()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }
    }
}