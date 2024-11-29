using CapaEntidad;
using CapaNegocio;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Implementacion_Mantenimiento.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private CN_Roles negocioRoles = new CN_Roles();

        // Vista de la gestión de roles
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarRoles()
        {
            List<Roles> lista = negocioRoles.Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CrearRol(Roles obj)
        {
            string mensaje;
            int resultado = negocioRoles.Crear(obj, out mensaje);
            return Json(new { resultado = resultado > 0, mensaje });
        }

        [HttpPost]
        public JsonResult ModificarRol(Roles obj)
        {
            string mensaje;
            bool resultado = negocioRoles.Modificar(obj, out mensaje);
            return Json(new { resultado, mensaje });
        }

        [HttpPost]
        public JsonResult EliminarRol(int rolID)
        {
            string mensaje;
            bool resultado = negocioRoles.Eliminar(rolID, out mensaje);
            return Json(new { resultado, mensaje });
        }
    }
}
