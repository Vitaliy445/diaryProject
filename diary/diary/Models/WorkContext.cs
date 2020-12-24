using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diary.Models
{
    public class WorkContext : DbContext
    {
        public DbSet<Worker> workers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EventWorkers> Events_Workers { get; set; }
        public DbSet<Role> Roles { get; set; }

        public WorkContext(DbContextOptions<WorkContext> options)
            : base(options)
        {       

            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string moderatorRoleName = "moderator";
            string userRoleName = "user";


            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role moderatorRole = new Role { Id = 2, Name = moderatorRoleName };
            Role userRole = new Role { Id = 3, Name = userRoleName };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole , moderatorRole, userRole });
            base.OnModelCreating(modelBuilder);
        }
    }
}
