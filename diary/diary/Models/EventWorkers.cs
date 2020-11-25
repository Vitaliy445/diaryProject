using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diary.Models
{
    public class EventWorkers
    {
        public int Id { set; get; }
        public int Event_Id { set; get; }
        public int Worker_Id { set; get; }
        public double Hours { set; get; }


    }
}
