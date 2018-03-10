using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using FuriousWeb.Models;

namespace FuriousWeb.Data
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static DatabaseContext Create()
        {
            return new DatabaseContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //pervadinam default'inius lenteliu pavadinimus
            modelBuilder.Entity<User>().ToTable("Users");                   //AspNetUsers
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");           //AspNetRoles
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");   //AspNetUserRoles
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims"); //AspNetUserClaims
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins"); //AspNetUserLogins
        }

        //duombazes lenteles (neiskaitant defaultiniu: Users, Roles ir t.t.)

        public DbSet<Product> Products { get; set; }
    }
}