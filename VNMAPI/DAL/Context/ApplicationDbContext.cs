using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class ApplicationDbContext: IdentityDbContext<IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);
        }


        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<UserRole>().HasData
            (
                new UserRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin",CreatedBy="1",UpdatedBy="1" },
                new UserRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User",CreatedBy="1",UpdatedBy= "1" }
            );
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


    }
}
