using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diary.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddlName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public double HourlyPayment { get; set; }
        public string Status { get; set; }
        public double Money { get; set; }
        public string Password { get; set; }
        public int Position_Id { get; set; }
        public int Departament_Id { get; set; }

        public string GetFullName()
        {
            return (FirstName + " " + MiddlName + " " + LastName);
        }
    }
}
