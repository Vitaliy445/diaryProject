using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diary.Models
{
    public static class SampleData
    {
        public static void Initialize(WorkContext context)
        {
            if (!context.workers.Any())
            {
               
                context.SaveChanges();
            }
        }
    }
}
