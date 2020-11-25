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
        public DbSet<Event> Events { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EventWorkers> Events_Workers { get; set; }

        public WorkContext(DbContextOptions<WorkContext> options)
            : base(options)
        {

            Database.EnsureCreated();
        }

        public void SendSalary(int id_user)
        {
            int month = DateTime.Now.Month - 1;
            if (month <= 0)
                month = 12;
            double salary = 0;
            Worker worker = new Worker();
            foreach (var w in Workers)
            {
                if(id_user == w.Id)
                {
                    worker = w;
                    break;
                }
            }
        
            foreach (var e in Events)
            {
                if (e.Date.Month == month)
                {
                    foreach (var ew in Events_Workers)
                    {
                        if (e.Id == ew.Id)
                        {
                            if (id_user == ew.Worker_Id)
                            {
                                salary += ew.Hours * worker.HourlyPayment;
                            }
                        }
                    }
                }
            }

            worker.Money += salary;

            Workers.Update(worker);
        }
    }
}
