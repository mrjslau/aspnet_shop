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
            Database.SetInitializer<DatabaseContext>(new CreateDatabaseIfNotExists<DatabaseContext>());
        }

        public static DatabaseContext Create()
        {
            return new DatabaseContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //pervadinam default'inius lenteliu pavadinimus
            modelBuilder.Entity<User>().ToTable("Users");                   //default: AspNetUsers
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");           //default: AspNetRoles
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");   //default: AspNetUserRoles
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims"); //default: AspNetUserClaims
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins"); //default: AspNetUserLogins
        }

        //db lenteles (neiskaitant defaultiniu: Users, Roles ir t.t.)

        public DbSet<Product> Products { get; set; } //prekes
        public DbSet<Order> Orders { get; set; } //užsakymai
        public DbSet<OrderDetail> OrderDetails { get; set; } //užsakymu duomenys
        public DbSet<Payment> Payments { get; set; } //mokejimai
        public DbSet<ProductImage> ProductImages { get; set; } //prekes paveiksleliai
    }
}