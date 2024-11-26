using CapaDatos;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;

namespace Implementacion_Mantenimiento.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarRoles()
        {
            CN_Roles negocioRoles = new CN_Roles();
            List<Roles> listaRoles = negocioRoles.Listar();
            return Json(listaRoles, JsonRequestBehavior.AllowGet);
        }

    }
}