using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diary.Models
{
    public class Event
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public DateTime Date { set; get; }
        public int Event_Workers_Id { set; get; }
    }
}
