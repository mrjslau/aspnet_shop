﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FuriousWeb.Startup))]
namespace FuriousWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);//Gabrielius redaguoja//Marijus neredaguoja
            ConfigureAuth(app);
            //testukas
        }
    }
}
