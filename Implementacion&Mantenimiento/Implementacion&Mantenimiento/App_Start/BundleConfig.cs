using System.Web;
using System.Web.Optimization;

namespace Implementacion_Mantenimiento
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                     "~/Scripts/jquery-3.7.0.js",
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.responsive.js",
                        "~/Scripts/Validaciones.js"));

            bundles.Add(new ScriptBundle("~/bundles/complementos").Include(
                      "~/Scripts/fontawesome/all.min.js",
                      "~/Scripts/scripts.js",
                      "~/Scripts/loadingoverlay/loadingoverlay.min.js",
                      "~/Scripts/sweetalert.min.js"

           ));

            /*        bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                                "~/Scripts/jquery.validate*"));

                    // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
                    // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
                    bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                                "~/Scripts/modernizr-*"));*/

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js", "~/Scripts/BD/sb-admin-2.js",
                        "~/Scripts/BD/sb-admin-2.min.js",
                        "~/Scripts/BD/demo/chart-area-demo.js",
                        "~/Scripts/BD/demo/chart-bar-demo.js",
                        "~/Scripts/BD/demo/chart-pie-demo.js",
                        "~/Scripts/BD/demo/datatables-demo.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css",
                     "~/Content/site2.css",
                     "~/Content/DataTables/css/jquery.dataTables.css",
                        "~/Content/DataTables/css/responsive.dataTables.css",
                        "~/Content/sweetalert.css"));
        }
    }
}
