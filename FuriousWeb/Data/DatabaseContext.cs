﻿using System.Data.Entity;
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
            modelBuilder.Entity<User>().ToTable("Users");                   //default: AspNetUsers
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");           //default: AspNetRoles
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");   //default: AspNetUserRoles
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims"); //default: AspNetUserClaims
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins"); //default: AspNetUserLogins
        }

        //db lenteles (neiskaitant defaultiniu: Users, Roles ir t.t.)

        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
    }
}