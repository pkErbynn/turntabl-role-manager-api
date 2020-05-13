using TurntablRoleManager.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace TurntablRoleManager.API.DbContexts
{
    public class TurntablDbContext : DbContext
    {
        public TurntablDbContext(DbContextOptions<TurntablDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeRole>()
                .HasKey(er => new { er.EmployeeId, er.Id });
        
            modelBuilder.Entity<EmployeeRole>().HasData(
                new EmployeeRole()
                {
                    EmployeeId = 1,
                    Id = Guid.Parse("7c4854f2-bbfc-4d5a-88fa-9fe19e480bc0")
                });

              base.OnModelCreating(modelBuilder);
          }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
    }
}
