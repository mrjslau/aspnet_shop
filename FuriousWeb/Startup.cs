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
                    using (var userManager = new ApplicationUserManager(new UserStore<User>(db)) { PasswordValidator = new MinimumLengthValidator(0) })
                    {
                        //creating roles
                        var adminRole = new IdentityRole() { Name = "Admin" };
                        roleManager.Create(adminRole);

                        var simpleUserRole = new IdentityRole() { Name = "User" };
                        roleManager.Create(simpleUserRole);

                        //creating users
                        var adminUser = new User { UserName = "admin@gmail.com", Email = "admin@gmail.com", Id = "1", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(adminUser, "Password!");
                        userManager.AddToRole(adminUser.Id, adminRole.Name);

                        var simpleUser = new User() { UserName = "user@gmail.com", Email = "user@gmail.com", Id = "2", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser, "Password!");
                        userManager.AddToRole(simpleUser.Id, simpleUserRole.Name);

                        var simpleUser1 = new User() { UserName = "user1@gmail.com", Email = "user1@gmail.com", Id = "3", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser1, "Password!");
                        userManager.AddToRole(simpleUser1.Id, simpleUserRole.Name);

                        var simpleUser2 = new User() { UserName = "user2@gmail.com", Email = "user2@gmail.com", Id = "4", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser2, "Password!");
                        userManager.AddToRole(simpleUser2.Id, simpleUserRole.Name);

                        /*var simpleUser3 = new User() { UserName = "user3@gmail.com", Email = "user3@gmail.com", Id = "5", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser3, "Password!");
                        userManager.AddToRole(simpleUser3.Id, simpleUserRole.Name);

                        var simpleUser4 = new User() { UserName = "user4@gmail.com", Email = "user4@gmail.com", Id = "6", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser4, "Password!");
                        userManager.AddToRole(simpleUser4.Id, simpleUserRole.Name);

                        var simpleUser5 = new User() { UserName = "user5@gmail.com", Email = "user5@gmail.com", Id = "7", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser5, "Password!");
                        userManager.AddToRole(simpleUser5.Id, simpleUserRole.Name);

                        var simpleUser6 = new User() { UserName = "user6@gmail.com", Email = "user6@gmail.com", Id = "8", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser6, "Password!");
                        userManager.AddToRole(simpleUser6.Id, simpleUserRole.Name);

                        var simpleUser7 = new User() { UserName = "user7@gmail.com", Email = "user7@gmail.com", Id = "9", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser7, "Password!");
                        userManager.AddToRole(simpleUser7.Id, simpleUserRole.Name);

                        var simpleUser8 = new User() { UserName = "user8@gmail.com", Email = "user8@gmail.com", Id = "10", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser8, "Password!");
                        userManager.AddToRole(simpleUser8.Id, simpleUserRole.Name);

                        var simpleUser9 = new User() { UserName = "user9@gmail.com", Email = "user9@gmail.com", Id = "11", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser9, "Password!");
                        userManager.AddToRole(simpleUser9.Id, simpleUserRole.Name);

                        var simpleUser10 = new User() { UserName = "user10@gmail.com", Email = "user10@gmail.com", Id = "12", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser10, "Password!");
                        userManager.AddToRole(simpleUser10.Id, simpleUserRole.Name);

                        var simpleUser11 = new User() { UserName = "user11@gmail.com", Email = "user11@gmail.com", Id = "13", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser11, "Password!");
                        userManager.AddToRole(simpleUser11.Id, simpleUserRole.Name);

                        var simpleUser12 = new User() { UserName = "user12@gmail.com", Email = "user12@gmail.com", Id = "14", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser12, "Password!");
                        userManager.AddToRole(simpleUser12.Id, simpleUserRole.Name);

                        var simpleUser13 = new User() { UserName = "user13@gmail.com", Email = "user13@gmail.com", Id = "15", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser13, "Password!");
                        userManager.AddToRole(simpleUser13.Id, simpleUserRole.Name);

                        var simpleUser14 = new User() { UserName = "user14@gmail.com", Email = "user14@gmail.com", Id = "16", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser14, "Password!");
                        userManager.AddToRole(simpleUser14.Id, simpleUserRole.Name);

                        var simpleUser15 = new User() { UserName = "user15@gmail.com", Email = "user15@gmail.com", Id = "17", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser15, "Password!");
                        userManager.AddToRole(simpleUser15.Id, simpleUserRole.Name);

                        var simpleUser16 = new User() { UserName = "user16@gmail.com", Email = "user16@gmail.com", Id = "18", Phone = "888888888", Address = "address", Name = "Name", Lastname = "Lastname" };
                        userManager.Create(simpleUser16, "Password!");
                        userManager.AddToRole(simpleUser16.Id, simpleUserRole.Name);
                        */
                    }
                }
            }
        }
    }
}