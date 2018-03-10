using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FuriousWeb.Startup))]
namespace FuriousWeb
{
    public partial class Startup
    {

        //komentaras
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
