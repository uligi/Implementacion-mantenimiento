using CapaEntidad;
using CapaNegocio;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Implementacion_Mantenimiento.Controllers
{
    [Authorize]
    public class TareasController : Controller
    {
        private CN_Tareas negocioTareas = new CN_Tareas();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarTareas()
        {
            List<Tareas> lista = negocioTareas.Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CrearTarea(Tareas obj)
        {
            string mensaje;
            int resultado = negocioTareas.Crear(obj, out mensaje);
            return Json(new { resultado = resultado > 0, mensaje });
        }

        [HttpPost]
        public JsonResult ModificarTarea(Tareas obj)
        {
            string mensaje;
            bool resultado = negocioTareas.Modificar(obj, out mensaje);
            return Json(new { resultado, mensaje });
        }

        [HttpPost]
        public JsonResult EliminarTarea(int tareaID)
        {
            string mensaje;
            bool resultado = negocioTareas.Eliminar(tareaID, out mensaje);
            return Json(new { resultado, mensaje });
        }

        public ActionResult Reportes()
        {
            return View();
        }
    }
}
