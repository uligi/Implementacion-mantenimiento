using System.Web;
using System.Web.Optimization;

namespace Implementacion_Mantenimiento
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/jquery-3.7.0.js",
                    
                      "~/Scripts/Validaciones.js"));

            bundles.Add(new ScriptBundle("~/bundles/complementos").Include(
                        "~/Scripts/fontawesome/all.min.js",
                        "~/Scripts/scripts.js",
                        "~/Scripts/loadingoverlay/loadingoverlay.min.js",
                        "~/Scripts/sweetalert.min.js",
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.responsive.js"

             ));

          

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                        "~/Scripts/vendor/jquery/jquery.min.js",
                        "~/Scripts/vendor/bootstrap/js/bootstrap.bundle.min.js",
                        "~/Scripts/vendor/jquery-easing/jquery.easing.min.js",
                        "~/Scripts/js/sb-admin-2.min.js"
                    ));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css",
                     "~/Content/site2.css",
                     "~/Content/DataTables/css/jquery.dataTables.css",
                        "~/Content/DataTables/css/responsive.dataTables.css",
                        "~/Content/sweetalert.css"));
        }
    }
}
