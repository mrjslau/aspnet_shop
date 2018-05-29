using System.Web;
using System.Web.Optimization;

namespace FuriousWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Design/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/Design/bootstrap.js",
                      "~/Scripts/Design/respond.js"));

            bundles.Add(new StyleBundle("~/Content/Styles/css").Include(
                      "~/Content/Styles/bootstrap.css",
                      "~/Content/Styles/site.css"));

            bundles.Add(new StyleBundle("~/Content/Styles/shop").Include(
                      "~/Content/Styles/bootstrap.min.css",
                      "~/libs/font-awesome/css/font-awesome.min.css",
                      "~/Content/Styles/prettyPhoto.css",
                      "~/Content/Styles/price-range.css",
                      "~/Content/Styles/animate.css",
                      "~/Content/Styles/main.css",
                      "~/Content/Styles/responsive.css"));
        }
    }
}
