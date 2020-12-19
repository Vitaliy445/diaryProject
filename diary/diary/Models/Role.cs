using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diary.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Worker> Workers { get; set; }
        public Role()
        {
            Workers = new List<Worker>();
        }
    }
}
