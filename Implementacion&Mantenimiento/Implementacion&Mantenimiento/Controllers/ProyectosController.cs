using CapaEntidad;
using CapaNegocio;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Implementacion_Mantenimiento.Controllers
{
    [Authorize]
    public class ProyectosController : Controller
    {
        private CN_Proyectos negocioProyectos = new CN_Proyectos();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarProyectos()
        {
            List<Proyectos> lista = negocioProyectos.Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarProyecto(Proyectos proyecto)
        {
            string mensaje = string.Empty;
            bool resultado;

            if (proyecto.ProyectoID == 0)
            {
                int id = negocioProyectos.Crear(proyecto, out mensaje);
                resultado = id > 0;
            }
            else
            {
                resultado = negocioProyectos.Modificar(proyecto, out mensaje);
            }

            return Json(new { resultado, mensaje });
        }

        [HttpPost]
        public JsonResult EliminarProyecto(int proyectoID)
        {
            string mensaje = string.Empty;
            bool resultado = negocioProyectos.Eliminar(proyectoID, out mensaje);
            return Json(new { resultado, mensaje });
        }

        public ActionResult Reportes()
        {
            return View();
        }
    }
}
