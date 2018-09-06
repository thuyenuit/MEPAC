using System.Web;
using System.Web.Optimization;

namespace MEPAC.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            /* bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/Scripts/jquery-{version}.js"));

             bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                         "~/Scripts/jquery.validate*"));*/

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            /* bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                         "~/Scripts/modernizr-*"));

             bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~/Scripts/bootstrap.js",
                       "~/Scripts/respond.js"));

             bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Content/bootstrap.css",
                       "~/Content/site.css"));*/

            bundles.Add(new ScriptBundle("~/client/bundles/jquery").Include(
                      "~/Assets/Client/js/jquery-2.1.4.min.js",
                      "~/Assets/Client/js/bootstrap.js",
                      "~/Assets/Client/js/move-top.js",
                      "~/Assets/Client/js/easing.js",
                      "~/Assets/Client/js/customize.js"));

            bundles.Add(new StyleBundle("~/client/bundles/css").Include(
                      "~/Assets/Client/css/bootstrap.css",
                      // "~/Assets/Client/css/font-awesome.min.css",
                      "~/Assets/Admin/plusgins/font-awesome/css/font-awesome.min.css",
                       "~/Assets/Admin/plusgins/Ionicons/css/ionicons.min.css",
                      "~/Assets/Client/css/style.css",
                      "~/Assets/Client/css/customize.css"));

            bundles.Add(new StyleBundle("~/Assets/Admin/css").Include(
                      "~/Assets/css/bootstrap.min.css",
                      "~/Assets/css/jquery-ui.custom.min.css",
                      "~/Assets/css/chosen.min.css",
                      "~/Assets/css/fonts.googleapis.com.css",
                       "~/Assets/css/bootstrap-datetimepicker.min.css",
                       "~/Assets/css/daterangepicker.min.css",
                      "~/Assets/css/ace.min.css",
                      "~/Assets/css/ace-skins.min.css",
                      "~/Assets/css/ace-rtl.min.css"));
        }
    }
}
