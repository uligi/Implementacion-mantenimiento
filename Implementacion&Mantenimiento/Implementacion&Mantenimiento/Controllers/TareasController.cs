using CapaEntidad;
using CapaNegocio;
using System.Collections.Generic;
using System.Web.Mvc;
using OfficeOpenXml;
using System;
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

        private CN_Tareas cn_tareas = new CN_Tareas();

       

        [HttpGet]
        public JsonResult ReporteTareas()
        {
            List<Tareas> lista = cn_tareas.ReporteTareas();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        public FileResult ExportarReporteTareas()
        {
            // Configurar el contexto de licencia para EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Obtener los datos del reporte
            var lista = cn_tareas.ReporteTareas();

            // Crear el archivo Excel
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reporte de Tareas");

                // Encabezados
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Descripción";
                worksheet.Cells[1, 3].Value = "Estado";
                worksheet.Cells[1, 4].Value = "Fecha Inicio";
                worksheet.Cells[1, 5].Value = "Fecha Fin";
                worksheet.Cells[1, 6].Value = "Proyecto";
                worksheet.Cells[1, 7].Value = "Asignado a";

                // Insertar datos
                for (int i = 0; i < lista.Count; i++)
                {
                    var tarea = lista[i];
                    worksheet.Cells[i + 2, 1].Value = tarea.TareaID;
                    worksheet.Cells[i + 2, 2].Value = tarea.Descripcion;
                    worksheet.Cells[i + 2, 3].Value = tarea.Estado;

                    if (tarea.FechaInicio is DateTime fechaInicio)
                    {
                        worksheet.Cells[i + 2, 4].Value = fechaInicio.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        worksheet.Cells[i + 2, 4].Value = "Sin fecha";
                    }

                    if (tarea.FechaFin is DateTime fechaFin)
                    {
                        worksheet.Cells[i + 2, 5].Value = fechaFin.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        worksheet.Cells[i + 2, 5].Value = "Sin fecha";
                    }

                    worksheet.Cells[i + 2, 6].Value = tarea.oProyectos?.Nombre ?? "Sin Proyecto";
                    worksheet.Cells[i + 2, 7].Value = tarea.oUsuarios?.oPersonas?.NombreCompleto ?? "Sin Asignar";
                }

                // Ajustar columnas automáticamente
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Retornar el archivo Excel
                var excelData = package.GetAsByteArray();
                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteTareas.xlsx");
            }
        }
    }
}
