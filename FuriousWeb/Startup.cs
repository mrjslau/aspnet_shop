using FuriousWeb.Data;
using FuriousWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Web;

[assembly: OwinStartupAttribute(typeof(FuriousWeb.Startup))]
namespace FuriousWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);      
        }

        public static void CreateDefaultRolesAndUsers()
        {
            using (var db = new DatabaseContext())
            using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db)))
            {
                if (!roleManager.RoleExists("Admin"))
                {
                    using (var userManager = new ApplicationUserManager(new UserStore<User>(db)) { PasswordValidator = new MinimumLengthValidator (0) })
                    {
                        //creating roles
                        var adminRole = new IdentityRole() { Name = "Admin" };
                        roleManager.Create(adminRole);

                        var simpleUserRole = new IdentityRole() { Name = "SimpleUser" };
                        roleManager.Create(simpleUserRole);
                        
                        //creating users
                        var adminUser = new User() { UserName = "ADMIN." };
                        userManager.Create(adminUser, "ADMIN.");
                        userManager.AddToRole(adminUser.Id, adminRole.Name);

                        var simpleUser = new User() { UserName = "USER." };
                        userManager.Create(simpleUser, "USER.");
                        userManager.AddToRole(simpleUser.Id, simpleUserRole.Name);
                    }
                }
            }
        }
    }
}
