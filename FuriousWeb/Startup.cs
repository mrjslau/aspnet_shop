using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FuriousWeb.Startup))]
namespace FuriousWeb
{
    public partial class Startup
    {
//tado komentaras
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
