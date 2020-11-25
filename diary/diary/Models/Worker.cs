using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diary.Models
{
    public class Worker
    {
        public int Id { set; get; }
        public string FirstName { set; get; }
        public string MiddlName { set; get; }
        public string LastName { set; get; }
        public int Position_Id { set; get; }
        public double HourlyPayment { set; get; }
    }
}
