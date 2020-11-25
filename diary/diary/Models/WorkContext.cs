using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diary.Models
{
    public class WorkContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }

        public WorkContext(DbContextOptions<WorkContext> options)
            : base(options)
        {

            Database.EnsureCreated();
        }
    }
}
