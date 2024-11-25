using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Implementacion_Mantenimiento.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            string mensajeConexion = string.Empty;

            try
            {
                // Cadena de conexión
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

                // Intentar abrir la conexión
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    mensajeConexion = "Conexión exitosa a la base de datos.";
                }
            }
            catch (Exception ex)
            {
                // Capturar errores de conexión
                mensajeConexion = "Error al conectar a la base de datos: " + ex.Message;
            }

            // Pasar el mensaje a la vista
            ViewBag.MensajeConexion = mensajeConexion;
            return View();
        }

    }
}