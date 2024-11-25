using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            CD_Roles negocioRoles = new CD_Roles();
            List<Roles> listaRoles = negocioRoles.Listar();
            return Json(listaRoles, JsonRequestBehavior.AllowGet);
        }
    }
}