using CapaEntidad;
using CapaNegocio;
using System.Collections.Generic;
using System.Web.Mvc;
using OfficeOpenXml;
using System;

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


        private CN_Proyectos _proyectosNegocio = new CN_Proyectos();
        public JsonResult ReporteProyectos()
        {
            var lista = _proyectosNegocio.ObtenerReporteProyectos();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }


        public FileResult ExportarReporteProyectos()
        {
            // Configurar el contexto de licencia para EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Obtener los datos del reporte
            var lista = _proyectosNegocio.ObtenerReporteProyectos();

            // Crear el archivo Excel
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reporte de Proyectos");

                // Encabezados
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Nombre";
                worksheet.Cells[1, 3].Value = "Descripción";
                worksheet.Cells[1, 4].Value = "Estado";
                worksheet.Cells[1, 5].Value = "Fecha Inicio";
                worksheet.Cells[1, 6].Value = "Fecha Fin";
                worksheet.Cells[1, 7].Value = "Activo";

                // Insertar datos
                for (int i = 0; i < lista.Count; i++)
                {
                    var proyecto = lista[i];
                    worksheet.Cells[i + 2, 1].Value = proyecto.ProyectoID;
                    worksheet.Cells[i + 2, 2].Value = proyecto.Nombre;
                    worksheet.Cells[i + 2, 3].Value = proyecto.Descripcion;
                    worksheet.Cells[i + 2, 4].Value = proyecto.Estado;

                    if (proyecto.FechaInicio is DateTime fechaInicio)
                    {
                        worksheet.Cells[i + 2, 5].Value = fechaInicio.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        worksheet.Cells[i + 2, 5].Value = "Sin fecha";
                    }

                    if (proyecto.FechaFin is DateTime fechaFin)
                    {
                        worksheet.Cells[i + 2, 6].Value = fechaFin.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        worksheet.Cells[i + 2, 6].Value = "Sin fecha";
                    }

                    worksheet.Cells[i + 2, 7].Value = proyecto.Activo ? "Sí" : "No";
                }

                // Ajustar columnas automáticamente
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Retornar el archivo Excel
                var excelData = package.GetAsByteArray();
                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteProyectos.xlsx");
            }
        }


    }
}
