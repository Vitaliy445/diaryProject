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

    }
}
