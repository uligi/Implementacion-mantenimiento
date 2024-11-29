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



    }
}