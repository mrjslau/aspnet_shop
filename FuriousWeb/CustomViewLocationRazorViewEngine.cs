using System.Web.Mvc;

namespace FuriousWeb
{
    public class CustomViewLocationRazorViewEngine : RazorViewEngine
    {
        public CustomViewLocationRazorViewEngine()
        {
            ViewLocationFormats = new[]
            {
                "~/Views/Store/{1}/{0}.cshtml",
                "~/Views/Store/{0}.cshtml",
                "~/Views/Store/Shared/{0}.cshtml",
                "~/Views/Admin/{1}/{0}.cshtml",
                "~/Views/Admin/{0}.cshtml",
                "~/Views/Admin/Shared/{0}.cshtml",
            };
            PartialViewLocationFormats = new[]
            {
                "~/Views/Store/{1}/{0}.cshtml",
                "~/Views/Store/{0}.cshtml",
                "~/Views/Store/Shared/{0}.cshtml",
                "~/Views/Admin/{1}/{0}.cshtml",
                "~/Views/Admin/{0}.cshtml",
                "~/Views/Admin/Shared/{0}.cshtml",
            };
        }
    }
}
