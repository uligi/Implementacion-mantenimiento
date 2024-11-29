using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaDatos;
using CapaNegocio;

namespace Implementacion_Mantenimiento.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            var usuarios = new CN_Usuarios().Listar();

            // DataTables espera un objeto con la propiedad "data"
            return Json(new { data = usuarios }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarUsuario(Usuarios usuario)
        {
            string mensaje = string.Empty;

            if (usuario.UsuarioID == 0) // Nuevo usuario
            {
                int resultado = new CN_Usuarios().Registrar(usuario, out mensaje);

                return Json(new { resultado = resultado > 0, mensaje });
            }
            else // Editar usuario existente
            {
                bool resultado = new CN_Usuarios().Editar(usuario, out mensaje);

                return Json(new { resultado, mensaje });
            }
        }

        [HttpPost]
        public JsonResult EditarUsuario(Usuarios usuario)
        {
            string mensaje = string.Empty;

            // Llama al método Editar de la capa de negocio
            bool resultado = new CN_Usuarios().Editar(usuario, out mensaje);

            return Json(new { resultado, mensaje });
        }


        [HttpPost]
        public JsonResult EliminarUsuario(int usuarioID, int personaID)
        {
            string mensaje = string.Empty;

            bool resultado = new CN_Usuarios().Eliminar(personaID, usuarioID, out mensaje);

            return Json(new { resultado, mensaje });
        }



    }
}